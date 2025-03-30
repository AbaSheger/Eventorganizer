package com.borsvy.config;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.client.RestTemplate;
import org.springframework.http.client.ClientHttpRequestInterceptor;
import org.springframework.retry.backoff.ExponentialBackOffPolicy;
import org.springframework.retry.policy.SimpleRetryPolicy;
import org.springframework.retry.support.RetryTemplate;
import org.springframework.http.client.BufferingClientHttpRequestFactory;
import org.springframework.http.client.SimpleClientHttpRequestFactory;
import org.springframework.web.client.DefaultResponseErrorHandler;
import org.springframework.web.client.HttpClientErrorException;
import org.springframework.web.client.HttpServerErrorException;
import org.springframework.http.HttpStatusCode;
import org.springframework.http.client.ClientHttpResponse;
import org.springframework.lang.NonNull;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Configuration
public class RestTemplateConfig {
    private static final Logger logger = LoggerFactory.getLogger(RestTemplateConfig.class);

    @Bean
    public RestTemplate restTemplate(RetryTemplate retryTemplate) {
        // Create RestTemplate with proper request factory for buffering
        SimpleClientHttpRequestFactory requestFactory = new SimpleClientHttpRequestFactory();
        // Set timeouts
        requestFactory.setConnectTimeout(10000);
        requestFactory.setReadTimeout(30000);
        
        // Use BufferingClientHttpRequestFactory to allow reading response multiple times
        RestTemplate restTemplate = new RestTemplate(
            new BufferingClientHttpRequestFactory(requestFactory)
        );
        
        // Add rate limit interceptor
        List<ClientHttpRequestInterceptor> interceptors = new ArrayList<>();
        interceptors.add(new RateLimitInterceptor(retryTemplate));
        restTemplate.setInterceptors(interceptors);
        
        // Configure custom error handler
        restTemplate.setErrorHandler(new StockApiErrorHandler());
        
        logger.info("RestTemplate configured with RateLimitInterceptor and custom error handler");
        return restTemplate;
    }
    
    @Bean
    public RetryTemplate retryTemplate() {
        RetryTemplate retryTemplate = new RetryTemplate();
        
        // Configure retry policy with specific exceptions
        Map<Class<? extends Throwable>, Boolean> retryableExceptions = new HashMap<>();
        retryableExceptions.put(RateLimitException.class, true);
        retryableExceptions.put(HttpClientErrorException.TooManyRequests.class, true);
        retryableExceptions.put(HttpServerErrorException.ServiceUnavailable.class, true);
        retryableExceptions.put(HttpServerErrorException.GatewayTimeout.class, true);
        retryableExceptions.put(IOException.class, true);
        
        SimpleRetryPolicy retryPolicy = new SimpleRetryPolicy(3, retryableExceptions, true);
        retryTemplate.setRetryPolicy(retryPolicy);
        
        // Configure backoff policy
        ExponentialBackOffPolicy backOffPolicy = new ExponentialBackOffPolicy();
        backOffPolicy.setInitialInterval(1000);
        backOffPolicy.setMultiplier(2.0);
        backOffPolicy.setMaxInterval(10000);
        retryTemplate.setBackOffPolicy(backOffPolicy);
        
        logger.info("RetryTemplate configured with exponential backoff and specific retryable exceptions");
        return retryTemplate;
    }
    
    // Custom error handler for stock API responses
    public static class StockApiErrorHandler extends DefaultResponseErrorHandler {
        private static final Logger logger = LoggerFactory.getLogger(StockApiErrorHandler.class);
        
        @Override
        public boolean hasError(ClientHttpResponse response) throws IOException {
            HttpStatusCode statusCode = response.getStatusCode();
            boolean isError = statusCode.is5xxServerError() || statusCode.value() == 429;
            
            if (isError) {
                logger.warn("API response error: {} - {}", statusCode.value(), response.getStatusText());
            }
            
            return isError;
        }

        @Override
        public void handleError(@NonNull ClientHttpResponse response) throws IOException {
            HttpStatusCode statusCode = response.getStatusCode();
            
            // Special handling for rate limit errors (429)
            if (statusCode.value() == 429) {
                logger.error("Rate limit exceeded: {}", response.getStatusText());
                throw new RateLimitException("Rate limit exceeded: " + response.getStatusText());
            }
            
            // Log other errors
            logger.error("API error: {} - {}", statusCode.value(), response.getStatusText());
            
            // Use default handling for other errors
            super.handleError(response);
        }
    }
    
    // Custom exception for rate limiting
    public static class RateLimitException extends RuntimeException {
        public RateLimitException(String message) {
            super(message);
        }
    }
}
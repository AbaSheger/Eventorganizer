package com.borsvy;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;
import org.springframework.retry.annotation.EnableRetry;

@SpringBootApplication
@EntityScan("com.borsvy.model")
@EnableJpaRepositories("com.borsvy.repository")
@EnableRetry
public class BorsVyApplication {

    public static void main(String[] args) {
        SpringApplication.run(BorsVyApplication.class, args);
    }
}
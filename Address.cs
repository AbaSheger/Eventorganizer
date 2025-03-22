using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    /// <summary>
    /// Represents an address with street, city, zip code, and country.
    /// </summary>
    public class Address
    {
        // Private fields
        private string street;
        private string city;
        private string zipCode;
        private Countries country;

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="street">The street address.</param>
        /// <param name="city">The city.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="country">The country.</param>
        public Address(string street, string city, string zipCode, Countries country)
        {
            this.Street = street;
            this.City = city; // Calls the City property
            this.ZipCode = zipCode;
            this.Country = country;
        }

        /// <summary>
        /// Gets or sets the street address.
        /// </summary>
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City
        {
            get { return city; }
            set
            {
                // Simple validation
                if (string.IsNullOrEmpty(value))
                {
                    city = "Unknown City";
                }
                else
                {
                    city = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public Countries Country
        {
            get { return country; }
            set { country = value; }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("{0,-25} {1,-10} {2,-15} {3}",
                Street,
                ZipCode,
                City,
                Country.ToString().Replace('_', ' '));
        }
    }
}

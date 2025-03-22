using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer
{
    /// <summary>
    /// Represents a participant in an event.
    /// </summary>
    public class Participant
    {
        private string firstName;
        private string lastName;
        private Address address;

        /// <summary>
        /// Initializes a new instance of the <see cref="Participant"/> class with specified first name, last name, and address.
        /// </summary>
        /// <param name="firstName">The first name of the participant.</param>
        /// <param name="lastName">The last name of the participant.</param>
        /// <param name="address">The address of the participant.</param>
        public Participant(string firstName, string lastName, Address address)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
        }

        /// <summary>
        /// Gets or sets the first name of the participant.
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set
            {
                // Simple validation
                if (string.IsNullOrEmpty(value))
                {
                    firstName = "Unknown";
                }
                else
                {
                    firstName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name of the participant.
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set
            {
                // Simple validation
                if (string.IsNullOrEmpty(value))
                {
                    lastName = "Unknown";
                }
                else
                {
                    lastName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the address of the participant.
        /// </summary>
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Returns a string that represents the current participant.
        /// </summary>
        /// <returns>A string that represents the current participant.</returns>
        public override string ToString()
        {
            return string.Format("{0,-8} {1,-45} {2}",
                lastName.ToUpper() + ",",
                firstName,
                address.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    public abstract class Person : BaseEntity
    {
        /// <summary>
        /// Primary key of the Person
        /// </summary>
        public new int Id { get; set; }

        /// <summary>
        /// First name of the Person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the Person.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Address of the Person.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email of the Person.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone of the Person.
        /// </summary>
        public string Phone { get; set; }
    }
}

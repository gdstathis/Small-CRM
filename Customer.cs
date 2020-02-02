using System;
using System.Collections.Generic;

namespace SmallCrm
{
    class Customer
    {
        /// <summary>
        /// Setter and getter for FirstName
        /// </summary>
        public string FirstName{ get; set; }

        /// <summary>
        /// Setter and getter for LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Setter and getter for the TotalMoney, the money who spends customer
        /// </summary>
        public decimal TotalMoney { get; set; }   

        /// <summary>
        /// Setter and getter for the orders that made from Customer
        /// </summary>
        public List<Order> Orders { get; set; } 

        /// <summary>
        /// A constructor for Customer without arguments.
        /// Initialize from user input firstname and lastname
        /// For the simplicity of the project I didnt use this constuructor
        /// </summary>
        public Customer()
        {
            Console.WriteLine("Give the firstname of the customer");
            try
            {
                FirstName = Console.ReadLine();
            }catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nFirst name must not be null");
            } 
            try
            {
                Console.WriteLine("Give the lastname of the customer");
                LastName = Console.ReadLine();
            }catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nLast name must not be null");
            }
            Orders = new List<Order>();
        }

        /// <summary>
        /// A constructor for Customer with 2 arguments (firstname,lastname) and 
        /// create a list of order.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastname"></param>
        public Customer(string firstName, string lastname)
        {
            if (firstName==null || lastname == null) {
                throw new ArgumentNullException("Firsname and lastname must not be null");
            }
            FirstName = firstName;
            LastName = lastname;
            Orders = new List<Order>();
        }
    }
}

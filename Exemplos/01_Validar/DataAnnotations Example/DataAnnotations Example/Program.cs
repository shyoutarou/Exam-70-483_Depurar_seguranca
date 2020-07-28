using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAnnotations_Example
{
    public class Customer
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string FirstName { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        public Address ShippingAddress { get; set; }
        [Required]
        public Address BillingAddress { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string AddressLine1 { get; set; }
        [Required, MaxLength(20)]
        public string AddressLine2 { get; set; }
        [Required, MaxLength(20)]
        public string City { get; set; }
        [RegularExpression(@"^[1 - 9][0 - 9]{3}\s?[a - zA - Z]{2}$")]
        public string ZipCode { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer();
            customer.FirstName = "João";
            customer.LastName = "1234567891012345678910";
            var context = new ValidationContext(customer, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(customer, context, results, true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
            }

            Console.ReadKey();
        }
    }
}

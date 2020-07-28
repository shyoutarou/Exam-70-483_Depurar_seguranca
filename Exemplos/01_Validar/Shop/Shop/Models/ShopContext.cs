using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Shop.Models
{
    public class ShopContext : DbContext
    {
        static ShopContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ShopContext>());
        }

        public IDbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .HasRequired(bm => bm.BillingAddress)
            .WithMany().WillCascadeOnDelete(false);

            using (ShopContext ctx = new ShopContext())
            {
                Address a = new Address
                {
                    //AddressLine1 = "Somewhere 1",
                    //AddressLine2 = "At some floor",
                    ////City = "SomeCity",
                    //ZipCode = "1111AA"
                };

                GenericValidator<Address>.Validate(a);

                Customer c = new Customer()
                {
                    //FirstName = "John",
                    LastName = "Doe",
                    BillingAddress = a,
                    ShippingAddress = a,
                };

                GenericValidator<Customer>.Validate(c);

                ctx.Customers.Add(c);
                ctx.SaveChanges();
            }

        }
    }

    public static class GenericValidator<T>
    {
        public static IList<ValidationResult> Validate(T entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity, null, null);
            Validator.TryValidateObject(entity, context, results);
            return results;
        }
    }
}

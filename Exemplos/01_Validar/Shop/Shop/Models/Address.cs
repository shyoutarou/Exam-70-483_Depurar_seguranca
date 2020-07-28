using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
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
}
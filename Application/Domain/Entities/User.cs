using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation
        public ICollection<Property> PropertiesCreated { get; set; }
        public ICollection<Rental> RentalsCreated { get; set; }


    }
}

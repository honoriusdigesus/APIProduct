using System.ComponentModel.DataAnnotations;

namespace APIProduct.Domain.Entities
{
    public class UserDomain
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string IdentityDocument { get; set; } = null!;

        public int RoleId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}

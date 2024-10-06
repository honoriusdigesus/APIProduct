using System.ComponentModel.DataAnnotations;

namespace APIProduct.Presenter.Entities
{
    public class UserPresenter
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string IdentityDocument { get; set; } = null!;
        public int RoleId { get; set; } = 1;
        public DateTime? CreatedAt { get; set; }
    }
}

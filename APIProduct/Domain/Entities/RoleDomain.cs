using System.ComponentModel.DataAnnotations;

namespace APIProduct.Domain.Entities
{
    public class RoleDomain
    {

        public int RoleId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string RoleName { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace APIProduct.Domain.Entities
{
    public class ProductDomain
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

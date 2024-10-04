namespace APIProduct.Presenter.Entities
{
    public class ProductPresenter
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

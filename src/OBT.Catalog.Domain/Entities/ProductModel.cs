using OBT.Products.Domain.DomainObjects;

namespace OBT.Products.Domain.Entities
{
    public class ProductModel : BaseEntity, IAggregateRoot
    {

        // EF Construtor
        public ProductModel()
        {
        }

        public ProductModel(string title, string description, double price, int quantity)
        {
            Title = title;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}
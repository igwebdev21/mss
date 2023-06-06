namespace MaplrSugarSnack.Models
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}

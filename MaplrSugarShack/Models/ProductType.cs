namespace MaplrSugarSnack.Models
{
    public class ProductType: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Product> Products { get; set;} = new List<Product>();
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaplrSugarSnack.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProductTypeId { get; set; }
        [DisplayName("Syrup type")]
        public ProductType ProductType { get; set; }
        [DisplayFormat(DataFormatString ="{0:c}")]
        public decimal Price { get; set; }
    }
}

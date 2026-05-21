using System.ComponentModel.DataAnnotations;

namespace OrderServices.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public bool IsPayment { get; set; }
        public DateTime? CreatedData { get; set; }
        public string? Status { get; set; }
        public int UserId { get; set; }

    }
}

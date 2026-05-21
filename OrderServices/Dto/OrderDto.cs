namespace OrderServices.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public bool IsPayment { get; set; }
        public string? Status { get; set; }
        public int UserId { get; set; }
    }
}

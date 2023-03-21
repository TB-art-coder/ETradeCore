namespace DataAccess.Models
{
    public class CartItemGroupByModel
    {
        // gruplanacak özellikler
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }


        // gruplama sonucunda hesaplayacağımız özellikler
        public double TotalPrice { get; set; }
        public int ProductCount { get; set; }
    }
}

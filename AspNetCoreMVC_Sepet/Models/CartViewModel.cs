using AspNetCoreMVC_Sepet.Models.Context;

namespace AspNetCoreMVC_Sepet.Models
{
    public class CartViewModel
    {
        //Bir sepetin ...'sı olur.
        public CartViewModel()
        {
            Quantity = 1;
        }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal? SubTotal
        {
            get //get bloğu property'nin değerini okumak için kullanılır.
            {
                return Quantity * Product.UnitPrice;

            }
        }//set bloğu, property'nin değerini değiştirmek veya bir değer atamak için kullanılır.
    }
}

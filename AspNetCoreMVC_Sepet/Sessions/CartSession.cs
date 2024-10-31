using AspNetCoreMVC_Sepet.Models;

namespace AspNetCoreMVC_Sepet.Sessions
{
    public class CartSession
    {

        //Bir CartSession (Oturum) ...'sı olur.

        //Bu nesne Server'da session olarak tutulacak.

        //List (Sepetin kendisi)
        public Dictionary<int, CartViewModel> MyCart = new Dictionary<int, CartViewModel>();

        // 1 Ekmek
        //Id: 2 Pirinç
        //Id: 3 Kek


        //Sepete Ürün Ekleme
        public void AddToCart(int productId,CartViewModel cart)
        {
            if(MyCart.ContainsKey(productId)) 
            {
                MyCart[productId].Quantity++;
                return;
            }
            MyCart.Add(productId, cart);
        }

        //Sepet güncelleme
        public void UpdateCart(Dictionary<int,int> updatedItems)
        {
            foreach(var item in updatedItems) 
            { 
                if (MyCart.ContainsKey(item.Key) && item.Value > 0)
                {
                    MyCart[item.Key].Quantity = item.Value; // Value = Quantity
                }
            }
        }
        
        //Sepet silme
        public void DeleteCart(int productId) 
        { 
            if (MyCart.ContainsKey(productId)) 
            { 
                MyCart.Remove(productId);
            }
        }
    }
}

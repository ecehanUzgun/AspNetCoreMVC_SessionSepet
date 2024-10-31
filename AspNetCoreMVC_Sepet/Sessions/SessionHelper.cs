using Newtonsoft.Json;

namespace AspNetCoreMVC_Sepet.Sessions
{
    public class SessionHelper
    {
        //GET: Server'da oluşturulan session içerisindeki bilgiyi object (nesne)'ye dönüştürerek bize teslim edecek olan eylem.
        public static T GetProductFromJson<T>(ISession session, string key)
        {
            var result = session.GetString(key);
            if (result == null)
            {
                return default(T);
            }
            else
            {
                var deserializeObject=JsonConvert.DeserializeObject<T>(result);
                return deserializeObject;
            }
        }


        //SET: Dışarıdan alınan object (nesne)'yi json'a dönüştüren ve bu json formatını da belirtilen session içerisinde yine belirtilen isim ile tutulmasını sağlayan metotddur.  
        public static void SetProductFromJson(ISession session, string key, object value)
        {
            var jsonFormat = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonFormat);
        }
    }
}

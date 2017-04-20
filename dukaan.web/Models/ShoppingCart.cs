using System.Collections.Generic;

namespace dukaan.web.Models
{
    public class ShoppingCart
    {
        public IDictionary<string, ShoppingCartItems> ShoppingCartItems { get; set; }
    }
}

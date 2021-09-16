using ShoppingCart.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Library.ShoppingCart.Models;

namespace ShoppingCart.Models
{
    public class ProductJsonConverter : JsonCreationConverter<Product>
    {
        protected override Product Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["PricePerOunce"] != null || jObject["pricePerOunce"] != null)
            {
                return new ProductByWeight();
            }
            else if (jObject["UnitPrice"] != null || jObject["unitPrice"] != null)
            {
                return new ProductByQuantity();
            }
            else
            {
                return new Product();
            }
        }
    }
}

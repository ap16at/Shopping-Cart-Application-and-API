using ShoppingCart.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Library.ShoppingCart.Models;

namespace ShoppingCart.Models
{
    public class ItemJsonConverter : JsonCreationConverter<InventoryItem>
    {
        protected override InventoryItem Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["PricePerOunce"] != null || jObject["pricePerOunce"] != null)
            {
                return new InventoryItemByWeight();
            }
            else if (jObject["UnitPrice"] != null || jObject["unitPrice"] != null)
            {
                return new InventoryItemByQuantity();
            }
            else
            {
                return new InventoryItem();
            }
        }
    }
}

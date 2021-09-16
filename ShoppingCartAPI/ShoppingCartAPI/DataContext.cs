//using Library.ShoppingCart.Models;
using ShoppingCart.Models;
//using ShoppingCartAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartAPI
{
    public class DataContext
    {
        public static List<InventoryItem> InventoryItems = new List<InventoryItem> {
             new InventoryItemByQuantity { Name = "Lightbulb", Id = Guid.NewGuid(), UnitPrice = 1.50, Description = "1 lightbulb" },
             new InventoryItemByQuantity { Name = "Ketchup", Id = Guid.NewGuid(), UnitPrice = 6.99, Description = "1 Heinz Ketchup bottle" },
             new InventoryItemByQuantity { Name = "Toilet Paper", Id = Guid.NewGuid(), UnitPrice = 2.49, Description = "1 roll" },
             new InventoryItemByQuantity { Name = "Paper Towel Roll", Id = Guid.NewGuid(), UnitPrice = 2.99, Description = "1 roll" },
             new InventoryItemByQuantity { Name = "Toothbrush", Id = Guid.NewGuid(), UnitPrice = 4.99, Description = "1 toothbrush" },
             new InventoryItemByQuantity { Name = "Coke", Id = Guid.NewGuid(), UnitPrice = 1.99, Description = "1 2-liter bottle" },
             new InventoryItemByQuantity { Name = "Lays potato chips", Id = Guid.NewGuid(), UnitPrice = 2.99, Description = "1 bag of chips" },
             new InventoryItemByQuantity { Name = "Spaghetti", Id = Guid.NewGuid(), UnitPrice = 5.99, Description = "1 box of pasta" },
             new InventoryItemByQuantity { Name = "Can of soup", Id = Guid.NewGuid(), UnitPrice = 6.99, Description = "Chicken noodle" },
             new InventoryItemByQuantity { Name = "Frying Pan", Id = Guid.NewGuid(), UnitPrice = 17.99, Description = "Non-stick" },
             new InventoryItemByWeight { Name = "Apples", Id = Guid.NewGuid(), PricePerOunce = 2.79, Description = "Red apple" },
             new InventoryItemByWeight { Name = "Bannanas", Id = Guid.NewGuid(), PricePerOunce = 1.34, Description = "Yellow banana" },
             new InventoryItemByWeight { Name = "Ribeye", Id = Guid.NewGuid(), PricePerOunce = 3.50, Description = "Ribeye steak" },
             new InventoryItemByWeight { Name = "Chicken breast", Id = Guid.NewGuid(), PricePerOunce = 2.50, Description = "Cage free" },
             new InventoryItemByWeight { Name = "Salmon", Id = Guid.NewGuid(), PricePerOunce = 2.35, Description = "Alaskan" },
             new InventoryItemByWeight { Name = "Potatoes", Id = Guid.NewGuid(), PricePerOunce = 0.69, Description = "yellow" },
             new InventoryItemByWeight { Name = "Onions", Id = Guid.NewGuid(), PricePerOunce = 0.69, Description = "sweet onions" },
             new InventoryItemByWeight { Name = "Avocados", Id = Guid.NewGuid(), PricePerOunce = 1.28, Description = "Mexican Hass" },
             new InventoryItemByWeight { Name = "Red Bell Peppers", Id = Guid.NewGuid(), PricePerOunce = 0.99, Description = "Organic" },
             new InventoryItemByWeight { Name = "Green Bell Peppers", Id = Guid.NewGuid(), PricePerOunce = 0.99, Description = "Organic" },
        };

        public static List<Product> Cart = new List<Product> { };

        public static List<InventoryItem> Search = new List<InventoryItem> { };
    }
}

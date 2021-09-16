//using Library.ShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("inventory")]
    public class InventoryController : ControllerBase
    {
        [HttpGet("GetAll")]
        public ActionResult<List<InventoryItem>> Get()
        {
            return Ok(DataContext.InventoryItems);
        }

        [HttpGet("SearchResults")]
        public ActionResult<List<InventoryItem>> GetSearch()
        {
            return Ok(DataContext.Search);
        }

        [HttpPost("Search")]
        public ActionResult<InventoryItem> Search([FromBody] InventoryItem item)
        {
            if(item.Name == "clear")
            {
                DataContext.Search.Clear();
                return item;
            }
            DataContext.Search.Add(item);
            return item;
        }
    }
}

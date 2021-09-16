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
    [Route("Cart")]
    public class ShoppingCartController : ControllerBase
    {
        [HttpGet("GetAll")]
        public ActionResult<List<Product>> Get()
        {
            return Ok(DataContext.Cart);
        }

        [HttpPost("Add")]
        public ActionResult<Product> Add([FromBody] Product SelectedProduct)
        {
            if (SelectedProduct == null)
            {
                return BadRequest();
            }

            int amountInt = 0;
            double amountDouble = 0;


            if (SelectedProduct is ProductByQuantity)
            {
                amountDouble = (SelectedProduct as ProductByQuantity).Units;
                if (DataContext.Cart.Any(i => i.Name.Equals(SelectedProduct.Name)))
                {
                    var temp = new Product();
                    temp = DataContext.Cart.FirstOrDefault(i => i.Name.Equals(SelectedProduct.Name));
                    double unitsTemp = ((temp as ProductByQuantity).Units);
                    double newAmount = unitsTemp + amountDouble;
                    DataContext.Cart.Remove(temp);
                    DataContext.Cart.Add(new ProductByQuantity
                    {
                        Name = (SelectedProduct as ProductByQuantity).Name,
                        Id = (SelectedProduct as ProductByQuantity).Id,
                        Description = (SelectedProduct as ProductByQuantity).Description,
                        UnitPrice = (SelectedProduct as ProductByQuantity).UnitPrice,
                        Units = newAmount
                    });
                }
                else
                {
                    DataContext.Cart.Add(new ProductByQuantity
                    {
                        Name = (SelectedProduct as ProductByQuantity).Name,
                        Id = (SelectedProduct as ProductByQuantity).Id,
                        Description = (SelectedProduct as ProductByQuantity).Description,
                        UnitPrice = (SelectedProduct as ProductByQuantity).UnitPrice,
                        Units = amountDouble
                    });
                }
            }
            else if (SelectedProduct is ProductByWeight)
            {
                amountDouble = (SelectedProduct as ProductByWeight).Ounces;
                if (amountDouble == 0)
                {
                    amountDouble = amountInt;
                }
                if (DataContext.Cart.Any(i => i.Name.Equals(SelectedProduct.Name)))
                {
                    var temp = new Product();
                    temp = DataContext.Cart.FirstOrDefault(i => i.Name.Equals(SelectedProduct.Name));
                    double ouncesTemp = ((temp as ProductByWeight).Ounces);
                    double newAmount = ouncesTemp + amountDouble;
                    DataContext.Cart.Remove(temp);
                    DataContext.Cart.Add(new ProductByWeight
                    {
                        Name = (SelectedProduct as ProductByWeight).Name,
                        Id = (SelectedProduct as ProductByWeight).Id,
                        Description = (SelectedProduct as ProductByWeight).Description,
                        PricePerOunce = (SelectedProduct as ProductByWeight).PricePerOunce,
                        Ounces = newAmount
                    });
                }
                else
                {
                    DataContext.Cart.Add(new ProductByWeight
                    {
                        Name = (SelectedProduct as ProductByWeight).Name,
                        Id = (SelectedProduct as ProductByWeight).Id,
                        Description = (SelectedProduct as ProductByWeight).Description,
                        PricePerOunce = (SelectedProduct as ProductByWeight).PricePerOunce,
                        Ounces = amountDouble
                    });
                }
            }

            return Ok(SelectedProduct);
        }

        [HttpPost("Remove")]
        public ActionResult<Product> Remove([FromBody] Product SelectedProductC)
        {
            int amountInt = 0;
            double amountDouble = 0;
            var temp = new Product();
            temp = DataContext.Cart.FirstOrDefault(i => i.Name.Equals(SelectedProductC.Name));

            if (SelectedProductC is ProductByQuantity)
            {
                if ((temp as ProductByQuantity).Units < (SelectedProductC as ProductByQuantity).Units)
                {
                    return null;
                }
                else if ((SelectedProductC as ProductByQuantity).Units == (temp as ProductByQuantity).Units)
                {
                    DataContext.Cart.Remove(temp);
                }
                else
                {

                    double unitsTemp = ((temp as ProductByQuantity).Units);
                    double newAmount = unitsTemp - (SelectedProductC as ProductByQuantity).Units;
                    DataContext.Cart.Remove(temp);
                    DataContext.Cart.Add(new ProductByQuantity
                    {
                        Name = (SelectedProductC as ProductByQuantity).Name,
                        Id = (SelectedProductC as ProductByQuantity).Id,
                        Description = (SelectedProductC as ProductByQuantity).Description,
                        UnitPrice = (SelectedProductC as ProductByQuantity).UnitPrice,
                        Units = newAmount
                    });
                }
            }
            else if (SelectedProductC is ProductByWeight)
            {
                if (amountDouble == 0)
                {
                    amountDouble = amountInt;
                }
                if ((temp as ProductByWeight).Ounces < (SelectedProductC as ProductByWeight).Ounces)
                {
                    return null;
                }
                else if ((SelectedProductC as ProductByWeight).Ounces == (temp as ProductByWeight).Ounces)
                {
                    DataContext.Cart.Remove(temp);
                }
                else
                {
                    double ouncesTemp = ((temp as ProductByWeight).Ounces);
                    double newAmount = ouncesTemp - (SelectedProductC as ProductByWeight).Ounces;
                    DataContext.Cart.Remove(temp);
                    DataContext.Cart.Add(new ProductByWeight
                    {
                        Name = (SelectedProductC as ProductByWeight).Name,
                        Id = (SelectedProductC as ProductByWeight).Id,
                        Description = (SelectedProductC as ProductByWeight).Description,
                        PricePerOunce = (SelectedProductC as ProductByWeight).PricePerOunce,
                        Ounces = newAmount
                    });
                }
            }

            return SelectedProductC;
        }

        [HttpGet("Clear")]
        public ActionResult<List<Product>> Clear()
        {
            (DataContext.Cart).Clear();
            return Ok(DataContext.Cart);
        }
    }
}

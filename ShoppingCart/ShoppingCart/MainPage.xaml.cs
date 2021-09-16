using ShoppingCart.Dialogs;
using ShoppingCart.ViewModels;
using ShoppingCart.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ShoppingCart
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            var handler = new WebRequestHandler();
            var InventoryItems = JsonConvert.DeserializeObject<List<InventoryItem>>(handler.Get("http://localhost:58558/inventory/getall").Result);
            var ShoppingCartItems = JsonConvert.DeserializeObject<List<Product>>(handler.Get("http://localhost:58558/Cart/getall").Result);
            var context = DataContext as MainViewModel;

            InventoryItems.ForEach(context.Inventory.Add);
            ShoppingCartItems.ForEach(context.Cart.Add);

            context.GetWindowI(context.currentPageI);
            context.GetWindowC(context.currentPageC);

        }

        private async void AddToCart(object sender, RoutedEventArgs e)
        {
            var diag = new AddDialog((DataContext as MainViewModel).ProductType());
            var result = await diag.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {
                var amount = diag.AmountA;
                if (diag.Conditional)
                {
                    var productTemp = (DataContext as MainViewModel).SelectedProduct;
                    int amountInt = 0;
                    double amountDouble = 0;

                    if (productTemp is InventoryItemByQuantity)
                    {
                        try { amountInt = int.Parse(amount); }
                        catch { return; }
                        var productForAPI = new ProductByQuantity();
                        productForAPI.Name = (productTemp as InventoryItemByQuantity).Name;
                        productForAPI.Id = (productTemp as InventoryItemByQuantity).Id;
                        productForAPI.Description = (productTemp as InventoryItemByQuantity).Description;
                        productForAPI.UnitPrice = (productTemp as InventoryItemByQuantity).UnitPrice;
                        productForAPI.Units = amountInt;
                        productForAPI = JsonConvert.DeserializeObject<ProductByQuantity>(await new WebRequestHandler().Post("http://localhost:58558/Cart/Add", productForAPI));
                    }
                    else if (productTemp is InventoryItemByWeight)
                    {
                        try { amountDouble = Convert.ToDouble(amount); }
                        catch { return; }
                        if (amountDouble == 0)
                        {
                            amountDouble = amountInt;
                        }
                        var productForAPI = new ProductByWeight();
                        productForAPI.Name = (productTemp as InventoryItemByWeight).Name;
                        productForAPI.Id = (productTemp as InventoryItemByWeight).Id;
                        productForAPI.Description = (productTemp as InventoryItemByWeight).Description;
                        productForAPI.PricePerOunce = (productTemp as InventoryItemByWeight).PricePerOunce;
                        productForAPI.Ounces = amountDouble;
                        productForAPI = JsonConvert.DeserializeObject<ProductByWeight>(await new WebRequestHandler().Post("http://localhost:58558/Cart/Add", productForAPI));
                    }

                    (DataContext as MainViewModel).AddToCart(amount);
                }
            }
        }

        private async void RemoveFromCart(object sender, RoutedEventArgs e)
        {
            var diag = new RemoveDialog((DataContext as MainViewModel).ProductCType());
            var result = await diag.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var amount = diag.AmountR;
                if (diag.Conditional)
                {
                    var productTemp = (DataContext as MainViewModel).SelectedProductC;
                    int amountInt = 0;
                    double amountDouble = 0;

                    if (productTemp is ProductByQuantity)
                    {
                        try { amountInt = int.Parse(amount); }
                        catch { return; }
                        var productForAPI = new ProductByQuantity();
                        productForAPI.Name = (productTemp as ProductByQuantity).Name;
                        productForAPI.Id = (productTemp as ProductByQuantity).Id;
                        productForAPI.Description = (productTemp as ProductByQuantity).Description;
                        productForAPI.UnitPrice = (productTemp as ProductByQuantity).UnitPrice;
                        productForAPI.Units = amountInt;
                        productTemp = JsonConvert.DeserializeObject<Product>(await new WebRequestHandler().Post("http://localhost:58558/Cart/Remove", productForAPI));
                    }
                    else if (productTemp is ProductByWeight)
                    {
                        try { amountDouble = Convert.ToDouble(amount); }
                        catch { return; }
                        if (amountDouble == 0)
                        {
                            amountDouble = amountInt;
                        }
                        var productForAPI = new ProductByWeight();
                        productForAPI.Name = (productTemp as ProductByWeight).Name;
                        productForAPI.Id = (productTemp as ProductByWeight).Id;
                        productForAPI.Description = (productTemp as ProductByWeight).Description;
                        productForAPI.PricePerOunce = (productTemp as ProductByWeight).PricePerOunce;
                        productForAPI.Ounces = amountDouble;
                        productTemp = JsonConvert.DeserializeObject<Product>(await new WebRequestHandler().Post("http://localhost:58558/Cart/Remove", productForAPI));
                    }
                    (DataContext as MainViewModel).RemoveFromCart(amount);
                }
            }
        }

        private async void Search(object sender, RoutedEventArgs e)
        {
            var clear_item = new InventoryItem();
            clear_item.Name = "clear";
            var inventoryClear = JsonConvert.DeserializeObject<InventoryItem>(await new WebRequestHandler().Post("http://localhost:58558/inventory/Search", clear_item));

            InventoryItem item;

            (DataContext as MainViewModel).InventoryWindow.Clear();

            for (int i = 0; i < (DataContext as MainViewModel).Inventory.Count; i++)
            {
                item = (DataContext as MainViewModel).Search((DataContext as MainViewModel).Query, i);

                if(item.Name != null)
                {
                    var inventoryTemp = JsonConvert.DeserializeObject<InventoryItem>(await new WebRequestHandler().Post("http://localhost:58558/inventory/Search", item));
                }

            }

            var items = JsonConvert.DeserializeObject<List<InventoryItem>>(new WebRequestHandler().Get("http://localhost:58558/inventory/SearchResults").Result);
            (DataContext as MainViewModel).InventoryWindow.Clear();
            items.ForEach((DataContext as MainViewModel).InventoryWindow.Add);

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).GetWindowI((DataContext as MainViewModel).currentPageI);
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            var cartClear = JsonConvert.DeserializeObject<List<Product>>(new WebRequestHandler().Get("http://localhost:58558/Cart/Clear").Result);
            (DataContext as MainViewModel).ClearCart();
        }

        private async void Checkout(object sender, RoutedEventArgs e)
        {
            var diag = new ReceiptDialog((DataContext as MainViewModel).Receipt());
            await diag.ShowAsync();
        }

        private void PreviousInventory(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).PreviousInventory();
        }

        private void PreviousCart(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).PreviousCart();
        }

        private void NextInventory(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).InventoryNext();
        }

        private void NextCart(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).NextCart();
        }
    }
}

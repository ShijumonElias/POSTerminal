using System;
using System.Collections.Generic;
namespace PointOfSaleTerminal
{
    public struct Product
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int VolumeQuantity { get; set; }
        public decimal VolumePrice { get; set; }
        
        // Get the price for the selected product from price settings
        public decimal GetPrice(int quantity)
        {
            decimal price = 0;
            Product product = this;

            if (quantity < 0)
            {
                throw new ArgumentException("Producy quantity must be a positive integer.");
            }

            if (quantity < product.VolumeQuantity || product.VolumeQuantity == 0)
            {
                price = product.UnitPrice * quantity;
            }
            else
            {
                int volumeCount = quantity / product.VolumeQuantity;
                int unitCount = quantity % product.VolumeQuantity;
                price = volumeCount * product.VolumePrice + unitCount * product.UnitPrice;
            }

            return price;
        }
    }

    public class POSTerminal
    {
        private Dictionary<string, Product> _ProdPrices = new Dictionary<string, Product>();
        private List<(string productCode, int quantity)> _shopCart = new List<(string, int)>();

        //Set unit price  and volume price for selected product
        public void SetPricing(string productCode, decimal unitPrice, int volumeQuantity = 0, decimal volumePrice = 0)
        {
            _ProdPrices[productCode] = new Product
            {
                Name = productCode,
                UnitPrice = unitPrice,
                VolumeQuantity = volumeQuantity,
                VolumePrice = volumePrice
            };
        }

        //Scan and add products to shoppinf cart
        public void Scan(string productCode)
        {
            if (!_ProdPrices.ContainsKey(productCode))
            {
                throw new ArgumentException("Invalid product code.");
            }

            var item = _shopCart.Find(x => x.productCode == productCode);
            if (item.Equals(default((string, int))))
            {
                _shopCart.Add((productCode, 1));
            }
            else
            {
                _shopCart[_shopCart.IndexOf(item)] = (productCode, item.quantity + 1);
            }
        }

        //Calculate the total price for the items in the shopping cart
        public decimal CalculateTotal()
        {
            decimal total = 0;

            foreach ((string productCode, int quantity) in _shopCart)
            {
                Product product = _ProdPrices[productCode];
                total += product.GetPrice(quantity);
            }

            return total;
        }
        // Clear the items in the shopping Cart
        public void EmptyCart()
        {
            _shopCart.Clear();
        }
    }
}
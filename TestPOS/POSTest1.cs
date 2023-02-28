using PointOfSale;
using Xunit;

namespace TestPOS
{
    public class CheckoutTests
    {
        [Fact]
        public void TestTotalPriceForPerUnitPricing()
        {
            // Arrange
            var checkout = new Checkout();
            checkout.SetPricing("A", 1.25m);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.Equal(3.75m, totalPrice);
        }

        [Fact]
        public void TestTotalPriceForVolumePricing()
        {
            // Arrange
            var checkout = new Checkout();
            checkout.SetPricing("A", 1.25m);
            checkout.SetVolumePricing("A", 3, 3m);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.Equal(3m, totalPrice);
        }

        [Fact]
        public void TestTotalPriceForMixedPricing()
        {
            // Arrange
            var checkout = new Checkout();
            checkout.SetPricing("A", 1.25m);
            checkout.SetVolumePricing("A", 3, 3m);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.Equal(4.25m, totalPrice);
        }

        [Fact]
        public void TestTotalPriceForMultipleItems()
        {
            // Arrange
            var checkout = new Checkout();
            checkout.SetPricing("A", 1.25m);
            checkout.SetVolumePricing("A", 3, 3m);
            checkout.SetPricing("B", 4.99m);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.Equal(7.24m, totalPrice);
        }
    }
}
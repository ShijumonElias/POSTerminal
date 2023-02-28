using PointOfSaleTerminal;
using System.Collections.Generic;
using Xunit;

namespace PointOfSaleTerminal.Tests
{
    public class PointOfSaleTerminalTests
    {
        [Fact]
        public void Test_TotalPrice_For_SingleProd()
        {
            // Arrange
            //Create terminal instance and set product proices
            POSTerminal terminal = new POSTerminal();
            terminal.SetPricing("A", 1.25m);

            // Act
            terminal.Scan("A");
            var totalPrice = terminal.CalculateTotal();

            // Assert
            Assert.Equal(1.25m, totalPrice);
        }

        [Fact]
        public void Test_TotalPrice_For_VolumePricing()
        {
            // Arrange
            var terminal = new POSTerminal();
            terminal.SetPricing("A",1.25m, 3, 3);

            // Act
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("A");
            var totalPrice = terminal.CalculateTotal();

            // Assert
            Assert.Equal(3m, totalPrice);
        }

        [Fact]
        public void Test_TotalPrice_For_MultipleItems()
        {
            // Arrange
            var terminal = new POSTerminal();
            terminal.EmptyCart();
            terminal.SetPricing("A",1.25m, 3, 3);
            terminal.SetPricing("B", 4.25m);

            // Act
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("B");
            var totalPrice = terminal.CalculateTotal();

            // Assert
            Assert.Equal(7.24m, totalPrice);
        }

        [Fact]
        public void Test_TotalPrice_For_Volume_And_Single()
        {
            // Arrange
            var terminal = new POSTerminal();
            terminal.EmptyCart();
            terminal.SetPricing("A",1.25m, 3, 3);

            // Act
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("A");
            terminal.Scan("A");
            var totalPrice = terminal.CalculateTotal();

            // Assert
            Assert.Equal(4.25m + 1.25m, totalPrice);
        }

        [Fact]
        public void Test_TotalPrice_For_UnknownProduct()
        {
            // Arrange
            var terminal = new POSTerminal();
            terminal.EmptyCart();
            // Act
            terminal.Scan("C");
            var totalPrice = terminal.CalculateTotal();

            // Assert
            Assert.Equal(0m, totalPrice);
        }
        [Fact]
        public void Test_TotalPrice_For_Purchase1()
        {
            // Arrange
            var terminal = new POSTerminal();
            terminal.EmptyCart();
            terminal.SetPricing("A", 1.25m, 3, 3);
            terminal.SetPricing("B", 4.25m);
            terminal.SetPricing("C",1,6,5);
            terminal.SetPricing("D", 0.75m);

            // Act
            terminal.Scan("A");
            terminal.Scan("B");
            terminal.Scan("C");
            terminal.Scan("D");
            terminal.Scan("A");
            terminal.Scan("B");
            terminal.Scan("A");

            var totalPrice = terminal.CalculateTotal();

            // Assert
            Assert.Equal(13.25m, totalPrice);
        }
        [Fact]
        public void Test_TotalPrice_For_Purchase2()
        {
            // Arrange
            var terminal = new POSTerminal();
            terminal.EmptyCart();
            terminal.SetPricing("A", 1.25m, 3, 3);
            terminal.SetPricing("B", 4.25m);
            terminal.SetPricing("C", 1, 6, 5);
            terminal.SetPricing("D", 0.75m);

            // Act
            terminal.Scan("C");
            terminal.Scan("C");
            terminal.Scan("C");
            terminal.Scan("C");
            terminal.Scan("C");
            terminal.Scan("C");
            terminal.Scan("C");

            var totalPrice = terminal.CalculateTotal();

            // Assert
            Assert.Equal(6, totalPrice);
        }
        [Fact]
        public void Test_TotalPrice_For_Purchase3()
        {
            // Arrange
            var terminal = new POSTerminal();
            terminal.EmptyCart();
            terminal.SetPricing("A", 1.25m, 3, 3);
            terminal.SetPricing("B", 4.25m);
            terminal.SetPricing("C", 1, 6, 5);
            terminal.SetPricing("D", 0.75m);

            // Act
            terminal.Scan("A");
            terminal.Scan("B");
            terminal.Scan("C");
            terminal.Scan("D");
           
            var totalPrice = terminal.CalculateTotal();

            // Assert
            Assert.Equal(7.25m, totalPrice);
        }
    }
}
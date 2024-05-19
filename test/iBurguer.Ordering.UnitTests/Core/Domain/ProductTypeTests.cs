using FluentAssertions;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.UnitTests.Core.Domain;

public class ProductTypeTests
{
    [Fact]
    public void ShouldCreateAValidInstanceOfMainDishProductType()
    {
        // Arrange & Act
        var status = ProductType.MainDish;

        // Assert
        status.Value.Should().Be(1);
        status.Name.Should().Be("MainDish");
    }

    [Fact]
    public void ShouldCreateAValidInstanceOfSideDishProductType()
    {
        // Arrange & Act
        var status = ProductType.SideDish;

        // Assert
        status.Value.Should().Be(2);
        status.Name.Should().Be("SideDish");
    }
    
    [Fact]
    public void ShouldCreateAValidInstanceOfDessertProductType()
    {
        // Arrange & Act
        var status = ProductType.Dessert;

        // Assert
        status.Value.Should().Be(4);
        status.Name.Should().Be("Dessert");
    }
    
    [Fact]
    public void ShouldCreateAValidInstanceOfDrinkProductType()
    {
        // Arrange & Act
        var status = ProductType.Drink;

        // Assert
        status.Value.Should().Be(3);
        status.Name.Should().Be("Drink");
    }
}
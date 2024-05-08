using Ardalis.SmartEnum;

namespace iBurguer.Ordering.Core.Domain;

public sealed class ProductType : SmartEnum<ProductType>
{
    public static readonly ProductType MainDish = new("MainDish", 1);
    public static readonly ProductType SideDish = new("SideDish", 2);
    public static readonly ProductType Drink = new("Drink", 3);
    public static readonly ProductType Dessert = new("Dessert", 4);

    private ProductType(string name, int value) : base(name, value) { }
}
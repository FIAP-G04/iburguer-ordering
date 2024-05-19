using AutoFixture.Kernel;

namespace iBurguer.Ordering.UnitTests.Util;

public class UShortSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (!(request is Type t) || t != typeof(ushort))
            return new NoSpecimen();

        var random = new Random();
        return (ushort)random.Next(1, ushort.MaxValue);
    }
}
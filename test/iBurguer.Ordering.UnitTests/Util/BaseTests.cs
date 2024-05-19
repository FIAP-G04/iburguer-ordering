using AutoFixture;

namespace iBurguer.Ordering.UnitTests.Util;

public class BaseTests
{
    private readonly Fixture _fixture;
    
    public BaseTests()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new UShortSpecimenBuilder());
    }

    public Fixture Fake => _fixture;
}
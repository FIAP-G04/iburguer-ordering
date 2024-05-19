using AutoFixture.Xunit2;
using FluentAssertions;
using static iBurguer.Ordering.Core.Exceptions;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.UnitTests.Util;

namespace iBurguer.Ordering.UnitTests.Core.Domain;

public class PickupCodeTests : BaseTests
{
    [Theory, AutoData]
    public void ShouldCreateValidPickupCode(string expectedCode)
    {
        // Act
        var pickupCode = new PickupCode(expectedCode);

        // Assert
        pickupCode.Code.Should().Be(expectedCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowExceptionInvaliPickupCode(string invalidCode)
    {
        // Act & Assert
        Assert.Throws<ThePickupCodeCannotBeEmptyOrNullException>(() => new PickupCode(invalidCode));
    }

    [Fact]
    public void ShouldGeneratePickupCodeWithCorrectLength()
    {
        // Act
        var pickupCode = PickupCode.Generate();

        // Assert
        pickupCode.Code.Length.Should().Be(6);
    }

    [Fact]
    public void ShouldUniqueCodes()
    {
        // Act
        var pickupCode1 = PickupCode.Generate();
        var pickupCode2 = PickupCode.Generate();

        // Assert
        pickupCode1.Code.Should().NotBe(pickupCode2.Code);
    }

    [Fact]
    public void ShouldReturnsOnlyAlphanumericCodes()
    {
        // Act
        var pickupCode = PickupCode.Generate();

        // Assert
        pickupCode.Code.Should().MatchRegex("^[A-Z0-9]+$");
    }
}
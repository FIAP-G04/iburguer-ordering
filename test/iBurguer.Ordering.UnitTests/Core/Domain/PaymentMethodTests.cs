using FluentAssertions;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.UnitTests.Core.Domain;

public class PaymentMethodTests
{
    [Fact]
    public void ShouldCreateAValidInstanceOfQrCodePaymentMethod()
    {
        // Arrange & Act
        var status = PaymentMethod.QrCode;

        // Assert
        status.Value.Should().Be(1);
        status.Name.Should().Be("QRCode");
    }
}
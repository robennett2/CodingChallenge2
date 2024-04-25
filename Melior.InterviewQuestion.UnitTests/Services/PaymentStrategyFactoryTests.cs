using FluentAssertions;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Moq;
using Moq.AutoMock;

namespace Melior.InterviewQuestion.UnitTests;

public class PaymentStrategyFactoryTests
{
    private readonly AutoMocker _mocker = new(MockBehavior.Strict);
    private readonly PaymentStrategyFactory _sut;
    
    public PaymentStrategyFactoryTests()
    {
        _sut = _mocker.CreateInstance<PaymentStrategyFactory>();
    }
    
    [Fact]
    public void GetPaymentStrategy_ForBacs_ReturnsBacsPaymentStrategy()
    {
        // Arrange
        var paymentScheme = PaymentScheme.Bacs;
        
        // Act
        var result = _sut.GetPaymentStrategy(paymentScheme);
        
        // Assert
        result.Should().BeOfType<BacsPaymentStrategy>();
    }
    
    [Fact]
    public void GetPaymentStrategy_ForFasterPayments_ReturnsFasterPaymentsPaymentStrategy()
    {
        // Arrange
        var paymentScheme = PaymentScheme.FasterPayments;
        
        // Act
        var result = _sut.GetPaymentStrategy(paymentScheme);
        
        // Assert
        result.Should().BeOfType<FasterPaymentsPaymentStrategy>();
    }
    
    [Fact]
    public void GetPaymentStrategy_ForChaps_ReturnsChapsPaymentStrategy()
    {
        // Arrange
        var paymentScheme = PaymentScheme.Chaps;
        
        // Act
        var result = _sut.GetPaymentStrategy(paymentScheme);
        
        // Assert
        result.Should().BeOfType<ChapsPaymentStrategy>();
    }
    
    [Fact]
    public void GetPaymentStrategy_ForUnknownPaymentScheme_ThrowsException()
    {
        // Arrange
        var paymentScheme = (PaymentScheme)int.MaxValue;
        
        // Act
        Action act = () => _sut.GetPaymentStrategy(paymentScheme);
        
        // Assert
        act
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Cannot resolve strategy for payment scheme. (Parameter 'paymentScheme')\nActual value was 2147483647.");
    }
}
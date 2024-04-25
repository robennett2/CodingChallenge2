using FluentAssertions;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Moq;
using Moq.AutoMock;

namespace Melior.InterviewQuestion.UnitTests;

public class BacsPaymentStrategyTests
{
    private readonly AutoMocker _mocker = new(MockBehavior.Strict);
    private readonly BacsPaymentStrategy _sut;
    
    public BacsPaymentStrategyTests()
    {
        _sut = _mocker.CreateInstance<BacsPaymentStrategy>();
    }
    
    [Fact]
    public void MakePayment_ThatWasSuccessful_ReturnsSuccess()
    {
        // Arrange
        var debtorAccount = new Account()
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
        };
        
        var request = new MakePaymentRequest();
        
        // Act
        var result = _sut.MakePayment(request, debtorAccount);
        
        // Assert
        result.Success.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(AllowedPaymentSchemes.FasterPayments)]
    [InlineData(AllowedPaymentSchemes.Chaps)]
    public void MakePayment_ForAccountThatDoesNotSupportBacs_ReturnsFailure(AllowedPaymentSchemes allowedPaymentSchemes)
    {
        // Arrange
        var debtorAccount = new Account()
        {
            AllowedPaymentSchemes = allowedPaymentSchemes
        };
        
        var request = new MakePaymentRequest();
        
        // Act
        var result = _sut.MakePayment(request, debtorAccount);
        
        // Assert
        result.Success.Should().BeFalse();
    }
}
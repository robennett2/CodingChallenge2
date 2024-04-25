using FluentAssertions;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Moq;
using Moq.AutoMock;

namespace Melior.InterviewQuestion.UnitTests;

public class FasterPaymentsPaymentStrategyTests
{
    private readonly AutoMocker _mocker = new(MockBehavior.Strict);
    private readonly FasterPaymentsPaymentStrategy _sut;
    
    public FasterPaymentsPaymentStrategyTests()
    {
        _sut = _mocker.CreateInstance<FasterPaymentsPaymentStrategy>();
    }
    
    [Fact]
    public void MakePayment_ThatWasSuccessful_ReturnsSuccess()
    {
        // Arrange
        var debtorAccount = new Account()
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Status = AccountStatus.Live
        };
        
        var request = new MakePaymentRequest();
        
        // Act
        var result = _sut.MakePayment(request, debtorAccount);
        
        // Assert
        result.Success.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(AllowedPaymentSchemes.Bacs)]
    [InlineData(AllowedPaymentSchemes.Chaps)]
    public void MakePayment_ForAccountThatDoesNotSupportFasterPayments_ReturnsFailure(AllowedPaymentSchemes allowedPaymentSchemes)
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
    
    
    [Fact]
    public void MakePayment_ForAccountWithInsufficientBalance_ReturnsFailure()
    {
        // Arrange
        var debtorAccount = new Account()
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Balance = 0
        };
        
        var request = new MakePaymentRequest()
        {
            Amount = 100
        };
        
        // Act
        var result = _sut.MakePayment(request, debtorAccount);
        
        // Assert
        result.Success.Should().BeFalse();
    }
}
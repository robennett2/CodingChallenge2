using FluentAssertions;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Moq;
using Moq.AutoMock;

namespace Melior.InterviewQuestion.UnitTests;

public class ChapsPaymentStrategyTests
{
    private readonly AutoMocker _mocker = new(MockBehavior.Strict);
    private readonly ChapsPaymentStrategy _sut;
    
    public ChapsPaymentStrategyTests()
    {
        _sut = _mocker.CreateInstance<ChapsPaymentStrategy>();
    }
    
    [Fact]
    public void MakePayment_ThatWasSuccessful_ReturnsSuccess()
    {
        // Arrange
        var debtorAccount = new Account()
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Status = AccountStatus.Live
        };
        
        var request = new MakePaymentRequest();
        
        // Act
        var result = _sut.MakePayment(request, debtorAccount);
        
        // Assert
        result.Success.Should().BeTrue();
    }
    
    [Fact]
    public void MakePayment_ForAccountThatDoesNotSupportChaps_ReturnsFailure()
    {
        // Arrange
        var debtorAccount = new Account()
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
        };
        
        var request = new MakePaymentRequest();
        
        // Act
        var result = _sut.MakePayment(request, debtorAccount);
        
        // Assert
        result.Success.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(AccountStatus.Disabled)]
    [InlineData(AccountStatus.InboundPaymentsOnly)]
    public void MakePayment_ForInactiveAccount_ReturnsFailure(AccountStatus accountStatus)
    {
        // Arrange
        var debtorAccount = new Account()
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Status = accountStatus
        };
        
        var request = new MakePaymentRequest();
        
        // Act
        var result = _sut.MakePayment(request, debtorAccount);
        
        // Assert
        result.Success.Should().BeFalse();
    }
}
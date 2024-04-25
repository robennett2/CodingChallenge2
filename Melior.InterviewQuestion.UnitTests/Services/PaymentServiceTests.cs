using FluentAssertions;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Moq;
using Moq.AutoMock;

namespace Melior.InterviewQuestion.UnitTests;

public class PaymentServiceTests
{
    private readonly AutoMocker _mocker = new(MockBehavior.Strict);
    private readonly PaymentService _sut;
    private Mock<IPaymentStrategyFactory> PaymentStrategyFactoryMock =>_mocker.GetMock<IPaymentStrategyFactory>();
    private Mock<IAccountRepository> AccountRepositoryMock => _mocker.GetMock<IAccountRepository>();
    private Mock<IPaymentStrategy> PaymentStrategyMock => _mocker.GetMock<IPaymentStrategy>();

    public PaymentServiceTests()
    {
        _sut = _mocker.CreateInstance<PaymentService>();
    }
    
    [Fact]
    public void MakePayment_ThatWasSuccessful_UpdatesAccountBalance()
    {
        // Arrange
        var debtorAccountNumber = "123";
        var paymentScheme = PaymentScheme.Bacs;
        var debitAmount = 90M;
        var request = new MakePaymentRequest()
        {
            DebtorAccountNumber = debtorAccountNumber,
            PaymentScheme = paymentScheme,
            Amount = debitAmount
        };

        var expectedAccount = new Account()
        {
            Balance = 100M
        };
        
        AccountRepositoryMock
            .Setup(x => x.GetAccount(request.DebtorAccountNumber))
            .Returns(expectedAccount);
        
        PaymentStrategyFactoryMock
            .Setup(x => x.GetPaymentStrategy(request.PaymentScheme))
            .Returns(PaymentStrategyMock.Object);
        
        AccountRepositoryMock.Setup(x => x.UpdateAccount(expectedAccount));
        
        PaymentStrategyMock.Setup(x => x.MakePayment(request, expectedAccount))
            .Returns(new MakePaymentResult()
            {
                Success = true
            });
        
        // Act
        var result = _sut.MakePayment(request);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        expectedAccount.Balance.Should().Be(10M);
        
        AccountRepositoryMock.Verify(x => x.GetAccount(debtorAccountNumber), Times.Once);
        PaymentStrategyFactoryMock.Verify(x => x.GetPaymentStrategy(paymentScheme), Times.Once);
        AccountRepositoryMock.Verify(x => x.UpdateAccount(expectedAccount), Times.Once);
    }
    
    [Fact]
    public void MakePayment_AccountNotFound_ReturnsUnsuccessful()
    {
        // Arrange
        var debtorAccountNumber = "123";
        var paymentScheme = PaymentScheme.Bacs;
        var debitAmount = 90M;
        var request = new MakePaymentRequest()
        {
            DebtorAccountNumber = debtorAccountNumber,
            PaymentScheme = paymentScheme,
            Amount = debitAmount
        };
        
        AccountRepositoryMock
            .Setup(x => x.GetAccount(request.DebtorAccountNumber))
            .Returns(null as Account);
        
        // Act
        var result = _sut.MakePayment(request);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        
        AccountRepositoryMock.Verify(x => x.GetAccount(debtorAccountNumber), Times.Once);
        PaymentStrategyFactoryMock.Verify(x => x.GetPaymentStrategy(It.IsAny<PaymentScheme>()), Times.Never);
        AccountRepositoryMock.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }
    
    [Fact]
    public void MakePayment_ThatWasNotSuccessful_DoesNotUpdateAccountBalance()
    {
        // Arrange
        var debtorAccountNumber = "123";
        var paymentScheme = PaymentScheme.Bacs;
        var debitAmount = 90M;
        var request = new MakePaymentRequest()
        {
            DebtorAccountNumber = debtorAccountNumber,
            PaymentScheme = paymentScheme,
            Amount = debitAmount
        };

        var expectedAccount = new Account()
        {
            Balance = 100M
        };
        
        AccountRepositoryMock
            .Setup(x => x.GetAccount(request.DebtorAccountNumber))
            .Returns(expectedAccount);
        
        PaymentStrategyFactoryMock
            .Setup(x => x.GetPaymentStrategy(request.PaymentScheme))
            .Returns(PaymentStrategyMock.Object);
        
        PaymentStrategyMock.Setup(x => x.MakePayment(request, expectedAccount))
            .Returns(new MakePaymentResult()
            {
                Success = false
            });
        
        AccountRepositoryMock.Setup(x => x.UpdateAccount(expectedAccount));
        
        // Act
        var result = _sut.MakePayment(request);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        expectedAccount.Balance.Should().Be(100M);
        
        AccountRepositoryMock.Verify(x => x.GetAccount(debtorAccountNumber), Times.Once);
        PaymentStrategyFactoryMock.Verify(x => x.GetPaymentStrategy(paymentScheme), Times.Once);
        AccountRepositoryMock.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }
}
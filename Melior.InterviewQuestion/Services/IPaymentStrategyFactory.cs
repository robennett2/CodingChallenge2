using System.Threading;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;

public interface IPaymentStrategyFactory
{
    IPaymentStrategy GetPaymentStrategy(PaymentScheme paymentScheme);
}
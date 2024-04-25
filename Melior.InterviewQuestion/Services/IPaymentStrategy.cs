using System.Threading;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;

public interface IPaymentStrategy
{
    MakePaymentResult MakePayment(
        MakePaymentRequest request,
        Account debtorAccount);
}
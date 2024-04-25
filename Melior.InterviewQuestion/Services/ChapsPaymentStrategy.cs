using System;
using System.Threading;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;

public class ChapsPaymentStrategy : IPaymentStrategy
{
    public MakePaymentResult MakePayment(MakePaymentRequest request, Account debtorAccount)
    {
        // case PaymentScheme.Chaps:
        // if (account == null)
        // {
        //     result.Success = false;
        // }
        // else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
        // {
        //     result.Success = false;
        // }
        // else if (account.Status != AccountStatus.Live)
        // {
        //     result.Success = false;
        // }
        // break;
        throw new NotImplementedException();
    }
}
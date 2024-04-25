using System;
using System.Threading;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;

public class FasterPaymentsPaymentStrategy : IPaymentStrategy
{
    public MakePaymentResult MakePayment(MakePaymentRequest request, Account debtorAccount)
    {
        // case PaymentScheme.FasterPayments:
        // if (account == null)
        // {
        //     result.Success = false;
        // }
        // else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
        // {
        //     result.Success = false;
        // }
        // else if (account.Balance < request.Amount)
        // {
        //     result.Success = false;
        // }
        // break;
        throw new NotImplementedException();
    }
}
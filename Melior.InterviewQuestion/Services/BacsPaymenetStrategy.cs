using System;
using System.Threading;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;


public class BacsPaymenetStrategy : IPaymentStrategy
{
    public MakePaymentResult MakePayment(MakePaymentRequest request, Account debtorAccount)
    {
        // case PaymentScheme.Bacs:
        // if (account == null)
        // {
        //     result.Success = false;
        // }
        // else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
        // {
        //     result.Success = false;
        // }
        // break;
        throw new NotImplementedException();
    }
}
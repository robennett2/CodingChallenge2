using System;
using System.Threading;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;

public class ChapsPaymentStrategy : IPaymentStrategy
{
    public MakePaymentResult MakePayment(MakePaymentRequest request, Account debtorAccount)
    {

        if (!debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
        {
            return new MakePaymentResult()
            {
                Success = false
            };
        }

        if (debtorAccount.Status != AccountStatus.Live)
        {
            return new MakePaymentResult()
            {
                Success = false
            };
        }

        return new MakePaymentResult()
        {
            Success = true
        };
    }
}


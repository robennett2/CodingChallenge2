using System;
using System.Threading;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;

public class FasterPaymentsPaymentStrategy : IPaymentStrategy
{
    public MakePaymentResult MakePayment(MakePaymentRequest request, Account debtorAccount)
    {
        if (!debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
        {
            return new MakePaymentResult()
            {
                Success = false
            };
        }
        
        if (debtorAccount.Balance < request.Amount)
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
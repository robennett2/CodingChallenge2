using System;
using System.Threading;
using System.Threading.Tasks;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;


public class BacsPaymentStrategy : IPaymentStrategy
{
    public MakePaymentResult MakePayment(MakePaymentRequest request, Account debtorAccount)
    {
        if (!debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
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
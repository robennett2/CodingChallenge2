using System;
using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;

public class PaymentStrategyFactory : IPaymentStrategyFactory
{
    public IPaymentStrategy GetPaymentStrategy(PaymentScheme paymentScheme)
    {
        return paymentScheme switch
        {
            PaymentScheme.Bacs => new BacsPaymentStrategy(),
            PaymentScheme.FasterPayments => new FasterPaymentsPaymentStrategy(),
            PaymentScheme.Chaps => new ChapsPaymentStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(paymentScheme), paymentScheme, "Cannot resolve strategy for payment scheme.")
        };
    }
}
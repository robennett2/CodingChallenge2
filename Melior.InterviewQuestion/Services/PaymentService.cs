using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentStrategyFactory _paymentStrategyFactory;
        private readonly IAccountRepository _accountRepository;

        public PaymentService(IPaymentStrategyFactory paymentStrategyFactory, IAccountRepository accountRepository)
        {
            _paymentStrategyFactory = paymentStrategyFactory;
            _accountRepository = accountRepository;
        }
        
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountRepository.GetAccount(request.DebtorAccountNumber);

            if (account is null)
            {
                return new MakePaymentResult
                {
                    Success = false
                };
            }
            
            var paymentStrategy = _paymentStrategyFactory.GetPaymentStrategy(request.PaymentScheme);
            var result = paymentStrategy.MakePayment(request, account);
            
            if (result.Success is false)
            {
                return result;
            }

            account.Debit(request.Amount);
            _accountRepository.UpdateAccount(account);
            return result;
        }
    }
}

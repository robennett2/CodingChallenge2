using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Services;

public interface IAccountRepository
{
    Account? GetAccount(string accountNumber);

    void UpdateAccount(Account account);
}
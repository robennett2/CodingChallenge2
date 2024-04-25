using Melior.InterviewQuestion.Types;
using Microsoft.Extensions.Options;

namespace Melior.InterviewQuestion.Services;

public class AccountRepository : IAccountRepository
{
    private readonly AccountServiceSettings _settings;
    
    public AccountRepository(IOptionsSnapshot<AccountServiceSettings> accountServiceSettingsSnapshot)
    {
        _settings = accountServiceSettingsSnapshot.Value;
    }
    
    public Account? GetAccount(string accountNumber)
    {
        // var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];
        //
        // Account account = null;
        //
        // if (dataStoreType == "Backup")
        // {
        //     var accountDataStore = new BackupAccountDataStore();
        //     account = accountDataStore.GetAccount(request.DebtorAccountNumber);
        // }
        // else
        // {
        //     var accountDataStore = new AccountDataStore();
        //     account = accountDataStore.GetAccount(request.DebtorAccountNumber);
        // }
        throw new System.NotImplementedException();
    }

    public void UpdateAccount(Account account)
    {
        // if (dataStoreType == "Backup")
        // {
        //     var accountDataStore = new BackupAccountDataStore();
        //     accountDataStore.UpdateAccount(account);
        // }
        // else
        // {
        //     var accountDataStore = new AccountDataStore();
        //     accountDataStore.UpdateAccount(account);
        // }
        throw new System.NotImplementedException();
    }
}
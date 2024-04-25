using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Types;
using Microsoft.Extensions.Options;

namespace Melior.InterviewQuestion.Services;

public class AccountRepository : IAccountRepository
{
    private readonly IAccountDataStore _accountDataStore;
    private readonly IBackupAccountDataStore _backupAccountDataStore;
    private readonly IOptionsSnapshot<AccountRepositoryOptions> _accountServiceSettingsSnapshot;
    
    public AccountRepository(
        IAccountDataStore accountDataStore,
        IBackupAccountDataStore backupAccountDataStore,
        IOptionsSnapshot<AccountRepositoryOptions> accountServiceSettingsSnapshot)
    {
        _accountDataStore = accountDataStore;
        _backupAccountDataStore = backupAccountDataStore;
        _accountServiceSettingsSnapshot = accountServiceSettingsSnapshot;
    }
    
    public Account? GetAccount(string accountNumber)
    {
        var dataStoreType = _accountServiceSettingsSnapshot.Value.DataStoreType;
        if (dataStoreType == "Backup")
        {
            return _backupAccountDataStore.GetAccount(accountNumber);
        }
        else
        {
            return _accountDataStore.GetAccount(accountNumber);
        }
    }

    public void UpdateAccount(Account account)
    {
        if (_accountServiceSettingsSnapshot.Value.DataStoreType == "Backup")
        {
            _backupAccountDataStore.UpdateAccount(account);
        }
        else
        {
            _accountDataStore.UpdateAccount(account);
        }
    }
}
using FluentAssertions;
using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;

namespace Melior.InterviewQuestion.UnitTests;

public class AccountRepositoryTests
{
    private readonly AutoMocker _mocker = new(MockBehavior.Strict);
    private Mock<IAccountDataStore> PrimaryAccountDataStoreMock => _mocker.GetMock<IAccountDataStore>();
    private Mock<IBackupAccountDataStore> BackupAccountDataStoreMock => _mocker.GetMock<IBackupAccountDataStore>();
    private Mock<IOptionsSnapshot<AccountRepositoryOptions>> AccountRepositoryOptionsSnapshotMock => _mocker.GetMock<IOptionsSnapshot<AccountRepositoryOptions>>();
    private readonly AccountRepository _sut;

    public AccountRepositoryTests()
    {
        _sut = _mocker.CreateInstance<AccountRepository>();
    }
    
    [Fact]
    public void GetAccount_WhenDataStoreTypeIsBackup_ReturnsAccountFromBackupDataStore()
    {
        // Arrange
        var accountNumber = "123";
        var account = new Account();
        AccountRepositoryOptionsSnapshotMock
            .Setup(x => x.Value)
            .Returns(new AccountRepositoryOptions()
            {
                DataStoreType = "Backup"
            });
        BackupAccountDataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);
        
        // Act
        var result = _sut.GetAccount(accountNumber);
        
        // Assert
        result.Should().Be(account);
    }
    
    [Fact]
    public void GetAccount_WhenDataStoreTypeIsNotBackup_ReturnsAccountFromDataStore()
    {
        // Arrange
        var accountNumber = "123";
        var account = new Account();
        AccountRepositoryOptionsSnapshotMock
            .SetupGet(x => x.Value)
            .Returns(new AccountRepositoryOptions()
            {
                DataStoreType = "Primary"
            });
        PrimaryAccountDataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);
        
        // Act
        var result = _sut.GetAccount(accountNumber);
        
        // Assert
        result.Should().Be(account);
    }
    
    [Fact]
    public void UpdateAccount_WhenDataStoreTypeIsBackup_UpdatesAccountInBackupDataStore()
    {
        // Arrange
        var account = new Account();
        AccountRepositoryOptionsSnapshotMock
            .Setup(x => x.Value)
            .Returns(new AccountRepositoryOptions()
            {
                DataStoreType = "Backup"
            });
        
        BackupAccountDataStoreMock.Setup(x => x.UpdateAccount(account));
        
        // Act
        _sut.UpdateAccount(account);
        
        // Assert
        BackupAccountDataStoreMock.Verify(x => x.UpdateAccount(account), Times.Once);
    }
    
    [Fact]
    public void UpdateAccount_WhenDataStoreTypeIsNotBackup_UpdatesAccountInDataStore()
    {
        // Arrange
        var account = new Account();
        AccountRepositoryOptionsSnapshotMock
            .Setup(x => x.Value)
            .Returns(new AccountRepositoryOptions()
            {
                DataStoreType = "Primary"
            });
        
        PrimaryAccountDataStoreMock.Setup(x => x.UpdateAccount(account));
        
        // Act
        _sut.UpdateAccount(account);
        
        // Assert
        PrimaryAccountDataStoreMock.Verify(x => x.UpdateAccount(account), Times.Once);
    }
}
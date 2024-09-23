namespace Banking
{
    public class Bank
    {
        private List<BankAccount> accounts = new List<BankAccount>();
        private object _accountsLock = new object();
        public void OpenAccount(double initalBalance)
        {
            lock (_accountsLock)
            {
                int accountNumber = accounts.Count + 1;
                BankAccount newAccount = new BankAccount(accountNumber, initalBalance);
                accounts.Add(newAccount);
            }
        }
        public void CloseAccount(int accountNumber)
        {
            lock (_accountsLock)
            {
                BankAccount? accountToRemove = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (accountToRemove != null)
                {
                    accounts.Remove(accountToRemove);
                }
            }
        }
        public double GetTotalBalance()
        {
            double totalBalance = 0;
            lock (_accountsLock)
            {
                foreach (var account in accounts)
                {
                    totalBalance += account.Balance;
                }
                return totalBalance;
            }
        }
        public void Transfer(int sourceAccountNumber, int destinationAccountNumber, double amount)
        {
            lock (_accountsLock)
            {
                BankAccount? sourceAccount = accounts.Find(acc => acc.AccountNumber == sourceAccountNumber);
                BankAccount? destinatioAccount = accounts.Find(acc => acc.AccountNumber == destinationAccountNumber);
                if (sourceAccount != null && destinatioAccount != null)
                {
                    if (sourceAccount != destinatioAccount)
                    {
                        sourceAccount.TransferTo(destinatioAccount, amount);
                    }
                    else
                    {
                        throw new ArgumentException("Source and destination account cannot be the same.");
                    }
                }
                else
                {
                    throw new ArgumentException("Source or destination account not found.");
                }
            }
        }
        public double GetAccountBalance(int accountNumber)
        {
            lock (_accountsLock)
            {
                BankAccount? account = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    return account.Balance;
                }
                else
                {
                    throw new ArgumentException($"Account {accountNumber} not found");
                }
            }
        }
        public void Deposit(int accountNumber, double amount)
        {
            lock (_accountsLock)
            {
                BankAccount? account = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    account.Deposit(amount);
                }
                else
                {
                    throw new ArgumentException($"Account {accountNumber} not found");
                }
            }
        }
        public void Withdraw(int accountNumber, double amount)
        {
            lock (_accountsLock)
            {
                BankAccount? account = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    account.Withdraw(amount);
                }
                else
                {
                    throw new ArgumentException($"Account {accountNumber} not found");
                }
            }
        }
    }
}
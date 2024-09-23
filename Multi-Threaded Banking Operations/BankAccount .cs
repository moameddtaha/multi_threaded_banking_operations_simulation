using Banking.Exceptions;

namespace Banking
{
    public class BankAccount
    {
        // Properties
        public int AccountNumber { get; private set; }
        public double Balance { get; private set; }
        private object _balanceLock = new object();

        // Constructor
        public BankAccount(int accountNumber, double initalBalance)
        {
            AccountNumber = accountNumber;
            Balance = initalBalance;
        }

        //Methods

        public void Deposit(double amount)
        {
            lock (_balanceLock)
            {
                if (amount > 0)
                {
                    Balance += amount;
                }
                else
                {
                    throw new ArgumentException("The ammount to deposit cannot be less or equal to zero.");
                }
            }
        }
        public void Withdraw(double amount)
        {
            lock (_balanceLock)
            {
                if (amount <= 0)
                {
                    throw new ArgumentException("Withdraw amount must be greater than zero");
                }
                else if (Balance <= amount)
                {
                    throw new InsufficientBalanceException(AccountNumber, Balance);
                }
                else
                {
                    Balance -= amount;
                }
            }
        }
        public void TransferTo(BankAccount destination, double amount)
        {
            lock (_balanceLock)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                    destination.Deposit(amount);
                }
                else
                {
                    throw new InsufficientBalanceException(AccountNumber, Balance);
                }
            }
        }
    }
}

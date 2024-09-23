namespace Banking.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public int AccountNumber { get; }
        public double Amount { get; }
        public InsufficientBalanceException(int accountNumber, double amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
        }

    }
}
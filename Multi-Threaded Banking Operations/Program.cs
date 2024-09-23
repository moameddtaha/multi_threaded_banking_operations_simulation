using System.Diagnostics;
using Banking.Exceptions;

namespace Banking
{
    public class Program
    {
        static Stopwatch stopwatch = new Stopwatch();
        const double runTime = 60;
        public static void Main()
        {
            Console.WriteLine("Program started.");
            Bank bank = new Bank();

            for (int i = 0; i < 5; i++)
            {
                bank.OpenAccount(1000);
            }

            Thread depositThread = new Thread(() => PerformDeposits(bank));
            Thread withdrawlThread = new Thread(() => PerformWithdrawls(bank));
            Thread transfersThread = new Thread(() => PerformTransfers(bank));
            Thread monitoringThread = new Thread(() => MonitoringTransfers(bank));

            stopwatch.Start();

            depositThread.Start();
            withdrawlThread.Start();
            transfersThread.Start();
            monitoringThread.Start();

            depositThread.Join();
            withdrawlThread.Join();
            transfersThread.Join();

            monitoringThread.Interrupt();
            monitoringThread.Join();

            Console.WriteLine("All threads have completed their tasks.");
        }

        public static void PerformDeposits(Bank bank)
        {
            try
            {
                Random random = new Random();
                while (stopwatch.Elapsed.Seconds < runTime)
                {
                    int accountNumber = random.Next(1, 6);
                    double depositAmount = random.Next(1, 101);
                    try
                    {
                        bank.Deposit(accountNumber, depositAmount);

                        Console.WriteLine($"Deposited ${depositAmount} into Account {accountNumber}. Updated Balance: {bank.GetAccountBalance(accountNumber)}");

                        // Sleep for a random time.
                        Thread.Sleep(random.Next(500, 1001));
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Deposit thread was interrupted.");
            }
        }
        public static void PerformWithdrawls(Bank bank)
        {
            try
            {
                Random random = new Random();
                while (stopwatch.Elapsed.Seconds < runTime)
                {
                    int accountNumber = random.Next(1, 6);
                    double withdrawaltAmount = random.Next(1, 101);
                    try
                    {
                        bank.Withdraw(accountNumber, withdrawaltAmount);

                        Console.WriteLine($"Deposited ${withdrawaltAmount} into Account {accountNumber}. Updated Balance: {bank.GetAccountBalance(accountNumber)}");
                    }
                    catch (InsufficientBalanceException ex)
                    {
                        Console.WriteLine($"Withdrawal from Account {ex.AccountNumber} faild: Insufficient funds. Updated Balance: {bank.GetAccountBalance(accountNumber)}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    // Sleep for a random time.
                    Thread.Sleep(random.Next(500, 1001));
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Withdrawl thread was interrupted.");
            }
        }
        public static void PerformTransfers(Bank bank)
        {
            try
            {
                Random random = new Random();
                while (stopwatch.Elapsed.Seconds < runTime)
                {
                    int sourceAccountNumber = random.Next(1, 6);
                    int destinatioAccount = random.Next(1, 6);
                    double transferAmount = random.Next(1, 101);
                    try
                    {
                        bank.Transfer(sourceAccountNumber, destinatioAccount, transferAmount);

                        Console.WriteLine($"Transfered ${transferAmount} from Account {sourceAccountNumber} to Account {destinatioAccount}. Updated Balance is Source Account: {bank.GetAccountBalance(sourceAccountNumber)}. Updated Balance in Destination Account: {bank.GetAccountBalance(destinatioAccount)}");
                    }
                    catch (InsufficientBalanceException ex)
                    {
                        Console.WriteLine($"Transfer from Account {ex.AccountNumber} faild: Insufficient funds.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    // Sleep for a random time.
                    Thread.Sleep(random.Next(500, 1001));
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Transfer thread was interrupted.");
            }
        }
        public static void MonitoringTransfers(Bank bank)
        {
            try
            {
                while (true)
                {
                    double totalBalance = bank.GetTotalBalance();
                    Console.WriteLine($"Total Balance in the Bank: {totalBalance}");
                    Thread.Sleep(5000);
                }
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Monitoring thread was interrupted.");
            }
        }
    }
}
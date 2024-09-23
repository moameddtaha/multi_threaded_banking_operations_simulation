# Multi-Threaded Banking Operations Simulation

This project simulates a multi-threaded banking environment in a C# Console Application. It demonstrates fundamental concepts in concurrent programming, thread synchronization, exception handling, and realistic banking operations.

## Features:

    Bank and Account Management:
        Implemented Bank and BankAccount classes to manage accounts, including opening, closing, depositing, withdrawing, and transferring funds.
        Ensures thread safety using locking mechanisms to prevent race conditions during concurrent operations.

    Multi-Threading Simulation:
        Utilizes multiple threads for depositing, withdrawing, transferring funds, and monitoring account balances.
        Randomizes transaction amounts and timings to simulate realistic banking activities.

    Exception Handling:
        Implements custom exceptions for handling scenarios like insufficient funds during withdrawals or transfers.
        Ensures robust error handling to maintain data integrity across multiple threads.

    Thread Termination and Cleanup:
        Gracefully terminates all threads after a specified time duration, ensuring proper cleanup and displaying relevant completion messages.
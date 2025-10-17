using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    enum AccountType { Current, Savings }

    abstract class Account
    {
        public long AccountNumber { get; }
        public AccountType Type { get; }
        public decimal Balance { get; protected set; }

        protected Account(long accountNumber, AccountType type)
        {
            AccountNumber = accountNumber;
            Type = type;
            Balance = 0.0m;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            Balance += amount;
        }

        public virtual bool Withdraw(decimal amount)
        {
            if (amount <= 0) return false;
            if (amount > Balance) return false;
            Balance -= amount;
            return true;
        }

        public override string ToString() => $"{Type} - #{AccountNumber} - Balance: {Balance:C}";
    }

    class CurrentAccount : Account
    {
        public CurrentAccount(long accountNumber) : base(accountNumber, AccountType.Current) { }
    }

    class SavingsAccount : Account
    {
        public SavingsAccount(long accountNumber) : base(accountNumber, AccountType.Savings) { }
    }

    class User
    {
        public string FullName { get; }
        public string UserName { get; }
        public List<Account> Accounts { get; } = new();

        public User(string fullName, string userName)
        {
            FullName = fullName;
            UserName = userName;
        }
    }

    private static readonly List<User> users = new();
    private static readonly Random rng = new();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Fasco Bank LTD");
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. Login existing user");
            Console.WriteLine("3. Exit");
            Console.Write("Select your choice>>> ");

            var choice = ReadInt();
            Console.Clear();

            switch (choice)
            {
                case 1:
                    CreateUser();
                    break;
                case 2:
                    LoginUser();
                    break;
                case 3:
                    Console.WriteLine("Thank you for banking with us...");
                    return;
                default:
                    Console.WriteLine("Incorrect choice, try again!!!");
                    Pause();
                    break;
            }
        }
    }

    private static void CreateUser()
    {
        Console.Write("Input Fullname: ");
        var fullName = Console.ReadLine()?.Trim() ?? string.Empty;
        Console.Clear();
        Console.Write("Input Username: ");
        var userName = Console.ReadLine()?.Trim() ?? string.Empty;
        Console.Clear();

        if (string.IsNullOrWhiteSpace(userName))
        {
            Console.WriteLine("Username cannot be empty.");
            Pause();
            return;
        }

        if (users.Any(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Username already exists. Choose another username.");
            Pause();
            return;
        }

        var user = new User(fullName, userName);

        // create both current and savings accounts
        var currentAccountNumber = GenerateUniqueAccountNumber();
        var savingsAccountNumber = GenerateUniqueAccountNumber();

        user.Accounts.Add(new CurrentAccount(currentAccountNumber));
        user.Accounts.Add(new SavingsAccount(savingsAccountNumber));

        users.Add(user);

        Console.WriteLine("User and accounts created successfully!");
        Console.WriteLine($"Current Account: {currentAccountNumber}");
        Console.WriteLine($"Savings Account: {savingsAccountNumber}");
        Pause();
    }

    private static void LoginUser()
    {
        Console.Write("Enter username: ");
        var input = Console.ReadLine()?.Trim();
        Console.Clear();

        var user = users.FirstOrDefault(u => u.UserName.Equals(input, StringComparison.OrdinalIgnoreCase));
        if (user == null)
        {
            Console.WriteLine("User not found. Create an account first!");
            Pause();
            return;
        }

        UserMenu(user);
    }

    private static void UserMenu(User user)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {user.FullName} ({user.UserName})");
            Console.WriteLine("Your accounts:");
            for (int i = 0; i < user.Accounts.Count; i++)
                Console.WriteLine($"{i + 1}. {user.Accounts[i]}");

            Console.WriteLine($"{user.Accounts.Count + 1}. Logout");
            Console.Write("Select account>>> ");

            var sel = ReadInt();
            if (sel >= 1 && sel <= user.Accounts.Count)
            {
                AccountMenu(user.Accounts[sel - 1]);
            }
            else if (sel == user.Accounts.Count + 1)
            {
                Console.Clear();
                return;
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                Pause();
            }
        }
    }

    private static void AccountMenu(Account account)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Account: {account.Type} #{account.AccountNumber}");
            Console.WriteLine($"Balance: {account.Balance:C}");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit money");
            Console.WriteLine("3. Withdraw money");
            Console.WriteLine("4. Back");
            Console.Write("Enter Operation> ");

            var choice = ReadInt();

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Balance: {account.Balance:C}");
                    Pause();
                    break;
                case 2:
                    Console.Write("Enter amount to deposit> ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal dep) && dep > 0)
                    {
                        account.Deposit(dep);
                        Console.WriteLine($"Deposited {dep:C}. New balance: {account.Balance:C}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    Pause();
                    break;
                case 3:
                    Console.Write("Enter amount to withdraw> ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal w) && w > 0)
                    {
                        if (account.Withdraw(w))
                            Console.WriteLine($"Withdrew {w:C}. New balance: {account.Balance:C}");
                        else
                            Console.WriteLine("Insufficient funds or invalid amount.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    Pause();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Incorrect choice, try again.");
                    Pause();
                    break;
            }
        }
    }

    private static long GenerateUniqueAccountNumber()
    {
        long candidate;
        do
        {
            candidate = rng.NextInt64(2_500_000_000L, 2_599_999_999L);
        } while (users.SelectMany(u => u.Accounts).Any(a => a.AccountNumber == candidate));
        return candidate;
    }

    private static int ReadInt()
    {
        var s = Console.ReadLine();
        if (int.TryParse(s, out int v)) return v;
        return -1;
    }

    private static void Pause()
    {
        Console.Write("Press enter to continue...");
        Console.ReadLine();
    }
}
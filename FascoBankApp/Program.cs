using System;
using System.Runtime.CompilerServices;

class Program
{ 
    class Records
    {
        public long AccountNumber;
        public string AccountName;
        public string UserName;
        public decimal Balance;
    }
    private static List<Records> records = new List<Records>();
    private static void Main()
    {
        Console.WriteLine("Welcome to Fasco Bank LTD");
        Console.WriteLine("1. Create new account");
        Console.WriteLine("2. Login existing account");
        Console.WriteLine("3. Exit");
        Console.WriteLine("select your choice>>>");

        string inputChoice = Console.ReadLine();
        int.TryParse(inputChoice, out int choice);
        Console.Clear();
        if (choice == 1 || choice == 2 || choice == 3)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Let's create an account for you");
                    CreateAccount(records);
                    break;
                case 2:
                    Console.WriteLine("Login your account");
                    FetchAccount(records);
                    break;
                case 3:
                    Console.WriteLine("Thank you for banking with us...");
                    break;
            }
        }

        else
        {
            Console.WriteLine("Incorrect choice, try again!!!");
            Main();
        }
    }
        private static void CreateAccount(List<Records> records)
        {
        var random = new Random();
        Console.Write("Input Fullname: ");
        var accountName = Console.ReadLine();
        Console.Clear();
        Console.Write("Input Username: ");
        var userName = Console.ReadLine();
        Console.Clear();
        long accountNumber = new Random().NextInt64(1000000000L, 9999999999L);
        records.Add(new Records() { AccountNumber = accountNumber, AccountName = accountName, UserName = userName, Balance = 0.0m });
        Console.WriteLine("Account Created.Details;");
        Console.WriteLine($"AccountNumber:{accountNumber}");
        Console.WriteLine("Press enter to go back to menu...");
        Console.ReadLine();
        Console.Clear();
        Main();
        }
        private static void FetchAccount(List<Records> records) 
        {
        Console.Write("Enter username: ");
        var input = Console.ReadLine();

        }
    
}
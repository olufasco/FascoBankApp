using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

class Program
{ 
    class Records
    {
        public long AccountNumber;
        public string AccountName;
        public string UserName;
        public decimal Balance;
        public int pin;
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
        long accountNumber = new Random().NextInt64(2500000000L, 2599999999L);
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
        foreach (var account in records)
        {
            if (input != null && input.Equals(account.UserName))
                Console.Clear();
            {
                Console.WriteLine($"Welcome {account.AccountName}\nYour current balance is {account.Balance}");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit money");
                Console.WriteLine("3. Withdraw money");
                Console.WriteLine("4. Exit");
                Console.Write("Enter Operation>");
                string inputChoice = Console.ReadLine();
                int.TryParse(inputChoice, out int choice);
                Console.Clear();
                if (choice == 1 || choice == 2 || choice == 3 || choice == 4)
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine($"Balance:{account.Balance}");
                            Console.WriteLine("Press enter to go back to menu...");
                            Console.ReadLine();
                            Console.Clear();
                            Main();
                            break;
                        case 2:
                            Console.Write("Enter amount to deposit>");
                            input = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine($"Depositing {input} to acccount...");
                            if (decimal.TryParse(input, out var deposit))
                            {
                                account.Balance += deposit;
                                Console.WriteLine($"New balance is {account.Balance}");
                                Console.WriteLine("Press enter to go back to home menu...");
                                Console.ReadLine();
                                Console.Clear();
                                Main();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Error, please specify quantity for deposit and try again...");
                                Console.WriteLine("Press enter to go back to home menu...");
                                Console.ReadLine();
                                Console.Clear();
                                Main();
                            }
                            break;
                        case 3:
                            Console.Write("Enter amount to withdraw>");
                            input = Console.ReadLine();
                            if (decimal.TryParse(input, out var withdraw))
                            {
                                if (withdraw <= account.Balance)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Withdrawing {input} from acccount...");
                                    account.Balance -= withdraw;
                                    Console.WriteLine($"New balance is {account.Balance}");
                                    Console.WriteLine("Press enter to go back to home menu...");
                                    Console.ReadLine();
                                    Console.Clear();
                                    Main();
                                    break;
                                }
                                else 
                                {
                                    Console.WriteLine("Error: Insufficient funds");
                                }
                                    Console.WriteLine("Press enter to go back to home menu...");
                                Console.ReadLine();
                                Console.Clear();
                                Main();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Error, please specify withdraw amount and try again...");
                                Console.WriteLine("Press enter to go back to home menu...");
                                Console.ReadLine();
                                Console.Clear();
                                Main();
                            }
                            break;
                            case 4:
                            Console.WriteLine("Thank you for choosing Fasco bank!\nPress enter to go back to home menu...");
                            Console.ReadLine();
                            Console.Clear();
                            Main();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect choice, try again!!!");
                    Main();
                }
            }
        }
    }
}
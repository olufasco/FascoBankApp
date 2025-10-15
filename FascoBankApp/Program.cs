using System;
using System.Runtime.CompilerServices;

class Program
{ 
    class Records
    {
        public int AccountNumber;
        public string AccountName;
        public string Username;
        public decimal Balance;
    }
    private static readonly Records _records = new Records();
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

                    break;
                case 2:
                    Console.WriteLine("Login your account\nEnter username");
                    break;
                case 3:
                    Console.WriteLine("Thank you for banking with us...");
                    break;
            }
        }

        else
        {
            Console.WriteLine("Incorrect choice, try again later!");
            Main();
        }
    }
}
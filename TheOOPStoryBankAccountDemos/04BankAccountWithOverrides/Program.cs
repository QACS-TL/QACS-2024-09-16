﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BankAccount account1 = new BankAccount(
                "12345678", 
                "Busby Berkeley");
            //inital account, funds deposited and withdrawn
            account1.DepositMoney(100.80m);
            account1.WithdrawMoney(60.80m);
            Console.WriteLine(
                $"Account Balance:{account1.Balance:C}");

            //create new account using constructor,
            //funds deposited and attempt to withdraw too much
            BankAccount account2 = new BankAccount(
                "98765432", 
                "Ginger Rogers", 
                200.00m);
            account2.DepositMoney(100.80m);
            account2.WithdrawMoney(360.80m);
            Console.WriteLine(
                $"Account Balance:{account2.Balance:C}");

            //Current account with £100 Overdaft
            CurrentAccount currentAccount1 = new CurrentAccount(
                "98787676", 
                "Donald O'Connor");
            currentAccount1.OverdraftLimit = 100;
            //Withdrawing a value that takes the
            //balance below 0 but above overdraft limit
            currentAccount1.WithdrawMoney(50);
            //Withdrawing a value that takes the
            //balance above overdraft limit
            currentAccount1.WithdrawMoney(51);
            Console.WriteLine(
                $"Current Account Balance:{currentAccount1.Balance:C}");

            //Create new current acccount using constructor
            CurrentAccount currentAccount2 = new CurrentAccount(
                "98765432", 
                "Fred Astaire", 
                100.00m, 
                100.00m);
            //Withdrawing a value that takes the
            //balance below 0 but above overdraft limit
            currentAccount2.WithdrawMoney(150.00m);
            //Withdrawing a value that takes the
            //balance above overdraft limit
            currentAccount2.WithdrawMoney(51.00m);
            Console.WriteLine(
                $"Current Account Balance:{currentAccount2.Balance:C}");

            //look at a newly created CurrentAccount
            //object as a simple BankAccount
            BankAccount bankAccount = new CurrentAccount(
                "87654321", 
                "Cyd Charisse", 
                100.00m, 
                100.00m);
            //Withdrawing a value that takes the
            //balance below 0 but above overdraft limit
            bankAccount.WithdrawMoney(150.00m);
            //Withdrawing a value that takes the
            //balance above overdraft limit
            currentAccount2.WithdrawMoney(51.00m);
            Console.WriteLine(
                $"Current Account Balance:{bankAccount.Balance:C}");


            //Savings account with no overdraft and add interest
            SavingsAccount savingsAccount1 = new SavingsAccount(
                "12121212", 
                "Gene Kelly", 
                50.00M, 
                5);
            //withdrawing any value with no other depoists will fail
            savingsAccount1.DepositMoney(50.00m);
            savingsAccount1.AddInterest();
            Console.WriteLine(
                $"Savings Account Balance:{savingsAccount1.Balance:C}");

            //Create new savings acccount using
            //constructor and attempting to withdraw too much
            SavingsAccount savingsAccount2 = new SavingsAccount(
                "23434545", 
                "Debbie Reynolds", 
                150m);
            savingsAccount2.WithdrawMoney(120.00m);
            //withdrawing any value with no other deposits will fail
            savingsAccount2.WithdrawMoney(50.00m);
            Console.WriteLine(
                $"Savings Account Balance:{savingsAccount2.Balance:C}");

            //ISA account with no overdraft
            ISAAccount isaAccount1 = new ISAAccount(
                "56745654", 
                "Jean Hagen", 
                1000.00M, 
                10.00M);
            //Attempt to withdraw too much money
            isaAccount1.WithdrawMoney(1500.00m);
            isaAccount1.AddInterest();
            Console.WriteLine($"ISA Account Balance:{isaAccount1.Balance:C}");

            //Create new ISA acccount using constructor
            BankAccount bankAccount2 = new ISAAccount(
                "76757473", 
                "Frank Sinatra", 
                15000.00m, 
                10.0M);
            //Add sum that doesn not exceed ISA limit
            bankAccount2.DepositMoney(500.00m);
            //Try to exceed upper ISA limit
            bankAccount2.DepositMoney(5000.01m);
            Console.WriteLine(
                $"ISA Account Balance:{bankAccount2.Balance:C}");


            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(account1);
            accounts.Add(account2);
            accounts.Add(currentAccount1);
            accounts.Add(currentAccount2);
            accounts.Add(savingsAccount1);
            accounts.Add(savingsAccount2);
            accounts.Add(isaAccount1);
            // accounts.Add(isaAccount2);

            accounts[3].WithdrawMoney(100); //Calls Current Account version - WooHoo!

            decimal totalFundsInBank = 0;
            foreach(BankAccount account in accounts)
            {
                totalFundsInBank += account.Balance;
            }
            Console.WriteLine(
                $"Using trad foreach loop. Total amount "
              + $"of money stored in bank is {totalFundsInBank:C}");

            //Using Lambda
            totalFundsInBank = accounts.Sum(a => a.Balance);
            Console.WriteLine(
                $"Using Lambda. Total amount "
              + $"of money stored in bank is {totalFundsInBank:C}");

            //BankAccount bankAccount1 = new CurrentAccount("888888", "Bill Bailey", 100.00M, 200.00M);
            //Console.WriteLine(bankAccount1.OverdraftLimit);

            if (accounts[2] is CurrentAccount)
            {
                decimal overdraftLimit = ((CurrentAccount)accounts[2]).OverdraftLimit;
            }


            SavingsAccount savingsAccount = accounts[3] as SavingsAccount;
            if (savingsAccount != null)
            {
                savingsAccount.InterestRate = 10.00M;
                savingsAccount.AddInterest();
            }

            if (accounts[4].GetType() == typeof(ISAAccount))
            {
                ISAAccount isa = (ISAAccount)accounts[4];
                isa.DepositMoney(100000000.00M); //will hit ISA fund ceiling and fail
            }

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public string depositOrWithdrawal { get; set; }
        public string checkingOrSaving { get; set; }
        public double transactionAmount { get; set; }

        //public Transaction(string inOrOut, string checkOrSave, double amount)
        //{
        //    depositOrWithdrawal = inOrOut;
        //    checkingOrSaving = checkOrSave;
        //    transactionAmount = amount;
        //}

    }
    class Program
    {
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();
            return userInput;
        }

        static double PromptForPositiveDouble(string promptDouble)
        {
            double userInput = 0;
            var isThisGoodInput = false;
            while (isThisGoodInput == false)
            {
                Console.Write(promptDouble);
                isThisGoodInput = Double.TryParse(Console.ReadLine(), out userInput);
                if (userInput <= 0)
                {
                    isThisGoodInput = false;
                    Console.WriteLine("Please input a number greater than zero.");
                }
            }
            return userInput;
        }

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to The Machine!");

            var transactions = new List<Transaction>();

            var menuLoop = true;

            while (menuLoop)
            {
                var menu2Loop = true;
                Console.WriteLine();
                var menuChoice = PromptForString("(D)eposit\n(W)ithdrawal\n(T)ransaction History\n(Q)uit\n").ToUpper();

                if (menuChoice == "Q")
                {
                    menuLoop = false;
                }
                else if (menuChoice == "D")
                {
                    // choice to deposit in either account
                    while (menu2Loop)
                    {
                        var menu2ndChoice = PromptForString("Deposit in (c)hecking or (s)avings? Or (q)uit. ").ToUpper();

                        if (menu2ndChoice == "C")
                        {
                            var depositChecking = PromptForPositiveDouble("How much would you like to deposit in checking? ");
                            var menu3rdChoice = PromptForString($"You'd like to deposit ${depositChecking} into checking? (y) to confirm ").ToUpper();
                            if (menu3rdChoice == "Y")
                            {
                                var confirmedDepositToCheckingTransaction = new Transaction();
                                confirmedDepositToCheckingTransaction.checkingOrSaving = "checking";
                                confirmedDepositToCheckingTransaction.depositOrWithdrawal = "deposit";
                                confirmedDepositToCheckingTransaction.transactionAmount = depositChecking;
                                transactions.Add(confirmedDepositToCheckingTransaction);
                                Console.WriteLine("Deposited in checking!");
                            }
                        }
                        else if (menu2ndChoice == "S")
                        {
                            var depositSavings = PromptForPositiveDouble("How much would you like to deposit in savings? ");
                            var menu3rdChoice = PromptForString($"You'd like to deposit ${depositSavings} into savings? (y) to confirm ").ToUpper();
                            if (menu3rdChoice == "Y")
                            {
                                var confirmedDepositToSavingsTransaction = new Transaction();
                                confirmedDepositToSavingsTransaction.checkingOrSaving = "saving";
                                confirmedDepositToSavingsTransaction.depositOrWithdrawal = "deposit";
                                confirmedDepositToSavingsTransaction.transactionAmount = depositSavings;
                                transactions.Add(confirmedDepositToSavingsTransaction);
                                Console.WriteLine("Deposited in savings!");
                            }

                        }
                        else if (menu2ndChoice == "Q")
                        {
                            menu2Loop = false;
                        }
                    }

                }
                else if (menuChoice == "W")
                {
                    // choice to withdraw in either account
                    while (menu2Loop)
                    {
                        var menu2ndChoice = PromptForString("Withdraw from (c)hecking or (s)avings? Or (q)uit. ").ToUpper();

                        if (menu2ndChoice == "C")
                        {
                            var withdrawChecking = PromptForPositiveDouble("How much would you like to withdraw from checking? ");
                            var menu3rdChoice = PromptForString($"You'd like to withdraw ${withdrawChecking} from checking? (y) to confirm ").ToUpper();
                            if (menu3rdChoice == "Y")
                            {
                                var confirmedWithdrawFromCheckingTransaction = new Transaction();
                                confirmedWithdrawFromCheckingTransaction.checkingOrSaving = "checking";
                                confirmedWithdrawFromCheckingTransaction.depositOrWithdrawal = "withdrawal";
                                confirmedWithdrawFromCheckingTransaction.transactionAmount = withdrawChecking;
                                transactions.Add(confirmedWithdrawFromCheckingTransaction);
                                Console.WriteLine("Withdrawn from checking!");
                            }
                        }
                        else if (menu2ndChoice == "S")
                        {
                            var withdrawSavings = PromptForPositiveDouble("How much would you like to withdraw from savings? ");
                            var menu3rdChoice = PromptForString($"You'd like to withdraw ${withdrawSavings} from savings? (y) to confirm ").ToUpper();
                            if (menu3rdChoice == "Y")
                            {
                                var confirmedWithdrawalFromSavingsTransaction = new Transaction();
                                confirmedWithdrawalFromSavingsTransaction.checkingOrSaving = "saving";
                                confirmedWithdrawalFromSavingsTransaction.depositOrWithdrawal = "withdrawal";
                                confirmedWithdrawalFromSavingsTransaction.transactionAmount = withdrawSavings;
                                transactions.Add(confirmedWithdrawalFromSavingsTransaction);
                                Console.WriteLine("Withdrawn from savings!");
                            }

                        }
                        else if (menu2ndChoice == "Q")
                        {
                            menu2Loop = false;
                        }
                    }
                }
                else if (menuChoice == "T")
                {
                    // choice to see either all checking or all savings transactions
                    Console.WriteLine("list of transactions");
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"{transaction.checkingOrSaving} {transaction.depositOrWithdrawal} {transaction.transactionAmount}");
                    }
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}

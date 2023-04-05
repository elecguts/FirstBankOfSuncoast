using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public string depositOrWithdrawal { get; set; }
        public string checkingOrSaving { get; set; }
        public double transactionAmount { get; set; }
    }
    class TransactionHistory
    {
        public List<Transaction> transactions = new List<Transaction>();
        public double TotalAccount(string accountType)
        {
            double totalInAccount = 0;
            var query = transactions.Where(transaction => transaction.checkingOrSaving == accountType);
            foreach (var transaction in query)
            {
                if (transaction.depositOrWithdrawal == "deposit")
                {
                    totalInAccount = totalInAccount + transaction.transactionAmount;
                }
                else
                {
                    totalInAccount = totalInAccount - transaction.transactionAmount;
                }
            }
            return totalInAccount;
        }
        public void SaveAccount()
        {
            var fileWriter = new StreamWriter("transactions.csv");

            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(transactions);

            fileWriter.Close();
        }
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

            TextReader reader;
            if (File.Exists("transactions.csv"))
            {
                reader = new StreamReader("transactions.csv");
            }
            else
            {
                reader = new StringReader("");
            }
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            var csvReader = new CsvReader(reader, config);

            var user1History = new TransactionHistory();
            user1History.transactions = csvReader.GetRecords<Transaction>().ToList();
            reader.Close();

            var menuLoop = true;

            while (menuLoop)
            {
                var menu2Loop = true;
                Console.WriteLine();
                var menuChoice = PromptForString("(D)eposit\n(W)ithdrawal\n(T)ransaction History\n(B)alances\n(Q)uit\n").ToUpper();

                if (menuChoice == "Q")
                {
                    menuLoop = false;
                }
                else if (menuChoice == "B")
                {
                    Console.WriteLine();
                    Console.WriteLine($"${user1History.TotalAccount("checking")} in checking");
                    Console.WriteLine($"${user1History.TotalAccount("saving")} in savings");
                    Console.WriteLine();
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
                                user1History.transactions.Add(confirmedDepositToCheckingTransaction);
                                user1History.SaveAccount();
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
                                user1History.transactions.Add(confirmedDepositToSavingsTransaction);
                                user1History.SaveAccount();
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
                            if (user1History.TotalAccount("checking") - withdrawChecking >= 0)
                            {
                                var menu3rdChoice = PromptForString($"You'd like to withdraw ${withdrawChecking} from checking? (y) to confirm ").ToUpper();
                                if (menu3rdChoice == "Y")
                                {
                                    var confirmedWithdrawFromCheckingTransaction = new Transaction();
                                    confirmedWithdrawFromCheckingTransaction.checkingOrSaving = "checking";
                                    confirmedWithdrawFromCheckingTransaction.depositOrWithdrawal = "withdrawal";
                                    confirmedWithdrawFromCheckingTransaction.transactionAmount = withdrawChecking;
                                    user1History.transactions.Add(confirmedWithdrawFromCheckingTransaction);
                                    user1History.SaveAccount();
                                    Console.WriteLine("Withdrawn from checking!");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Your checking account currently has ${user1History.TotalAccount("checking")}");
                            }
                        }
                        else if (menu2ndChoice == "S")
                        {
                            var withdrawSavings = PromptForPositiveDouble("How much would you like to withdraw from savings? ");
                            if (user1History.TotalAccount("saving") - withdrawSavings >= 0)
                            {
                                var menu3rdChoice = PromptForString($"You'd like to withdraw ${withdrawSavings} from savings? (y) to confirm ").ToUpper();
                                if (menu3rdChoice == "Y")
                                {
                                    var confirmedWithdrawalFromSavingsTransaction = new Transaction();
                                    confirmedWithdrawalFromSavingsTransaction.checkingOrSaving = "saving";
                                    confirmedWithdrawalFromSavingsTransaction.depositOrWithdrawal = "withdrawal";
                                    confirmedWithdrawalFromSavingsTransaction.transactionAmount = withdrawSavings;
                                    user1History.transactions.Add(confirmedWithdrawalFromSavingsTransaction);
                                    user1History.SaveAccount();
                                    Console.WriteLine("Withdrawn from savings!");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Your savings account currently has ${user1History.TotalAccount("saving")}");
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
                    foreach (var transaction in user1History.transactions)
                    {
                        Console.WriteLine($"{transaction.checkingOrSaving} {transaction.depositOrWithdrawal} {transaction.transactionAmount}");
                    }
                    Console.WriteLine($"${user1History.TotalAccount("checking")} in checking");
                    Console.WriteLine($"${user1History.TotalAccount("saving")} in savings");
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}

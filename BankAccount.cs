using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class BankAccount
    {
        private static int accountNumberSeed = 1234567890;
        
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance 
        { 
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }

        public BankAccount(string name, decimal initialBalance)
        {
            //this.Owner = name;
            //this.Balance = initialBalance;
            // "this" is only required when param names are the same as the property names
            Number = accountNumberSeed.ToString();
            accountNumberSeed++;
            
            Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
        }

        private List<Transaction> allTransactions = new List<Transaction>();

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawl(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawl must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawl");
            }
            var withdrawl = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawl);
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }
            return report.ToString();
        }
    }
}

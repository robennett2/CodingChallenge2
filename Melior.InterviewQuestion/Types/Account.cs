﻿namespace Melior.InterviewQuestion.Types
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }
        
        public void Debit(decimal amount)
        {
            Balance -= amount;
        }
    }
}


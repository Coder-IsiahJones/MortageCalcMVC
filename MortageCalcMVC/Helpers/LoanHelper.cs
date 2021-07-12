using MortageCalcMVC.Models;
using System;

namespace MortageCalcMVC.Helpers
{
    public class LoanHelper
    {
        public Loan GetPayments(Loan loan)
        {
            // Calculate the monthly payment
            loan.Payment = CalculatePayment(loan.Amount, loan.Rate, loan.Term);

            // Loop from 1 to the term
            var balance = loan.Amount;
            var totalInterest = 0.0m;
            var monthlyInterest = 0.0m;
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalculateMonthlyRate(loan.Rate);

            // Loop over each month until term is reached for the loan.
            for (int month = 1; month <= loan.Term; month++)
            {
                monthlyInterest = CalculateMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                // Push object into the loan model
                loan.Payments.Add(loanPayment);
            }

            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest;

            return loan;
        }

        private decimal CalculatePayment(decimal amount, decimal rate, int term)
        {
            // Convert Year to monthly rate.
            var monthlyRate = CalculateMonthlyRate(rate);

            // Convert to double
            var dblRate = Convert.ToDouble(monthlyRate);
            var dblAmount = Convert.ToDouble(amount);

            // Equation
            double dblPayment = (dblAmount * dblRate) / (1 - Math.Pow(1 + dblRate, -term));

            return Convert.ToDecimal(dblPayment);
        }

        private decimal CalculateMonthlyRate(decimal rate)
        {
            return rate / 1200;
        }

        private decimal CalculateMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return balance * monthlyRate;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortageCalcMVC.Models
{
    public class LoanPayment
    {
        public int Month { get; set; }
        public decimal Payment { get; set; }
        public decimal MonthlyPrincipal { get; set; }
        public decimal MonthlyIntrest { get; set; }
        public decimal TotalIntrest { get; set; }
        public decimal Balance { get; set; }
    }
}

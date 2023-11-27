﻿using System.ComponentModel.DataAnnotations;

namespace Academy.Model.ViewModels
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }

    public class TransactionViewModel
    {
        public int Amount { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.Models.TransactionModels
{
    public class TransactionType
    { 
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransactionTypeId { get; set; }
        public string TransactionTypeTitle { get; set; }
        
        public virtual List<Transaction> Transactions { get; set; }
    }
}

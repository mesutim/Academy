using Academy.Model.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.ViewModels
{
    public class ShowQuestionVM
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}


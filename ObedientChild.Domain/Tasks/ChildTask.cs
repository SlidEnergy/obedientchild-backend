using ObedientChild.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain
{
    public class ChildTask
    {
        public int Id { get; set; }

        public int ChildId { get; set; }

        public DateOnly Date { get; set; }

        public int DeedId { get; set; }

        public virtual Deed Deed { get; set; }

        public ChildTaskStatus Status { get; set; }

        public ChildTask(int childId, DateOnly date, int deedId) 
        {
            ChildId = childId;
            Date = date;
            DeedId = deedId;
            Status = ChildTaskStatus.ToDo;
        }
    }
}

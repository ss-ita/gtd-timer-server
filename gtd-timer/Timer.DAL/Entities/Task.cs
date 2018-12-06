using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gtdtimer.Timer.DAL.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string GroupName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public DateTime Goal { get; set; }
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

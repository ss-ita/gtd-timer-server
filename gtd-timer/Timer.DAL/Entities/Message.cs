using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gtdtimer.Timer.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageText { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

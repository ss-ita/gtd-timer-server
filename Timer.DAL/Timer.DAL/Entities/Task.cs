using System;

namespace Timer.DAL.Timer.DAL.Entities
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

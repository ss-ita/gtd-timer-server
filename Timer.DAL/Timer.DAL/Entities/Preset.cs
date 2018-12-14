using System.Collections.Generic;

namespace Timer.DAL.Timer.DAL.Entities
{
    public class Preset
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Timer> Timers { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
        
    }
}

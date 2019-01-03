using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timer.DAL.Timer.DAL.Entities
{
    public class Preset
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
        
    }
}

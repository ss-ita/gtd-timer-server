using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gtdtimer.Timer.DAL.Entities
{
    public class Preset
    {
        public int Id { get; set; }
        public string PresetName { get; set; }
        public TimeSpan PresetWorkTime { get; set; }
        public TimeSpan PresetSmalBreakTime { get; set; }
        public TimeSpan PresetBigBreakTimeTime { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}

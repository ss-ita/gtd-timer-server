using Common.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.ModelsDTO
{
    public class TimerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Interval { get; set; }

        public int PresetId { get; set; }

    }

    public class PresetDTO
    {
        public int Id { get; set; }

        public string PresetName { get; set; }

        public IEnumerable<TimerDTO> Timers { get; set; }

        public int? UserId { get; set; }
    }
}
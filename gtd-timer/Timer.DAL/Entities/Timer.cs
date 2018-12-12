﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gtdtimer.Timer.DAL.Entities
{
    public class Timer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Interval { get; set; }

        public int PresetId { get; set; }
        public Preset Preset { get; set; }

    }
}
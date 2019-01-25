using System;

namespace GtdCommon.ModelsDto
{
    public class TaskRecordDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        public int TaskId { get; set; }
    }
}

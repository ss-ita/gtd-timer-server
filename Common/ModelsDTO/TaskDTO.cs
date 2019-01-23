using System;
using System.Xml.Serialization;

namespace Common.ModelsDTO
{
    [Serializable, XmlRoot("TaskDTO")]
    public class TaskDTO
    {
        [XmlIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ElapsedTime { get; set; }
        public DateTime LastStartTime { get; set; }
        public TimeSpan? Goal { get; set; }
        public bool IsActive { get; set; }
        public bool IsRunning { get; set; }
        [XmlIgnore]
        public int UserId { get; set; }
    }
}

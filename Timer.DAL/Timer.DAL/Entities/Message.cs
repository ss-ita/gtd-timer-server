﻿namespace Timer.DAL.Timer.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

using System;

namespace Vidly.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Type { get; set; }
        public string Book { get; set; }
    }
}
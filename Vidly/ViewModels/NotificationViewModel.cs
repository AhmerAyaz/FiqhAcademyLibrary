using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NotificationViewModel
    {
        public List<Notification> NewNotifications { get; set; }
        public List<Notification> OldNotifications { get; set; }
    }
}
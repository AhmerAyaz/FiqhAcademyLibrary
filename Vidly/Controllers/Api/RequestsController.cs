using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers.Api
{
    public class RequestsController : ApiController
    {
        private ApplicationDbContext _context;

        public RequestsController()
        {
            _context = new ApplicationDbContext();
        }
        //List to IENUMBERABLE
        public NotificationViewModel GetRequests(string query = null)
        {
            //var moviesQuery = _context.UserNotifications.Include(n => n.User).Where(n => n.User.Email == User.Identity.Name).Include(n => n.Notification).ToList();
            
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .ToList();
            NotificationViewModel viewModel = new NotificationViewModel();
            if (notifications.Count != 0)
                viewModel.NewNotifications = notifications;
            else
                viewModel.OldNotifications = _context.UserNotifications.Where(un => un.UserId == userId).OrderByDescending(s => s.NotificationId).Select(un => un.Notification).Take(3).ToList();

            return viewModel;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications.Where(un => un.UserId == userId && !un.IsRead).ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}

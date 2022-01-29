using PagedList;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize(Roles = RoleName.CanManageMovies)]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult getUsers()
        {
            return Json(_context.Users.Select(x => new
            {
                DepartmentID = x.Id,
                DepartmentName = x.FirstName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Index(string currentName, int? page, string customerName)
        {
            var customer = _context.Users.Where(u => u.Email != "admin@fiqh.com").ToList();

            if (customerName != null)
            {
                page = 1;
            }
            else
            {
                customerName = currentName;
            }

            ViewBag.currentName = customerName;
            if (!string.IsNullOrEmpty(customerName))
            {
                customer = _context.Users.Where(s => s.FirstName.Contains(customerName) && s.Email != "admin@fiqh.com").ToList();
            }


            if (customer == null)
                return HttpNotFound();


            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View("Index", customer.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Detail(string id)
        {
            var User = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            var Register = new RegisterViewModel
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Password = User.DrivingLicense,
                ConfirmPassword = User.DrivingLicense,
                Email = User.Email,
                Phone = User.Phone
            };
            return View(Register);
            //var customer = _context.Rentals.Include(c => c.Customer).Include(c => c.Movie).Include(c => c.Customer.MembershipType).Where(c => c.Customer.Id == id).ToList();

            //if (customer == null)
            //    return HttpNotFound();

            //if (customer.Count != 0)
            //{
            //    TempData["Name"] = customer[0].Customer.Name;
            //    TempData["Membership"] = customer[0].Customer.MembershipType.Name;
            //    TempData["Phone"] = customer[0].Customer.Phone;
            //    return View(customer);
            //}
            //else
            //{
            //    TempData["Name"] = "No books rented yet!";
            //    return View(customer);
            //}
            return View();
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        
        


    }
}
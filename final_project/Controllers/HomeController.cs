using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Login Page.";

            return View();
        }

        public ActionResult AddUserToDatabase(FormCollection fc)
        {
            String firstName = fc["firstname"];
            String lastName = fc["lastname"];
            String email = fc["email"];
            int age = Convert.ToInt16(fc["age"]);
            String password = fc["password"];
            String address = fc["address"];

            user use = new user();
            use.first_name = firstName;
            use.last_name= lastName;
            use.email = email;
            use.password = password;
            use.address = address;
            use.age = age;
            use.role_id= 1;

            kristineEntities fe = new kristineEntities();
            fe.users.Add(use);
            fe.SaveChanges();

            //insert the code that will save these information to the DB

            return RedirectToAction("ShowUser");
        }

       

        public ActionResult ShowUser()
        {
            kristineEntities fe = new kristineEntities();
            var userList = (from a in fe.users
                            select a).ToList();

            ViewData["UserList"] = userList;
            return View();
        }
        public ActionResult UserUpdate(int id)
        {
            kristineEntities rdbe = new kristineEntities();
            user u = rdbe.users.Find(id);

            if (u == null)
            {
                return HttpNotFound();
            }

            // You can pass the user object to the view for editing
            return View(u);
        }

      

        [HttpPost]

        public ActionResult Delete(int id)
        {
            kristineEntities rdbe = new kristineEntities();
            user u = (from a in rdbe.users
                      where a.user_id == id
                      select a).FirstOrDefault();
            rdbe.users.Remove(u);
            rdbe.SaveChanges();

            return RedirectToAction("ShowUser");
        }
        [HttpPost]
        public ActionResult Update(int id)
        {
            int x = id;


            kristineEntities user = new kristineEntities();

            var selectedUser = (from a in user.users where a.user_id == x select a).ToList();


            ViewData["User"] = selectedUser;

            return View();
            //  return RedirectToAction("UserUpdate");  // Redirect to the appropriate action or view
        }

        public ActionResult UpdateUser(FormCollection fc, int id)
        {
            kristineEntities rdbe = new kristineEntities();
            user u = (from a in rdbe.users
                      where a.user_id == id
                      select a).FirstOrDefault();

            String new_first_name = fc["new_firstname"];
            String new_last_name = fc["new_lastname"];
            String new_email = fc["new_email"];
            String new_address = fc["new_address"];
            int new_age = Convert.ToInt16(fc["new_age"]);
            String new_password = fc["new_password"];

            u.first_name = new_first_name;
            u.last_name = new_last_name;
            u.email = new_email;
            u.password = new_password;
            u.address = new_address;
            u.age = new_age;


            rdbe.SaveChanges();

            return RedirectToAction("ShowUser");
        }

    }
}
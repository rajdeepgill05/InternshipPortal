using InternshipPortal.Helpers;
using InternshipPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace InternshipPortal.Controllers
{
    public class ApplicationUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUser
        public ActionResult Index()
        {
            var users = db.Users.Select(user => new UserListViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                RoleName = db.Roles.FirstOrDefault(role => role.Id == user.Roles.FirstOrDefault().RoleId).Name
            }).ToList();
            return View(users);
        }

        public ActionResult AssignRole(string Id)
        {
            var model = new UserRoleViewModel();
            var userRoleHelper = new UserRoleHelper();

            model.Id = Id;
            model.UserName = db.Users.FirstOrDefault(p => p.Id == Id).UserName;
            var roles = userRoleHelper.GetAllroles();
            var userRoles = userRoleHelper.GetUserRoles(Id);

            model.Roles = new MultiSelectList(roles, "Name", "Name", userRoles);

            return View(model);
                
        }

        [HttpPost]
        public ActionResult AssignRole(UserRoleViewModel model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindById(model.Id);

            var userRoles = userManager.GetRoles(model.Id);

            foreach (var role in userRoles)
            {
                userManager.RemoveFromRole(user.Id, role);
            }

            foreach (var role in model.SelectedRoles)
            {
                userManager.AddToRole(user.Id, role);
            };

            return RedirectToAction("Index");
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userRoleHelper = new UserRoleHelper();
            var userRoles = userRoleHelper.GetUserRoles(id).ToList();
            if (userRoles.Contains("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(user);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserListViewModel model)
        {
            var Id = model.Id;
            ApplicationUser user = db.Users.Find(Id);
            
            
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
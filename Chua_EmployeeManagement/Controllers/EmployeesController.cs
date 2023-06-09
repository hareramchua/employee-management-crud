using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Chua_EmployeeManagement.Models;

namespace Chua_EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeDbEntities db = new EmployeeDbEntities();

        // Helper method to check if a string is alphanumeric
        private bool IsAlphaNumeric(string text)
        {
            return text.All(char.IsLetterOrDigit);
        }

        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                return View(employee);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving employee details.";
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmpNo,FirstName,LastName,Birthdate,ContactNo,EmailAddress")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if EmpNo is unique
                    if (db.Employees.Any(e => e.EmpNo == employee.EmpNo))
                    {
                        ModelState.AddModelError("EmpNo", "Employee number must be unique.");
                        return View(employee);
                    }

                    // Validate EmpNo format (alphanumeric, 6 characters)
                    if (!IsAlphaNumeric(employee.EmpNo) || employee.EmpNo.Length != 6)
                    {
                        ModelState.AddModelError("EmpNo", "Employee number should be alphanumeric and have a length of 6 characters.");
                        return View(employee);
                    }

                    // Validate FirstName (alphabetic, maximum 15 characters)
                    if (!Regex.IsMatch(employee.FirstName, @"^[a-zA-Z]+$") || employee.FirstName.Length > 15)
                    {
                        ModelState.AddModelError("FirstName", "First Name should only contain alphabetic characters and should not exceed 15 characters.");
                        return View(employee);
                    }

                    // Validate LastName (alphabetic, maximum 15 characters)
                    if (!Regex.IsMatch(employee.LastName, @"^[a-zA-Z]+$") || employee.LastName.Length > 15)
                    {
                        ModelState.AddModelError("LastName", "Last Name should only contain alphabetic characters and should not exceed 15 characters.");
                        return View(employee);
                    }

                    // Validate ContactNo format (11 characters and starts with 09)
                    if (employee.ContactNo.Length != 11 || !employee.ContactNo.StartsWith("09"))
                    {
                        ModelState.AddModelError("ContactNo", "Contact number should be 11 characters long and start with '09'.");
                        return View(employee);
                    }

                    // Check if the combination of FirstName and LastName is unique
                    if (db.Employees.Any(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName))
                    {
                        ModelState.AddModelError("FirstName", "An employee with the same First Name and Last Name already exists.");
                        ModelState.AddModelError("LastName", "An employee with the same First Name and Last Name already exists.");
                        return View(employee);
                    }

                    // Check if the ID is already used
                    if (db.Employees.Any(e => e.ID == employee.ID))
                    {
                        ModelState.AddModelError("ID", "An employee with the same ID already exists.");
                        return View(employee);
                    }

                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while creating the employee.";
                return View("Error");
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                return View(employee);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving employee details.";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmpNo,FirstName,LastName,Birthdate,ContactNo,EmailAddress")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if EmpNo is unique (excluding the current employee)
                    if (db.Employees.Any(e => e.EmpNo == employee.EmpNo && e.ID != employee.ID))
                    {
                        ModelState.AddModelError("EmpNo", "Employee number must be unique.");
                        return View(employee);
                    }

                    // Validate EmpNo format (alphanumeric, 6 characters)
                    if (!IsAlphaNumeric(employee.EmpNo) || employee.EmpNo.Length != 6)
                    {
                        ModelState.AddModelError("EmpNo", "Employee number should be alphanumeric and have a length of 6 characters.");
                        return View(employee);
                    }

                    // Validate FirstName (alphabetic, maximum 15 characters)
                    if (!Regex.IsMatch(employee.FirstName, @"^[a-zA-Z]+$") || employee.FirstName.Length > 15)
                    {
                        ModelState.AddModelError("FirstName", "First Name should only contain alphabetic characters and should not exceed 15 characters.");
                        return View(employee);
                    }

                    // Validate LastName (alphabetic, maximum 15 characters)
                    if (!Regex.IsMatch(employee.LastName, @"^[a-zA-Z]+$") || employee.LastName.Length > 15)
                    {
                        ModelState.AddModelError("LastName", "Last Name should only contain alphabetic characters and should not exceed 15 characters.");
                        return View(employee);
                    }

                    // Validate ContactNo format (11 characters and starts with 09)
                    if (employee.ContactNo.Length != 11 || !employee.ContactNo.StartsWith("09"))
                    {
                        ModelState.AddModelError("ContactNo", "Contact number should be 11 characters long and start with '09'.");
                        return View(employee);
                    }

                    // Check if the combination of FirstName and LastName is unique
                    if (db.Employees.Any(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName && e.ID != employee.ID))
                    {
                        ModelState.AddModelError("FirstName", "An employee with the same First Name and Last Name already exists.");
                        ModelState.AddModelError("LastName", "An employee with the same First Name and Last Name already exists.");
                        return View(employee);
                    }

                    // Check if the ID is already used
                    if (db.Employees.Any(e => e.ID == employee.ID && e.ID != employee.ID))
                    {
                        ModelState.AddModelError("ID", "An employee with the same ID already exists.");
                        return View(employee);
                    }

                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while updating the employee.";
                return View("Error");
            }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                return View(employee);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while retrieving employee details.";
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the employee.";
                return View("Error");
            }
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
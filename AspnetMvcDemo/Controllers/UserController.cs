using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class UserController : Controller
    {
        QCEntities db = new QCEntities();

        User user = new User();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult CreateUser()
        {
            return View(user);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult CreateUser([Bind(Exclude = "Photo")]User user)
        {
            try
            {


                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }

                //Here we pass the byte array to user context to store in db
                user.Photo = imageData;

                ObjectParameter statusCode = new ObjectParameter("StatusCode", typeof(int));
                ObjectParameter statusMessage = new ObjectParameter("StatusMessage", typeof(string));

                db.AddUpdateUser(0, user.FullName, user.PhoneNumber, user.Address,
                    user.Email, user.Username, user.Password,
                    user.Photo, user.JobTitle,
                    null, null, null, null, null, statusCode, statusMessage);
                TempData["AlertMessage"] = "User added Successfully";
                return RedirectToAction("UsersList");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        
        public ActionResult UserProfile()
        {
            String Username = Session["Username"].ToString();
            return View(db.Users.Where(x => x.Username == Username).FirstOrDefault());
        }
        
        public ActionResult EditProfile()
        {
            String Username = Session["Username"].ToString();
            return View(db.Users.Where(x => x.Username == Username).FirstOrDefault());
        }
       

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult EditUserProfile([Bind(Exclude = "Photo")]User user)
        {
            try
            {

                user.Id = db.Users.Where(x => x.Username == user.Username).Select(x => x.Id).FirstOrDefault();
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];
                    if (poImgFile.ContentLength > 0)
                    {
                        using (var binary = new BinaryReader(poImgFile.InputStream))
                        {
                            imageData = binary.ReadBytes(poImgFile.ContentLength);
                        }
                        user.Photo = imageData;
                    }else
                    {
                        user.Photo = db.Users.Where(x => x.Username == user.Username).Select(x => x.Photo).FirstOrDefault();
                    }
                }
                else
                {
                    user.Photo = db.Users.Where(x => x.Username == user.Username).Select(x=>x.Photo).FirstOrDefault();
                }

                //Here we pass the byte array to user context to store in db
               

                ObjectParameter statusCode = new ObjectParameter("StatusCode", typeof(int));
                ObjectParameter statusMessage = new ObjectParameter("StatusMessage", typeof(string));

                db.AddUpdateUser(user.Id, user.FullName, user.PhoneNumber, user.Address,
                    user.Email, user.Username, user.Password,
                    user.Photo, user.JobTitle,
                    null, null, null, null, null, statusCode, statusMessage);
                TempData["AlertMessage"] = "Profile updated Successfully";
                return RedirectToAction("UserProfile");
            }
            catch 
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

       [HttpGet]
       public ActionResult UsersList()
        {
            return View(db.Users.ToList());
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public FileContentResult UserPhotos()
        {
            // to get the user details to load user Image
            String Username = Session["Username"].ToString();
            
            var userImage = db.Users.Where(x => x.Username == Username).FirstOrDefault();
            if (userImage.Photo != null)
            {
                return new FileContentResult(userImage.Photo, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/avatars/UserIcon.jpg");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return File(imageData, "image/jpeg");
            }

            
        }
    }
}
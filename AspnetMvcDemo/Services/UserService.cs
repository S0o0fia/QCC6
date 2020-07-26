using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspnetMvcDemo.Services
{
    public class UserService
    {
        QCEntities db = new QCEntities();

        //Function for getting Factories for each User
        public List<User> UserDetails ()
        {
            var q = (from user in db.Users
                     where user.Location_Id != null
                     select user
                     ).ToList();
            return q;
        }
        public User UserDetails(int id)
        {
            var q = (from user in db.Users
                     where user.Id == id
                     select user
                     ).FirstOrDefault();
            return q;
        }
    }
}
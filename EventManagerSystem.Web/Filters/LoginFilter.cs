﻿using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Filters
{
    public static class LoginFilter
    {
        public static bool IsAdmin()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            if (user != null)
            {
                if (user.IsAdmin)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                //HttpContext.Current.Response.Redirect("/Home/Index");
                return false;
            }
        }

        public static bool IsNotLogedUser()
        {
            if (HttpContext.Current.Session["LoggedUser"] == null)
            {
                return true;
            }
            else
            {
                //HttpContext.Current.Response.Redirect("/Home/IndexPage");
                return false;
            }
        }

       


        public static int GetUserId()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            return user.Id;
        }

        public static User GetUserName()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];
            
            return user;
        }


        public static bool IsUserTeacher()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];
            if (user is Teacher)
            {
                return true;
            }
            return false;
        }
        public static bool IsUserStudent()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];
            if (user is Student)
            {
                return true;
            }
            return false;
        }

        public static User GetUserConfirm()
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            return user;
        }
    }
}
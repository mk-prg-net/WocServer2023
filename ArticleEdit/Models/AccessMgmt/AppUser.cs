﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// Steht nur bereit, wenn das Niget- Paket Mocrosoft.AspNet.Identity.Core installiert wurde
using Microsoft.AspNet.Identity;

namespace ArticleEdit.Models
{
    // Implementierung eines App- Users
    public class AppUser : 
        // Idiotisch: alle weiteren Schnittstellen erfordern einen IUser<string>
        IUser<string>, 
        // Der User hat eine 64 Bit UID
        IUser<long>
    {

        public AppUser(long userId, string userName)
        {
            this.Id = userId;
            this.UserName = userName;
        }

        public long Id { get; }

        // Um den restlichen Identity Framework zu genügen, muss die Id in einen
        // string gewandelt werden
        string IUser<string>.Id => Id.ToString();

        // Auch hier muss unbedingt ein setter definiert werden. Warum?
        public string UserName {
            get;
            set;
        }
    }
}
﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TestWebApi_v1.Models.DbContext
{
    public partial class User : IdentityUser
    {
        public User()
        {
            BoTruyens = new HashSet<BoTruyen>();
            UserRoles = new HashSet<UserRoles>();
            bookmarks = new HashSet<Bookmark>();
            Notification=new HashSet<ThongbaoUser>();
            BinhLuans = new HashSet<BinhLuan>();
            ReplyComments = new HashSet<ReplyComment>();
            UserJoinChats = new HashSet<UserJoinChat>();


        }

        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public DateTimeOffset? JoinDate { get; set; }


        public virtual ICollection<BoTruyen> BoTruyens { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public virtual ICollection<ThongbaoUser> Notification { get; set; }

        public virtual ICollection<Bookmark> bookmarks { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<ReplyComment> ReplyComments { get; set; }
        public virtual ICollection<UserJoinChat> UserJoinChats { get; set; }

    }
    public class UserViewChat
    {
        public string Id { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}

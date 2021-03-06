﻿using System;

namespace SoftwareHouse.DataAccess.Models.UserInformation
{
    public class UserWorkPhoto
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string PhotoUrl { get; set; }
    }
}
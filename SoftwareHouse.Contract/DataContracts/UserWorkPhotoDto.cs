﻿using System;

namespace SoftwareHouse.Contract.DataContracts
{
    public class UserWorkPhotoDto
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUserDto ApplicationUser { get; set; }

        public string PhotoUrl { get; set; }
    }
}

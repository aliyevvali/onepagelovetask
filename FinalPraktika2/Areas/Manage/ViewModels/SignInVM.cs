﻿using System.ComponentModel.DataAnnotations;

namespace FinalPraktika2.Areas.Manage.ViewModels
{
    public class SignInVM
    {
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace AdminLibary.ViewModel.LoginViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }   
    }
}

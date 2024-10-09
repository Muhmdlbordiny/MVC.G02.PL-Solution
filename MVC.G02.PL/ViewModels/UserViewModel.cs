﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC.G02.PL.ViewModels
{
    public class UserViewModel
    {
        public string Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [ValidateNever]
        public IEnumerable<string> Roles { get; set; }
    }
}

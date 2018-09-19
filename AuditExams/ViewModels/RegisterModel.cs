﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AuditExams.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Не указан e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Class? Class { get; set; }
        public int AccountTypeId { get; set; }
        [NotMapped]

        public AccountType AccountType
        {
            get => (AccountType)AccountTypeId;
            set => AccountTypeId = (int)value;
        }
    }
    public enum AccountType
    {
        Student =0,
        Teacher = 1,
        Admin = 2,
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace API.Models
{
    public class User
    {
        #region Fields
        private int id;
        private string firstname;
        private string lastname;
        private string username;
        private string password;
        private string phonenumber;
        private string email;
        private string address;
        #endregion

        #region Properties

        [Key]       
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

       
        [Required]
        [MaxLength(250)]
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        [Required]
        [MaxLength(250)]
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }


        [MaxLength(250)]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [Required]
        [MaxLength(250)]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        [MaxLength(250)]
        public string Phonenumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }

        [Required]
        [MaxLength(250)]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }


        [MaxLength(250)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        #endregion
    }
}

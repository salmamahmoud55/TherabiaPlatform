﻿namespace therabia.DTO
{
    public class RegisterDTO
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }

        public string role { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}

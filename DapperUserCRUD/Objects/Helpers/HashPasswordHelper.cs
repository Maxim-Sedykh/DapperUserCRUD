﻿using System.Security.Cryptography;
using System.Text;

namespace DapperUserCRUD.Objects.Helpers
{
    public static class HashPasswordHelper
    {
        public static string HashPassword(string password)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }
    }
}

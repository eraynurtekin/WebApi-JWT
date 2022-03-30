﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreatesSecurityKey(string securityKey)
        {
            //Projede birçok yerde kullanabiliriz,bizim içn standart bir kod...
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
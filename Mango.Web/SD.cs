﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Web
{
    public static class SD
    {
        public enum ApiType
        {
            GET,
            POST,
            PATCH,
            DELETE
        }

        public static string SessionToken = "JWTToken";
    }
}

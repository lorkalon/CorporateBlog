using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.DataProtection;

namespace CorporateBlog.WebApi.Authentication
{
    public class DataProtector:IDataProtector
    {
        public byte[] Protect(byte[] userData)
        {
            throw new NotImplementedException();
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            throw new NotImplementedException();
        }
    }
}
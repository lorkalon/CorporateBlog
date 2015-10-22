using System;
using System.Configuration;
using System.Linq;

namespace CorporateBlog.WebApi.Services
{
    public static class ConfigurationManagerService
    {
        public static bool DatabaseCreated
        {
            get
            {
                var values = ConfigurationManager.AppSettings.GetValues("DatabaseCreated");
                
                return values.Any() && Convert.ToBoolean(values.FirstOrDefault());
            }
            set
            {
                ConfigurationManager.AppSettings.Set("DatabaseCreated", value.ToString());
            }
        }

        public static string DefaultAdminName
        {
            get { return ConfigurationManager.AppSettings["DefaultAdminName"]; }
        }

        public static string DefaultAdminPassword
        {
            get { return ConfigurationManager.AppSettings["DefaultAdminPassword"]; }
        }

        public static string SmtpServiceEmail
        {
            get { return ConfigurationManager.AppSettings["smtp:Email"]; }
        }

        public static string SmtpServicePassword
        {
            get { return ConfigurationManager.AppSettings["smtp:Password"]; }
        }

        public static string SmtpServiceHost
        {
            get { return ConfigurationManager.AppSettings["smtp:Host"]; }
        }

        public static int SmtpServicePort
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["smtp:Port"]); }
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;

namespace DVLD_DataAccess
{
    static class Setteing
    {
        public static string ConnectionString { get; private set; }

        static Setteing()
        {
            var config = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

            ConnectionString = config.GetSection("constr").Value;
        }


    }
}

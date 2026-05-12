using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsCountryData
    {
        public enum enGendor { Male = 0, Female = 1 };

        public static bool GetCountryInfoByID(int ID, ref string CountryName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CountryID", SqlDbType.Int) { Value = ID }
            };

            string tmpCountryName = "";

            bool isFound = clsDataHelper.GetSingleRow("SP_GetCountryInfoByID", parameters, reader =>
            {
                tmpCountryName = reader.GetString(reader.GetOrdinal("CountryName"));
            });

            if (isFound)
            {
                CountryName = tmpCountryName;
            }

            return isFound;
        }



        public static bool GetCountryInfoByName(string CountryName, ref int ID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CountryName", SqlDbType.NVarChar) { Value = CountryName }
            };

            int tmpID = 0;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetCountryInfoByName", parameters, reader =>
            {
                tmpID = reader.GetInt32(reader.GetOrdinal("CountryID"));
            });

            if (isFound)
            {
                ID = tmpID;
            }

            return isFound;
        }

        public static async Task<DataTable> GetAllCountries()
        {
            return await clsDataHelper.GetDataTableAsync("SP_GetAllCountries", null);
        }

    }
}

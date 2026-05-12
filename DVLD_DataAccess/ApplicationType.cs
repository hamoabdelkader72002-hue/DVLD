using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using System.Net;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;

namespace DVLD_DataAccess
{
    public class clsApplicationTypeData
    {

        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID,
            ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            
            SqlParameter[] parameters =
            {
                new SqlParameter("@ID", SqlDbType.Int) { Value = ApplicationTypeID }
            };

            string tmpTitle = "";
            float tmpFees = 0f;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetApplicationType", parameters, reader =>
            {
                tmpTitle = reader.GetString(reader.GetOrdinal("ApplicationTypeTitle"));
                tmpFees = (float)reader.GetDecimal(reader.GetOrdinal("ApplicationFees"));
            });

            if(isFound)
            {
                ApplicationTypeTitle = tmpTitle;
                ApplicationFees = tmpFees;
            }

            return isFound;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsDataHelper.GetDataTable("SP_GetAllApplicationTypes", new SqlParameter[] {});
        }

        public static int AddNewApplicationType( string Title, float Fees)
        {

            SqlParameter[] parameters = new SqlParameter[] {

                new SqlParameter("@ApplicationTypeTitle" , SqlDbType.NVarChar){ Value = Title },
                new SqlParameter("@ApplicationFees" , SqlDbType.SmallMoney){ Value = Fees }
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewApplicationType", parameters);

            return (result != null) ? Convert.ToInt32(result) : -1;

        }



        public static bool UpdateApplicationType(int ApplicationTypeID, string Title, float Fees)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) {Value = ApplicationTypeID},
                new SqlParameter("@ApplicationTypeTitle", SqlDbType.NVarChar) {Value = Title},
                new SqlParameter("@ApplicationFees", SqlDbType.SmallMoney) {Value = Fees}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateApplicationType", parameters);

            return rowsAffected > 0;

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {

        public static bool GetDriverInfoByDriverID(int DriverID, 
            ref int PersonID,ref int CreatedByUserID,ref DateTime CreatedDate )
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("DriverID", SqlDbType.Int) {Value = DriverID}
            };

            int tmpPersonID = 0;
            DateTime tmpCreatedDate = DateTime.Today;
            int tmpCreatedByUserID = 0;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetDriver", parameters, reader =>
            {
                tmpPersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                tmpCreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                PersonID = tmpPersonID;
                CreatedDate = tmpCreatedDate;
                CreatedByUserID = tmpCreatedByUserID;
            }
            return isFound;
        }

        public static bool GetDriverInfoByPersonID(int PersonID,ref int DriverID,
            ref int CreatedByUserID,ref DateTime CreatedDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID}
            };

            int tmpDriverID = 0;
            DateTime tmpCreatedDate = DateTime.Today;
            int tmpCreatedByUserID = 0;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetDriverByPersonID", parameters, reader =>
            {
                tmpDriverID = reader.GetInt32(reader.GetOrdinal("DriverID"));
                tmpCreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                DriverID = tmpDriverID;
                CreatedDate = tmpCreatedDate;
                CreatedByUserID = tmpCreatedByUserID;
            }
            return isFound;
        }

        public static async Task<DataTable> GetAllDrivers(CancellationTokenSource cts)
        {
            return await clsDataHelper.GetDataTableAsync("SP_GetAllDriver_view", null);
        }

        public static int AddNewDriver( int PersonID, int CreatedByUserID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("CreatedDate", SqlDbType.DateTime) {Value = DateTime.Today},
                new SqlParameter("UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewDriver", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("DriverID", SqlDbType.Int) {Value = DriverID},
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateDriver", parameter);

            return rowsAffected > 0;
        }

    }
}

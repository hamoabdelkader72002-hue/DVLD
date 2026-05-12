using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseData
    {

        public static bool GetDetainedLicenseInfoByID(int DetainID,
            ref int LicenseID, ref DateTime DetainDate,
            ref float FineFees, ref int CreatedByUserID,
            ref bool IsReleased, ref DateTime ReleaseDate,
            ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DetainID", SqlDbType.Int) { Value = DetainID }
            };

            int tmpLicenseID = 0;
            DateTime tmpDetainDate = DateTime.Today;
            float tmpFineFees = default;
            int tmpCreatedByUserID = 0;
            bool tmpIsReleased = false;
            int tmpReleaseApplicationID = 0;
            DateTime tmpReleaseDate = DateTime.Today;
            int tmpReleasedByUserID = 0;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetDetainedLicense", parameters, reader =>
            {
                tmpLicenseID = reader.GetInt32(reader.GetOrdinal("LicenseID"));
                tmpDetainDate = reader.GetDateTime(reader.GetOrdinal("DetainDate"));
                tmpFineFees = (float) reader.GetDecimal(reader.GetOrdinal("FineFees"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                tmpIsReleased = reader.GetBoolean(reader.GetOrdinal("IsReleased"));
                tmpReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ReleaseApplicationID")) : -1;
                tmpReleaseDate = reader["ReleaseDate"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("ReleaseDate")) : default;
                tmpReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ReleasedByUserID")) : -1;
            });

            if (isFound)
            {
                LicenseID = tmpLicenseID;
                DetainDate = tmpDetainDate;
                FineFees = tmpFineFees;
                ReleaseDate = tmpReleaseDate;
                ReleasedByUserID = tmpReleasedByUserID;
                ReleaseApplicationID = tmpReleaseApplicationID;
                CreatedByUserID = tmpCreatedByUserID;
                IsReleased = tmpIsReleased;
            }

            return isFound;
        }

        
        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID,
         ref int DetainID, ref DateTime DetainDate,
         ref float FineFees, ref int CreatedByUserID,
         ref bool IsReleased, ref DateTime ReleaseDate,
         ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@LicenseID", SqlDbType.Int) { Value = LicenseID }
            };

            int tmpDetainID = 0;
            DateTime tmpDetainDate = DateTime.Today;
            int tmpReleaseApplicationID = 0;
            DateTime tmpReleaseDate = DateTime.Today;
            float tmpFineFees = default;
            int tmpCreatedByUserID = 0;
            int tmpReleasedByUserID = 0;
            bool tmpIsReleased = false;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetDetainedLicenseByLicenseID", parameters, reader =>
            {
                tmpDetainID = reader.GetInt32(reader.GetOrdinal("DetainID"));
                tmpDetainDate = reader.GetDateTime(reader.GetOrdinal("DetainDate"));
                tmpFineFees = (float)reader.GetDecimal(reader.GetOrdinal("FineFees"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                tmpIsReleased = reader.GetBoolean(reader.GetOrdinal("IsReleased"));
                tmpReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ReleaseApplicationID")) : -1;
                tmpReleaseDate = reader["ReleaseDate"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("ReleaseDate")) : default;
                tmpReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ReleasedByUserID")) : -1;
            });

            if (isFound)
            {
                DetainID = tmpDetainID;
                DetainDate = tmpDetainDate;
                FineFees = tmpFineFees;
                ReleaseDate = tmpReleaseDate;
                ReleasedByUserID = tmpReleasedByUserID;
                ReleaseApplicationID = tmpReleaseApplicationID;
                CreatedByUserID = tmpCreatedByUserID;
                IsReleased = tmpIsReleased;
            }
            return isFound;
        }

        public static async Task<DataTable> GetAllDetainedLicenses()
        {
            return await clsDataHelper.GetDataTableAsync("SP_GetAllDetainedLicenses_View", null);
        }

        public static async Task<int> AddNewDetainedLicense(
            int LicenseID,  DateTime DetainDate,
            float FineFees,  int CreatedByUserID)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LicenseID", SqlDbType.Int) {Value = LicenseID},
                new SqlParameter("DetainDate", SqlDbType.DateTime) {Value = DetainDate},
                new SqlParameter("FineFees", SqlDbType.Decimal) {Value = FineFees},
                new SqlParameter("CreatedByUserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            object result = await clsDataHelper.ExecuteScalarAsync("SP_DetainLicense", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdateDetainedLicense(int DetainID, 
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID)
        {

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@DetainID", SqlDbType.Int) {Value = DetainID},
                new SqlParameter("LicenseID", SqlDbType.Int) {Value = LicenseID},
                new SqlParameter("DetainDate", SqlDbType.DateTime) {Value = DetainDate},
                new SqlParameter("FineFees", SqlDbType.Decimal) {Value = FineFees},
                new SqlParameter("CreatedByUserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateDetainedLicense", parameter);

            return rowsAffected > 0;
        }


        public static bool ReleaseDetainedLicense(int DetainID,
                 int ReleasedByUserID, int ReleaseApplicationID)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@DetainID", SqlDbType.Int) {Value = DetainID},
                new SqlParameter("@ReleaseApplicationID", SqlDbType.Int) {Value = ReleaseApplicationID},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = ReleasedByUserID},
                new SqlParameter("@ReleaseDate", SqlDbType.Date) {Value = DateTime.Today}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_ReleaseDetainedLicense2", parameter);

            return rowsAffected > 0;
        }


        public static bool ReleaseDetainedLicense(int PersonID, DateTime ApplicationDate, int AppTypeID, byte AppStatus, DateTime LastStatusDate, decimal PaidFees, int UserID ,int DetainID, DateTime ReleaseDate)
        {

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("ApplicationDate", SqlDbType.Date) {Value = ApplicationDate},
                new SqlParameter("ApplicationTypeID", SqlDbType.Int) {Value = AppTypeID},
                new SqlParameter("ApplicationStatus", SqlDbType.TinyInt) {Value = AppStatus},
                new SqlParameter("LastStatusDate", SqlDbType.Date) {Value = LastStatusDate},
                new SqlParameter("PaidFees", SqlDbType.SmallMoney) {Value = PaidFees},
                new SqlParameter("DetainID", SqlDbType.Int) {Value = DetainID},
                new SqlParameter("ReleaseDate", SqlDbType.Date) {Value = ReleaseDate},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = UserID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_ReleaseLicense", parameter);

            return rowsAffected > 0;
        }



        public static bool IsLicenseDetained(int LicenseID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LicenseID", SqlDbType.Int) {Value = LicenseID}
            };

            object result =  clsDataHelper.ExecuteScalar("SP_IsLicenseDetained", parameters);

            return result != null ? Convert.ToBoolean(result) : false;
        }

    }
}

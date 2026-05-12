using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsInternationalLicenseData
    {

        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID, 
            ref int ApplicationID, 
            ref int DriverID, ref int IssuedUsingLocalLicenseID, 
            ref DateTime IssueDate, ref DateTime ExpirationDate,ref bool IsActive, ref int CreatedByUserID)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("InternationalLicenseID", SqlDbType.Int) {Value = InternationalLicenseID}
            };

            int tmpApplicationID = 0;
            int tmpDriverID = 0;
            int tmpIssuedUsingLocalLicenseID = 0;
            DateTime tmpIssueDate = DateTime.Today;
            DateTime tmpExpirationDate = DateTime.Today;
            bool tmpIsActive = false;
            int tmpCreatedByUserID = 0;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetInternationalLicense", parameters, reader =>
            {
                tmpApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID"));
                tmpDriverID = reader.GetInt32(reader.GetOrdinal("DriverID"));
                tmpIssuedUsingLocalLicenseID = reader.GetInt32(reader.GetOrdinal("IssuedUsingLocalLicenseID"));
                tmpIssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                tmpExpirationDate = reader.GetDateTime(reader.GetOrdinal("ExpirationDate"));
                tmpIsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                ApplicationID = tmpApplicationID;
                DriverID = tmpDriverID;
                IssuedUsingLocalLicenseID = tmpIssuedUsingLocalLicenseID;
                IssueDate = tmpIssueDate;
                ExpirationDate = tmpExpirationDate;
                IsActive = tmpIsActive;
                CreatedByUserID = tmpCreatedByUserID;
            }
            return isFound;
        }

        public static async Task<DataTable> GetAllInternationalLicenses(CancellationTokenSource cts)
        {
            return await clsDataHelper.GetDataTableAsync("SP_GetAllInternationalLicense", null);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return  clsDataHelper.GetDataTable("SP_GetDriverInternationalLicenses", new SqlParameter[] { new SqlParameter("@DriverID", SqlDbType.Int) { Value = DriverID } });
        }


        public static int AddNewInternationalLicense( int ApplicationID,
             int DriverID,  int IssuedUsingLocalLicenseID,
             DateTime IssueDate,  DateTime ExpirationDate, bool IsActive,  int CreatedByUserID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
                new SqlParameter("DriverID", SqlDbType.Int) {Value = DriverID},
                new SqlParameter("@IssueUsingLDLID", SqlDbType.Int) {Value = IssuedUsingLocalLicenseID},
                new SqlParameter("IssueDate", SqlDbType.Date) {Value = IssueDate},
                new SqlParameter("ExpirationDate", SqlDbType.Date) {Value = ExpirationDate},
                new SqlParameter("IsActive", SqlDbType.Bit) {Value = IsActive},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewInternationalLicense2", parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }




        public static int AddNewInternationalLicense(int PersonID, DateTime ApplicationDate, int ApptypeID, short AppStatus, DateTime LastStatusDate, decimal PaidFees, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("DriverID", SqlDbType.Int) {Value = DriverID},
                new SqlParameter("@ApplicationTypeID", SqlDbType.Int) {Value = ApptypeID},
                new SqlParameter("@IssueUsingLDLID", SqlDbType.Int) {Value = IssuedUsingLocalLicenseID},
                new SqlParameter("@ApplicationStatus", SqlDbType.TinyInt) {Value = AppStatus},
                new SqlParameter("LastStatusDate", SqlDbType.TinyInt) {Value = LastStatusDate},
                new SqlParameter("PaidFees", SqlDbType.SmallMoney) {Value = PaidFees},
                new SqlParameter("ApplicationDate", SqlDbType.Date) {Value = ApplicationDate},
                new SqlParameter("IssueDate", SqlDbType.Date) {Value = IssueDate},
                new SqlParameter("ExpirationDate", SqlDbType.Date) {Value = ExpirationDate},
                new SqlParameter("IsActive", SqlDbType.Bit) {Value = IsActive},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewInternationalLicense", parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }



        public static bool UpdateInternationalLicense(
              int InternationalLicenseID , int ApplicationID,
             int DriverID, int IssuedUsingLocalLicenseID,
             DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("InternationalLicenseID", SqlDbType.Int) {Value = InternationalLicenseID},
                new SqlParameter("DriverID", SqlDbType.Int) {Value = DriverID},
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
                new SqlParameter("@IssueUsingLDLID", SqlDbType.Int) {Value = IssuedUsingLocalLicenseID},
                new SqlParameter("IssueDate", SqlDbType.Date) {Value = IssueDate},
                new SqlParameter("ExpirationDate", SqlDbType.Date) {Value = ExpirationDate},
                new SqlParameter("IsActive", SqlDbType.Bit) {Value = IsActive},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateInternationalLicense", parameter);

            return rowsAffected > 0;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("DriverID", SqlDbType.Int) {Value = DriverID},
            };

            object result = clsDataHelper.ExecuteScalar("SP_GetActiveInternationalLicenseIDByDriverID", parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

    }
}

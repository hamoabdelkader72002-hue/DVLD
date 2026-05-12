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
    public class clsLicenseData
    {

        public static bool GetLicenseInfoByID(int LicenseID,ref int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate,ref string Notes,
            ref float PaidFees,ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LicenseID", SqlDbType.Int) {Value = LicenseID}
            };

            int tmpApplicationID = 0;
            int tmpDriverID = 0;
            int tmpLicenseClass = 0;
            DateTime tmpIssueDate = DateTime.Today;
            DateTime tmpExpirationDate = DateTime.Today;
            int tmpCreatedByUserID = 0;
            float tmpPaidFees = 0;
            byte tmpIssueReason = 0;
            bool tmpIsActive = false;
            string tmpNotes = "";

            bool isFound = clsDataHelper.GetSingleRow("SP_GetLicense", parameters, reader =>
            {
                tmpApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID"));
                tmpDriverID = reader.GetInt32(reader.GetOrdinal("DriverID"));
                tmpLicenseClass = reader.GetInt32(reader.GetOrdinal("LicenseClass"));
                tmpIssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                tmpExpirationDate = reader.GetDateTime(reader.GetOrdinal("ExpirationDate"));
                tmpNotes = reader["Notes"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("Notes")) : "";
                tmpPaidFees = (float)reader.GetDecimal(reader.GetOrdinal("PaidFees"));
                tmpIsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                tmpIssueReason = reader.GetByte(reader.GetOrdinal("IssueReason"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                ApplicationID = tmpApplicationID;
                DriverID = tmpDriverID;
                LicenseClass = tmpLicenseClass;
                IssueDate = tmpIssueDate;
                ExpirationDate = tmpExpirationDate;
                Notes = tmpNotes;
                PaidFees = tmpPaidFees;
                IsActive = tmpIsActive;
                IssueReason = tmpIssueReason;
                CreatedByUserID = tmpCreatedByUserID;
            }
            return isFound;
        }

        public static DataTable GetAllLicenses()
        {
            return clsDataHelper.GetDataTable("SP_GetAllLicenses", null);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsDataHelper.GetDataTable("SP_GetDriverLicenses", new SqlParameter[] {new SqlParameter("DriverID", DbType.Int64) { Value = DriverID} });
        }

        public static int AddNewLicense( int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,  string Notes, float PaidFees, bool IsActive,byte IssueReason, int CreatedByUserID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
                new SqlParameter("DriverID", SqlDbType.Int) {Value = DriverID},
                new SqlParameter("LicenseClass", SqlDbType.Int) {Value = LicenseClass},
                new SqlParameter("IssueDate", SqlDbType.Date) {Value = IssueDate},
                new SqlParameter("ExpirationDate", SqlDbType.Date) {Value = ExpirationDate},
                new SqlParameter("Notes", SqlDbType.NVarChar) {Value = Notes},
                new SqlParameter("PaidFees", SqlDbType.SmallMoney) {Value = PaidFees},
                new SqlParameter("IsActive", SqlDbType.Bit) {Value = IsActive},
                new SqlParameter("IssueReason", SqlDbType.TinyInt) {Value = IssueReason},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewLicense", parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdateLicense(int LicenseID ,int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate, string Notes,
             float PaidFees, bool IsActive,byte IssueReason, int CreatedByUserID)
        {

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("LicenseID", SqlDbType.Int) {Value = LicenseID},
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
                new SqlParameter("DriverID", SqlDbType.Int) {Value = DriverID},
                new SqlParameter("LicenseClass", SqlDbType.Int) {Value = LicenseClass},
                new SqlParameter("IssueDate", SqlDbType.DateTime) {Value = IssueDate},
                new SqlParameter("ExpirationDate", SqlDbType.DateTime) {Value = ExpirationDate},
                new SqlParameter("Notes", SqlDbType.NVarChar) {Value = Notes},
                new SqlParameter("PaidFees", SqlDbType.Decimal) {Value = PaidFees},
                new SqlParameter("IsActive", SqlDbType.Bit) {Value = IsActive},
                new SqlParameter("IssueReason", SqlDbType.TinyInt) {Value = IssueReason},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateLicense", parameter);

            return rowsAffected > 0;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID,int LicenseClassID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LicenseClass", SqlDbType.Int) {Value = LicenseClassID},
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_GetActiveLicenseIDByPersonID", parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool DeactivateLicense(int LicenseID)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("LicenseID", SqlDbType.Int) {Value = LicenseID},
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_DeactivateLicense", parameter);

            return rowsAffected > 0;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {


        public static bool GetApplicationInfoByID(int ApplicationID,
            ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref float PaidFees, ref int CreatedByUserID)
        {

            SqlParameter[] parameters =
            {
                new SqlParameter("@ID", SqlDbType.Int) { Value = ApplicationID }
            };

            int tmpID = 0;
            DateTime tmpApplicationDate = DateTime.Today;
            int tmpApplicationTypeID = 0;
            byte tmpApplicationStatus = 0;
            DateTime tmpLastStatusDate = DateTime.Today;
            float tmpPaidFees = default;
            int tmpCreatedByUserID = 0;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetApplication", parameters, reader =>
            {
                tmpID = reader.GetInt32(reader.GetOrdinal("ApplicantPersonID"));
                tmpApplicationDate = reader.GetDateTime(reader.GetOrdinal("ApplicationDate"));
                tmpApplicationTypeID = reader.GetInt32(reader.GetOrdinal("ApplicationTypeID"));
                tmpApplicationStatus = reader.GetByte(reader.GetOrdinal("ApplicationStatus"));
                tmpLastStatusDate = reader.GetDateTime(reader.GetOrdinal("LastStatusDate"));
                tmpPaidFees =(float) reader.GetDecimal(reader.GetOrdinal("PaidFees"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                ApplicantPersonID = tmpID;
                ApplicationDate = tmpApplicationDate;
                ApplicationTypeID = tmpApplicationTypeID;
                ApplicationStatus = tmpApplicationStatus;
                LastStatusDate = tmpLastStatusDate;
                PaidFees = tmpPaidFees;
                CreatedByUserID = tmpCreatedByUserID;
            }

            return isFound;
        }

        public static DataTable GetAllApplications()
        {
            return clsDataHelper.GetDataTable("sp_GetAllApplications", null);
        }   

        public static int AddNewApplication( int ApplicantPersonID,  DateTime ApplicationDate,  int ApplicationTypeID,
             byte ApplicationStatus,  DateTime LastStatusDate,
             float PaidFees,  int CreatedByUserID)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = ApplicantPersonID},
                new SqlParameter("ApplicationDate", SqlDbType.DateTime) {Value = ApplicationDate},
                new SqlParameter("ApplicationTypeID", SqlDbType.Int) {Value = ApplicationTypeID},
                new SqlParameter("ApplicationStatus", SqlDbType.TinyInt) {Value = ApplicationStatus},
                new SqlParameter("LastStatusDate", SqlDbType.DateTime) {Value = LastStatusDate},
                new SqlParameter("PaidFees", SqlDbType.SmallMoney) {Value = PaidFees},
                new SqlParameter("UserID", SqlDbType.Int) {Value = CreatedByUserID},
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewApplication", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }


        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
             byte ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)
        {

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
                new SqlParameter("ApplicantPersonID", SqlDbType.Int) {Value = ApplicantPersonID},
                new SqlParameter("ApplicationDate", SqlDbType.DateTime) {Value = ApplicationDate},
                new SqlParameter("ApplicationTypeID", SqlDbType.Int) {Value = ApplicationTypeID},
                new SqlParameter("ApplicationStatus", SqlDbType.TinyInt) {Value = ApplicationStatus},
                new SqlParameter("LastStatusDate", SqlDbType.DateTime) {Value = LastStatusDate},
                new SqlParameter("PaidFees", SqlDbType.Decimal) {Value = PaidFees},
                new SqlParameter("CreatedByUserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateApplication", parameter);

            return rowsAffected > 0;
        }

        public static bool DeleteApplication(int ApplicationID)
        {

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_DeleteApplication", parameter);

            return rowsAffected > 0;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsDataHelper.GetDataTable("SP_IsApplicationExist", null).HasData();
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
           //incase the ActiveApplication ID !=-1 return true.
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) !=-1);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ApplicantPersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("ApplicationTypeID", SqlDbType.Int) {Value = ApplicationTypeID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_GetActiveApplicationID", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID,int LicenseClassID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ApplicantPersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("ApplicationTypeID", SqlDbType.Int) {Value = ApplicationTypeID},
                new SqlParameter("LicenseClassID", SqlDbType.Int) {Value = LicenseClassID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_GetActiveApplicationIDForLicenseClass", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }
      
        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
                new SqlParameter("NewStatus", SqlDbType.TinyInt) {Value = NewStatus},
                new SqlParameter("LastStatusDate", SqlDbType.Date) {Value = DateTime.Today},
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateStatus", parameter);

            return rowsAffected > 0;
        }

    }
}

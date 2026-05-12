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
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {

        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
            ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("TestAppointmentID", SqlDbType.Int) {Value = TestAppointmentID}
            };

            int tmpTestTypeID = -1;
            int tmpLocalDrivingLicenseApplicationID = -1;
            DateTime tmpAppointmentDate = DateTime.Today;
            float tmpPaidFees = default;
            int tmpRetakeTestApplicationID = -1;
            bool tmpIsLocked = false;
            int tmpCreatedByUserID = -1;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetTestAppointment", parameters, reader =>
            {
                tmpTestTypeID = reader.GetInt32(reader.GetOrdinal("TestTypeID"));
                tmpLocalDrivingLicenseApplicationID = reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID"));
                tmpAppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                tmpPaidFees = (float)reader.GetDecimal(reader.GetOrdinal("PaidFees"));
                tmpRetakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID")) : -1;
                tmpIsLocked = reader.GetBoolean(reader.GetOrdinal("IsLocked"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                TestTypeID = tmpTestTypeID;
                LocalDrivingLicenseApplicationID = tmpLocalDrivingLicenseApplicationID;
                AppointmentDate = tmpAppointmentDate;
                PaidFees = tmpPaidFees;
                RetakeTestApplicationID = tmpRetakeTestApplicationID;
                IsLocked = tmpIsLocked;
                CreatedByUserID = tmpCreatedByUserID;
            }
            return isFound;
        }

        public static bool GetLastTestAppointment( 
             int LocalDrivingLicenseApplicationID,  int TestTypeID, 
            ref int TestAppointmentID,ref DateTime AppointmentDate,
            ref float PaidFees, ref int CreatedByUserID,ref bool IsLocked,ref int RetakeTestApplicationID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID},
                new SqlParameter("LocalDrivingLicenseApplicationID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
            };

            int tmpTestAppointmentID = -1;
            int tmpLocalDrivingLicenseApplicationID = -1;
            DateTime tmpAppointmentDate = DateTime.Today;
            float tmpPaidFees = default;
            int tmpRetakeTestApplicationID = -1;
            bool tmpIsLocked = false;
            int tmpCreatedByUserID = -1;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetLastTestAppointment", parameters, reader =>
            {
                tmpTestAppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID"));
                tmpLocalDrivingLicenseApplicationID = reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID"));
                tmpAppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                tmpPaidFees = (float)reader.GetDecimal(reader.GetOrdinal("PaidFees"));
                tmpRetakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID")) : -1;
                tmpIsLocked = reader.GetBoolean(reader.GetOrdinal("IsLocked"));
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                TestAppointmentID = tmpTestAppointmentID;
                LocalDrivingLicenseApplicationID = tmpLocalDrivingLicenseApplicationID;
                AppointmentDate = tmpAppointmentDate;
                PaidFees = tmpPaidFees;
                RetakeTestApplicationID = tmpRetakeTestApplicationID;
                IsLocked = tmpIsLocked;
                CreatedByUserID = tmpCreatedByUserID;
            }
            return isFound;
        }

        public static async Task<DataTable> GetAllTestAppointments()
        {
            return await clsDataHelper.GetDataTableAsync("SP_GetAllTestAppointments", null);
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID},
                new SqlParameter("LocalDrivingLicenseApplicationID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
            };

            return clsDataHelper.GetDataTable("SP_GetApplicationTestAppointmentsPerTestType", parameters);
        }

        public static int AddNewTestAppointment(
             int TestTypeID,  int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate,  float PaidFees,  int CreatedByUserID,int RetakeTestApplicationID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TestTypeID", SqlDbType.Int) {Value = TestTypeID},
                new SqlParameter("@LDLAppID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
                new SqlParameter("@AppointmentDate", SqlDbType.Date) {Value = AppointmentDate},
                new SqlParameter("@PaidFees", SqlDbType.SmallMoney) {Value = PaidFees},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = CreatedByUserID},
                new SqlParameter("@IsLocked", SqlDbType.Bit) {Value = 0},
                new SqlParameter("@RetakeTestAppID", SqlDbType.Int) {Value = RetakeTestApplicationID == -1 ? (object)DBNull.Value : RetakeTestApplicationID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewTestAppointment", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID,  int TestTypeID,  int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate,  float PaidFees, 
             int CreatedByUserID,bool IsLocked,int RetakeTestApplicationID)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("TestAppointmentID", SqlDbType.Int) {Value = TestAppointmentID},
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID},
                new SqlParameter("@LDLAppID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
                new SqlParameter("AppointmentDate", SqlDbType.SmallDateTime) {Value = AppointmentDate},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = CreatedByUserID},
                new SqlParameter("PaidFees", SqlDbType.SmallMoney) {Value = PaidFees},
                new SqlParameter("IsLocked", SqlDbType.Bit) {Value = IsLocked},
                new SqlParameter("@RetakeTestAppID", SqlDbType.Int) {Value = RetakeTestApplicationID == -1 ? (object)DBNull.Value : RetakeTestApplicationID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateTestAppointment", parameter);

            return rowsAffected > 0;
        }


        public static int GetTestID(int TestAppointmentID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("TestAppointmentID", SqlDbType.Int) {Value = TestAppointmentID},
            };

            object result = clsDataHelper.ExecuteScalar("SP_GetTestID", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }

    }
}

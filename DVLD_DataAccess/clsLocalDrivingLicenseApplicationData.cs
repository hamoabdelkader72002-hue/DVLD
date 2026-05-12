using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
      
        public static bool GetLocalDrivingLicenseApplicationInfoByID(
            int LocalDrivingLicenseApplicationID, ref int ApplicationID, 
            ref int LicenseClassID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LDLAppID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID}
            };

            int tmpApplicationID = 0;
            int tmpLicenseClassID = 0;

            bool isFound = clsDataHelper.GetSingleRow("GetLocalDrivingLicenseApplication", parameters, reader =>
            {
                tmpApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID"));
                tmpLicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID"));
            });

            if (isFound)
            {
                ApplicationID = tmpApplicationID;
                LicenseClassID = tmpLicenseClassID;
            }

            return isFound;
        }

        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(
         int ApplicationID, ref int LocalDrivingLicenseApplicationID, 
         ref int LicenseClassID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID}
            };

            int tmpLocalDrivingLicenseApplicationID = 0;
            int tmpLicenseClassID = 0;

            bool isFound = clsDataHelper.GetSingleRow("GetLocalDrivingLicenseApplicationByAppID", parameters, reader =>
            {
                tmpLocalDrivingLicenseApplicationID = reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID"));
                tmpLicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID"));
            });

            if (isFound)
            {
                LocalDrivingLicenseApplicationID = tmpLocalDrivingLicenseApplicationID;
                LicenseClassID = tmpLicenseClassID;
            }

            return isFound;
        }

        public static async Task<DataTable> GetAllLocalDrivingLicenseApplications(CancellationTokenSource cts)
        {

            return await clsDataHelper.GetDataTableAsync("GetAllLocalDrivingLicenseApplication", null);
        }

        public static int AddNewLocalDrivingLicenseApplication(
            int ApplicationID, int LicenseClassID )
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
                new SqlParameter("LicenseClassID", SqlDbType.Int) {Value = LicenseClassID},
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewLDLApp2", parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }



        public static int AddNewLocalDrivingLicenseApplication(int PersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatusID, DateTime LastStatusDate, decimal PaidFees, int UserID, int LicenseClassID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("ApplicationDate", SqlDbType.Int) {Value = ApplicationDate},
                new SqlParameter("ApplicationTypeID", SqlDbType.Int) {Value = ApplicationTypeID},
                new SqlParameter("ApplicationStatusID", SqlDbType.Int) {Value = ApplicationStatusID},
                new SqlParameter("LastStatusDate", SqlDbType.Int) {Value = LastStatusDate},
                new SqlParameter("PaidFees", SqlDbType.Int) {Value = PaidFees},
                new SqlParameter("UserID", SqlDbType.Int) {Value = UserID},
                new SqlParameter("LicenseClassID", SqlDbType.Int) {Value = LicenseClassID},
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewLDLApp", parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }


        public static bool UpdateLocalDrivingLicenseApplication(
            int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LocalDrivingLicenseApplicationID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
                new SqlParameter("ApplicationID", SqlDbType.Int) {Value = ApplicationID},
                new SqlParameter("LicenseClassID", SqlDbType.Int) {Value = LicenseClassID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("UpdateLocalDrivingLicenseApplication", parameters);

            return rowsAffected > 0;
        }


        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LocalDrivingLicenseApplicationID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_DeleteLDLApp", parameters);

            return rowsAffected > 0;
        }

        public static bool DoesPassTestType( int LocalDrivingLicenseApplicationID, int TestTypeID)

        {
            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("LocalDrivingLicenseApplicationID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID},
             };

            object result = clsDataHelper.ExecuteScalar("DoesPassTestType", parameters);
            return result != null ? Convert.ToBoolean(result) : false;
        }

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LocalDrivingLicenseApplicationID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID},
            };

            object result = clsDataHelper.ExecuteScalar("SP_DoesAttendTestType", parameters);
            return result != null ? Convert.ToBoolean(result) : false;
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("LocalDrivingLicenseApplicationID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID},
             };

            object result = clsDataHelper.ExecuteScalar("SP_TotalTrialsPerTest", parameters);
            return result != null ? Convert.ToByte(result) : default;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LocalDrivingLicenseApplicationID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID},
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID},
            };

            object result = clsDataHelper.ExecuteScalar("SP_IsThereAnActiveScheduledTest", parameters);
            return result != null ? Convert.ToBoolean(result) : false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsTestData
    {

        public static bool GetTestInfoByID(int TestID,
            ref int TestAppointmentID, ref bool TestResult,
            ref string Notes, ref int CreatedByUserID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("TestID", SqlDbType.Int) {Value = TestID}
            };

            int tmpTestAppointmentID = -1;
            bool tmpTestResult = false;
            string tmpNotes = "";
            int tmpCreatedByUserID = -1;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetTest", parameters, reader =>
            {
                tmpTestAppointmentID = reader.GetInt32(reader.GetOrdinal("ID"));
                tmpTestResult = reader.GetBoolean(reader.GetOrdinal("TestResult"));
                tmpNotes = reader["Notes"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("Notes")) : "";
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                TestAppointmentID = tmpTestAppointmentID;
                TestResult = tmpTestResult;
                Notes = tmpNotes;
                CreatedByUserID = tmpCreatedByUserID;
            }
            return isFound;
        }


        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
            (int PersonID,int LicenseClassID,int TestTypeID, ref int TestID,
              ref int TestAppointmentID, ref bool TestResult,
              ref string Notes, ref int CreatedByUserID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("LicenseClassID", SqlDbType.Int) {Value = LicenseClassID},
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID}
            };

            int tmpTestID = -1;
            int tmpTestAppointmentID = -1;
            bool tmpTestResult = false;
            string tmpNotes = "";
            int tmpCreatedByUserID = -1;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetLastTestByPersonAndTestTypeAndLicenseClass", parameters, reader =>
            {
                tmpTestID = reader.GetInt32(reader.GetOrdinal("TestID"));
                tmpTestAppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID"));
                tmpTestResult = reader.GetBoolean(reader.GetOrdinal("TestResult"));
                tmpNotes = reader["Notes"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("Notes")) : "";
                tmpCreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
            });

            if (isFound)
            {
                TestID = tmpTestID;
                TestAppointmentID = tmpTestAppointmentID;
                TestResult = tmpTestResult;
                Notes = tmpNotes;
                CreatedByUserID = tmpCreatedByUserID;
            }
            return isFound;
        }


        public static async Task<DataTable> GetAllTests()
        {
            return await clsDataHelper.GetDataTableAsync("SP_GetAllTests", null);
        }

        public static int AddNewTest( int TestAppointmentID,  bool TestResult,
             string Notes,  int CreatedByUserID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("TestAppointmentID", SqlDbType.Int) {Value = TestAppointmentID},
                new SqlParameter("TestResult", SqlDbType.Bit) {Value = TestResult},
                new SqlParameter("Notes", SqlDbType.NVarChar) {Value = Notes},
                new SqlParameter("UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewTest", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult,
             string Notes, int CreatedByUserID)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("TestID", SqlDbType.Int) {Value = TestID},
                new SqlParameter("TestAppointmentID", SqlDbType.Int) {Value = TestAppointmentID},
                new SqlParameter("TestResult", SqlDbType.Bit) {Value = TestResult},
                new SqlParameter("Notes", SqlDbType.NVarChar) {Value = Notes},
                new SqlParameter("@UserID", SqlDbType.Int) {Value = CreatedByUserID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateTest", parameter);

            return rowsAffected > 0;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LDLAppID", SqlDbType.Int) {Value = LocalDrivingLicenseApplicationID}
            };

            object result = clsDataHelper.ExecuteScalar("SP_GetPassTestCount", parameters);

            return result != null ? Convert.ToByte(result) : default;
        }
    }
}

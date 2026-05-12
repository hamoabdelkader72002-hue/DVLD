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

namespace DVLD_DataAccess
{
    public class clsTestTypeData
    {

        public static bool GetTestTypeInfoByID(int TestTypeID,
            ref string TestTypeTitle, ref string TestDescription, ref float TestFees)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", SqlDbType.Int) {Value = TestTypeID}
            };

            string tmpTestTypeTitle = "";
            string tmpTestDescription = "";
            float tmpTestFees = default;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetTestType", parameters, reader =>
            {
                tmpTestTypeTitle = reader.GetString(reader.GetOrdinal("TestTypeTitle"));
                tmpTestDescription = reader.GetString(reader.GetOrdinal("TestTypeDescription"));
                tmpTestFees = (float)reader.GetDecimal(reader.GetOrdinal("TestTypeFees"));
            });

            if (isFound)
            {
                TestTypeTitle = tmpTestTypeTitle;
                TestDescription = tmpTestDescription;
                TestFees = tmpTestFees;
            }
            return isFound;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsDataHelper.GetDataTable("SP_GetAllTestTypes", null);
        }

        public static int AddNewTestType( string Title,string Description, float Fees)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("TestTypeTitle", SqlDbType.NVarChar) {Value = Title},
                new SqlParameter("TestTypeDescription", SqlDbType.NVarChar) {Value = Description},
                new SqlParameter("@ApplicationFees", SqlDbType.SmallMoney) {Value = Fees}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewTestType", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdateTestType(int TestTypeID,string Title,string Description, float Fees)
        {

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("TestTypeID", SqlDbType.Int) {Value = TestTypeID},
                new SqlParameter("TestTypeTitle", SqlDbType.NVarChar) {Value = Title},
                new SqlParameter("@TestTypeDesc", SqlDbType.NVarChar) {Value = Description},
                new SqlParameter("@TestTypeFees", SqlDbType.SmallMoney) {Value = Fees}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateTestType", parameter);

            return rowsAffected > 0;
        }
    }
}

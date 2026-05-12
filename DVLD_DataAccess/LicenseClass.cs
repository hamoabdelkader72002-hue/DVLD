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
    public class clsLicenseClassData
    {

        public static bool GetLicenseClassInfoByID(int LicenseClassID,
            ref string ClassName, ref string ClassDescription, ref byte MinimumAllowedAge,
            ref byte DefaultValidityLength, ref float ClassFees)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("LicenseClassID", SqlDbType.Int) {Value = LicenseClassID}
            };

            string tmpClassName = "";
            string tmpClassDescription = default;
            byte tmpMinimumAllowedAge = default;
            byte tmpDefaultValidityLength = default;
            float tmpClassFees = default;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetLicenseClass", parameters, reader =>
            {
                tmpClassName = reader.GetString(reader.GetOrdinal("ClassName"));
                tmpClassDescription = reader.GetString(reader.GetOrdinal("ClassDescription"));
                tmpMinimumAllowedAge = reader.GetByte(reader.GetOrdinal("MinimumAllowedAge"));
                tmpDefaultValidityLength = reader.GetByte(reader.GetOrdinal("DefaultValidityLength"));
                tmpClassFees = (float)reader.GetDecimal(reader.GetOrdinal("ClassFees"));
            });

            if (isFound)
            {
                ClassName = tmpClassName;
                ClassDescription = tmpClassDescription;
                MinimumAllowedAge = tmpMinimumAllowedAge;
                DefaultValidityLength = tmpDefaultValidityLength;
                ClassFees = tmpClassFees;
            }
            return isFound;
        }


        public static bool GetLicenseClassInfoByClassName( string ClassName, ref int LicenseClassID,
            ref string ClassDescription, ref byte MinimumAllowedAge,
           ref byte DefaultValidityLength, ref float ClassFees)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ClassName", SqlDbType.NVarChar) {Value = ClassName}
            };

            int tmpLicenseClassID = 0;
            string tmpClassName = "";
            string tmpClassDescription = default;
            byte tmpMinimumAllowedAge = default;
            byte tmpDefaultValidityLength = default;
            float tmpClassFees = default;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetLicenseClassByClassName", parameters, reader =>
            {
                tmpLicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID"));
                tmpClassName = reader.GetString(reader.GetOrdinal("ClassName"));
                tmpClassDescription = reader.GetString(reader.GetOrdinal("ClassDescription"));
                tmpMinimumAllowedAge = reader.GetByte(reader.GetOrdinal("MinimumAllowedAge"));
                tmpDefaultValidityLength = reader.GetByte(reader.GetOrdinal("DefaultValidityLength"));
                tmpClassFees = (float)reader.GetDecimal(reader.GetOrdinal("ClassFees"));
            });

            if (isFound)
            {
                LicenseClassID = tmpLicenseClassID;
                ClassName = tmpClassName;
                ClassDescription = tmpClassDescription;
                MinimumAllowedAge = tmpMinimumAllowedAge;
                DefaultValidityLength = tmpDefaultValidityLength;
                ClassFees = tmpClassFees;
            }
            return isFound;
        }



        public static DataTable GetAllLicenseClasses()
        {
            return clsDataHelper.GetDataTable("SP_GetLicenseClasses", null);
        }

        public static int AddNewLicenseClass(string ClassName, string ClassDescription,
            byte MinimumAllowedAge,byte DefaultValidityLength, float ClassFees)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ClassName", SqlDbType.NVarChar) {Value = ClassName},
                new SqlParameter("ClassDescription", SqlDbType.NVarChar) {Value = ClassDescription},
                new SqlParameter("MinimumAllowedAge", SqlDbType.Bit) {Value = MinimumAllowedAge},
                new SqlParameter("DefaultValidityLength", SqlDbType.Bit) {Value = DefaultValidityLength},
                new SqlParameter("ClassFees", SqlDbType.Decimal) {Value = ClassFees}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewLicenseClass", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName, 
            string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("LicenseClassID", SqlDbType.Int) {Value = LicenseClassID},
                new SqlParameter("ClassName", SqlDbType.NVarChar) {Value = ClassName},
                new SqlParameter("ClassDescription", SqlDbType.NVarChar) {Value = ClassDescription},
                new SqlParameter("MinimumAllowedAge", SqlDbType.Bit) {Value = MinimumAllowedAge},
                new SqlParameter("DefaultValidityLength", SqlDbType.Bit) {Value = DefaultValidityLength},
                new SqlParameter("ClassFees", SqlDbType.Decimal) {Value = ClassFees}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdatePerson", parameter);

            return rowsAffected > 0;
        }
    }
}

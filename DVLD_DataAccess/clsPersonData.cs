using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
       
        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string SecondName,
          ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth,
           ref short Gendor,ref string Address,  ref string Phone, ref string Email,
           ref int NationalityCountryID, ref string ImagePath)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ID", SqlDbType.Int) {Value = PersonID}
            };

            string tmpFirstName = "";
            string tmpSecondName = "";
            string tmpThirdName = "";
            string tmpLastName = "";
            string tmpNationalNo = "";
            DateTime tmpDateOfBirth = DateTime.Today;
            byte tmpGendor = 0;
            string tmpAddress = "";
            string tmpPhone = "";
            string tmpEmail = "";
            int tmpNationalityCountryID = 0;
            string tmpImagePath = "";

            bool isFound = clsDataHelper.GetSingleRow("SP_GetPerson", parameters, reader =>
            {
                tmpFirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                tmpSecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                tmpThirdName = reader["ThirdName"] !=  DBNull.Value ? reader.GetString(reader.GetOrdinal("ThirdName")) : "";
                tmpLastName = reader.GetString(reader.GetOrdinal("LastName"));
                tmpNationalNo = reader.GetString(reader.GetOrdinal("NationalNo"));
                tmpDateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                tmpGendor = reader.GetByte(reader.GetOrdinal("Gendor"));
                tmpAddress = reader.GetString(reader.GetOrdinal("Address"));
                tmpPhone = reader.GetString(reader.GetOrdinal("Phone"));
                tmpEmail = reader["Email"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("Email")) : "";
                tmpNationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                tmpImagePath = reader["ImagePath"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("ImagePath")) : "";
            });

            if (isFound)
            {
                FirstName = tmpFirstName;
                SecondName = tmpSecondName;
                ThirdName = tmpThirdName;
                LastName = tmpLastName;
                NationalNo = tmpNationalNo;
                DateOfBirth = tmpDateOfBirth;
                Gendor = tmpGendor;
                Address = tmpAddress;
                Phone = tmpPhone;
                Email = tmpEmail;
                NationalityCountryID = tmpNationalityCountryID;
                ImagePath = tmpImagePath;
            }
            return isFound;
        }


        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName,   ref DateTime DateOfBirth,
         ref short Gendor,ref string Address, ref string Phone, ref string Email,
         ref int NationalityCountryID, ref string ImagePath)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("NationalNo", SqlDbType.NVarChar) {Value = NationalNo}
            };

            int tmpPersonID = 0;
            string tmpFirstName = "";
            string tmpSecondName = "";
            string tmpThirdName = "";
            string tmpLastName = "";
            DateTime tmpDateOfBirth = DateTime.Today;
            byte tmpGendor = 0;
            string tmpAddress = "";
            string tmpPhone = "";
            string tmpEmail = "";
            int tmpNationalityCountryID = 0;
            string tmpImagePath = "";

            bool isFound = clsDataHelper.GetSingleRow("SP_GetPersonByNationalNo", parameters, reader =>
            {
                tmpPersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                tmpFirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                tmpSecondName = reader.GetString(reader.GetOrdinal("SecondName"));
                tmpThirdName = reader["ThirdName"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("ThirdName")) : "";
                tmpLastName = reader.GetString(reader.GetOrdinal("LastName"));
                tmpDateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                tmpGendor = reader.GetByte(reader.GetOrdinal("Gendor"));
                tmpAddress = reader.GetString(reader.GetOrdinal("Address"));
                tmpPhone = reader.GetString(reader.GetOrdinal("Phone"));
                tmpEmail = reader["Email"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("Email")) : "";
                tmpNationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID"));
                tmpImagePath = reader["ImagePath"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("ImagePath")) : "";
            });

            if (isFound)
            {
                FirstName = tmpFirstName;
                SecondName = tmpSecondName;
                ThirdName = tmpThirdName;
                LastName = tmpLastName;
                PersonID = tmpPersonID;
                DateOfBirth = tmpDateOfBirth;
                Gendor = tmpGendor;
                Address = tmpAddress;
                Phone = tmpPhone;
                Email = tmpEmail;
                NationalityCountryID = tmpNationalityCountryID;
                ImagePath = tmpImagePath;
            }
            return isFound;
        }



        public static int AddNewPerson( string FirstName,  string SecondName,
           string ThirdName,  string LastName,  string NationalNo,  DateTime DateOfBirth,
           short Gendor, string Address,  string Phone,  string Email,
            int NationalityCountryID,  string ImagePath)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@NN", SqlDbType.NVarChar) {Value = NationalNo},
                new SqlParameter("@FN", SqlDbType.NVarChar) {Value = FirstName},
                new SqlParameter("@SN", SqlDbType.NVarChar) {Value = SecondName},
                new SqlParameter("@TN", SqlDbType.NVarChar) {Value = ThirdName},
                new SqlParameter("@LN", SqlDbType.NVarChar) {Value = LastName},
                new SqlParameter("DOB", SqlDbType.DateTime) {Value = DateOfBirth},
                new SqlParameter("Gendor", SqlDbType.TinyInt) {Value = Gendor},
                new SqlParameter("Add", SqlDbType.NVarChar) {Value = Address},
                new SqlParameter("Phone", SqlDbType.NVarChar) {Value = Phone},
                new SqlParameter("Email", SqlDbType.NVarChar) {Value = Email},
                new SqlParameter("NCId", SqlDbType.Int) {Value = NationalityCountryID},
                new SqlParameter("IPath", SqlDbType.NVarChar) {Value = ImagePath},
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewPerson", parameters);

            return result != null ? Convert.ToInt32(result) : -1;
        }



        public static bool UpdatePerson(int PersonID,  string FirstName, string SecondName,
           string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
           short Gendor, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@ID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("@NN", SqlDbType.NVarChar) {Value = NationalNo},
                new SqlParameter("@FN", SqlDbType.NVarChar) {Value = FirstName},
                new SqlParameter("@SN", SqlDbType.NVarChar) {Value = SecondName},
                new SqlParameter("@TN", SqlDbType.NVarChar) {Value = ThirdName},
                new SqlParameter("@LN", SqlDbType.NVarChar) {Value = LastName},
                new SqlParameter("@DOB", SqlDbType.DateTime) {Value = DateOfBirth},
                new SqlParameter("@Gendor", SqlDbType.TinyInt) {Value = Gendor},
                new SqlParameter("@Add", SqlDbType.NVarChar) {Value = Address},
                new SqlParameter("@Phone", SqlDbType.NVarChar) {Value = Phone},
                new SqlParameter("@Email", SqlDbType.NVarChar) {Value = Email},
                new SqlParameter("@NCId", SqlDbType.Int) {Value = NationalityCountryID},
                new SqlParameter("@IPath", SqlDbType.NVarChar) {Value = ImagePath}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdatePerson", parameter);

            return rowsAffected > 0;
        }

        
        public static async Task<DataTable> GetAllPeople(CancellationTokenSource cts)
        {
            return await clsDataHelper.GetDataTableAsync("SP_GetAllPeople", null);
        }

        public static bool DeletePerson(int PersonID)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_DeletePerson", parameter);

            return rowsAffected > 0;
        }

        public static bool IsPersonExist(int PersonID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID}
            };

            bool isFound = clsDataHelper.GetSingleRow("SP_IsPersonExistByPersonID", parameters, reader => reader.Close());

            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("NationalNo", SqlDbType.Int) {Value = NationalNo}
            };

            bool isFound = clsDataHelper.GetSingleRow("SP_IsPersonExistByNationalNo", parameters, reader => reader.Close());

            return isFound;
        }


    }
}

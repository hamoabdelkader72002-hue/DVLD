using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsUserData
    {
       
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName,
            ref string Password, ref bool IsActive)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("ID", SqlDbType.Int) {Value = UserID}
            };

            int tmpPersonID = 0;
            string tmpUserName = "";
            string tmpPassword = default;
            bool tmpIsActive = default;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetUser", parameters, reader =>
            {
                tmpPersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                tmpUserName = reader.GetString(reader.GetOrdinal("UserName"));
                tmpPassword = reader.GetString(reader.GetOrdinal("Password"));
                tmpIsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
            });

            if (isFound)
            {
                PersonID = tmpPersonID;
                UserName = tmpUserName;
                Password = tmpPassword;
                IsActive = tmpIsActive;
            }
            return isFound;
        }


        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
          ref string Password,ref bool IsActive)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID}
            };

            int tmpUserID = 0;
            string tmpUserName = "";
            string tmpPassword = default;
            bool tmpIsActive = default;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetUserByPersonID", parameters, reader =>
            {
                tmpUserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                tmpUserName = reader.GetString(reader.GetOrdinal("UserName"));
                tmpPassword = reader.GetString(reader.GetOrdinal("Password"));
                tmpIsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
            });

            if (isFound)
            {
               UserID = tmpUserID;
               UserName = tmpUserName;
               Password = tmpPassword;
               IsActive = tmpIsActive;
            }
            return isFound;
        }

        public static bool GetUserInfoByUsernameAndPassword(string UserName,  string Password, 
            ref int UserID, ref int PersonID, ref bool IsActive)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("UserName", SqlDbType.NVarChar) {Value = UserName},
                new SqlParameter("Password", SqlDbType.NVarChar) {Value = Password}
            };

            int tmpPersonID = 0;
            int tmpUserID = 0;
            bool tmpIsActive = default;

            bool isFound = clsDataHelper.GetSingleRow("SP_GetUserByUserNameAndPassword", parameters, reader =>
            {
                tmpUserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                tmpPersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                tmpIsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
            });

            if (isFound)
            {
                PersonID = tmpPersonID;
                UserID = tmpUserID;
                IsActive = tmpIsActive;
            }
            return isFound;
        }

        public static int AddNewUser(int PersonID,  string UserName,
             string Password,  bool IsActive)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("UserName", SqlDbType.NVarChar) {Value = UserName},
                new SqlParameter("Password", SqlDbType.NVarChar) {Value = Password},
                new SqlParameter("IsActive", SqlDbType.Bit) {Value = IsActive}
            };

            object result = clsDataHelper.ExecuteScalar("SP_AddNewUser", parameters);

            return result != null ? Convert.ToInt32(result) : -1;

        }


        public static bool UpdateUser(int UserID, int PersonID, string UserName,
             string Password, bool IsActive)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("UserID", SqlDbType.Int) {Value = UserID},
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
                new SqlParameter("UserName", SqlDbType.NVarChar) {Value = UserName},
                new SqlParameter("Password", SqlDbType.NVarChar) {Value = Password},
                new SqlParameter("IsActive", SqlDbType.Bit) {Value = IsActive}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_UpdateUser", parameter);

            return rowsAffected > 0;
        }


        public static async  Task<DataTable> GetAllUsers(CancellationTokenSource cts)
        {
            return await clsDataHelper.GetDataTableAsync("SP_GetAllUsers", null);
        }

        public static bool DeleteUser(int UserID)
        {

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("UserID", SqlDbType.Int) {Value = UserID}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_DeleteUser", parameter);

            return rowsAffected > 0;
        }

        public static bool IsUserExist(int UserID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("UserID", SqlDbType.Int) {Value = UserID},
            };

            bool isFound = clsDataHelper.GetSingleRow("SP_IsUserExist", parameters, reader =>
            {
                reader.Close();
            });

            return isFound;
        }

        public static bool IsUserExist(string UserName)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("UserName", SqlDbType.NVarChar) {Value = UserName},
            };

            bool isFound = clsDataHelper.GetSingleRow("SP_IsUserExistByUserName", parameters, reader =>
            {
                reader.Close();
            });

            return isFound;
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
           };

            bool isFound = clsDataHelper.GetSingleRow("SP_IsUserExistByPersonID", parameters, reader =>
            {
                reader.Close();
            });

            return isFound;
        }

        public static bool DoesPersonHaveUser44(int PersonID)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("PersonID", SqlDbType.Int) {Value = PersonID},
           };

            bool isFound = clsDataHelper.GetSingleRow("SP_IsUserExistByPersonID", parameters, reader =>
            {
                reader.Close();
            });

            return isFound;
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("UserID", SqlDbType.Int) {Value = UserID},
                new SqlParameter("Password", SqlDbType.NVarChar) {Value = NewPassword}
            };

            int rowsAffected = clsDataHelper.ExecuteNonQuery("SP_ChangePassword", parameter);

            return rowsAffected > 0;
        }

    }
}

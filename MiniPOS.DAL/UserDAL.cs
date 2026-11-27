using MiniPOS.DAL;
using MiniPOS.DTO;
using System.Data;
using System.Data.SqlClient;

namespace MiniPOS.DAL
{
    public class UserDAL
    {
        public UserDTO CheckLogin(string username, string passwordHash)
        {
            string query = @"SELECT UserID, Username, FullName, Role 
                             FROM Users 
                             WHERE Username = @Username AND PasswordHash = @PasswordHash";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@PasswordHash", passwordHash)
            };

            DataTable dt = DbHelper.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new UserDTO
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Username = row["Username"].ToString(),
                    FullName = row["FullName"].ToString(),
                    Role = row["Role"].ToString()
                };
            }
            return null;
        }
    }
}
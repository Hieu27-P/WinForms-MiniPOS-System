using MiniPOS.DAL;
using MiniPOS.DTO;

namespace MiniPOS.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDAL = new UserDAL();

        public UserDTO Login(string username, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(plainPassword))
                return null;

            string passHash = SecurityHelper.HashMD5(plainPassword);
            return _userDAL.CheckLogin(username.Trim(), passHash);
        }
    }
}

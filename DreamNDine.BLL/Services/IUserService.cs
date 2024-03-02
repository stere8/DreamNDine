using DreamNDine.BLL.Models;
using DreamNDine.BLL.Services;

namespace DreamNDine.BLL.Services
{
    public interface IUserService
    {
        User RegisterUser(User userDetails);
        User AuthenticateUser(string username, string password);
        User GetUserProfile(int userId);
        void UpdateUserProfile(int userId, User userDetails);
        bool IsAdmin(int userId);
    }
}
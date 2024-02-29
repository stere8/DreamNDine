using DreamNDine.BLL.Models;

public interface IUserService
{
    User RegisterUser(User userDetails);
    User AuthenticateUser(string username, string password);
    User GetUserProfile(int userId);
    void UpdateUserProfile(int userId, User userDetails);
    bool IsAdmin(int userId);
}
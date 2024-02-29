using DreamNDine.BLL.DbContext;
using DreamNDine.BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNDine.BLL.Services
{
    public class UserService : IUserService
    {

        private readonly DreamNDineContext _context;
        
        public UserService(DreamNDineContext context)
        {
            _context = context;
        }

        public User RegisterUser(User userDetails)
        {
            // 1. Input Validation (ensure username is unique, etc.)

            // 2. Password Hashing 
            userDetails.Password = HashPassword(userDetails.Password);

            // 3. Save to Database
            _context.Users.Add(userDetails);
            _context.SaveChanges();

            return userDetails;
        }

        public User AuthenticateUser(string username, string password)
        {
            // 1. Find user by username
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            // 2. If user found, verify password
            if (user != null && VerifyPasswordHash(password, user.Password))
            {
                return user;
            }

            return null; // User not found or incorrect password 
        }

        public void UpdateUserProfile(int userId, User userDetails)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user != null)
            {
                user.FirstName = userDetails.FirstName;
                user.LastName = userDetails.LastName;
                user.Email = userDetails.Email;
                user.Phone = userDetails.Phone;
                _context.SaveChanges();
            }
        }

        public bool IsAdmin(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            return user?.Role == Role.Admin;
        }


        public string HashPassword(string password)
        {
            // Install BCrypt.Net-Next NuGet Package
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public User GetUserProfile(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserID == userId);
        }
    }
}

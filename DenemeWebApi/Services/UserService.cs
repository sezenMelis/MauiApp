using DenemeFileOrbis.library.Responses;
using DenemeWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DenemeWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext appDbContext;
        public UserService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<ServiceResponse> ValidateUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new ServiceResponse()
                {
                    Message = "Username or password cannot be empty",
                    Success = false
                };
            }

            var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());

            if (user == null)
            {
                return new ServiceResponse()
                {
                    Message = "User not found",
                    Success = false
                };
            }

            if (user.Password != password)
            {
                return new ServiceResponse()
                {
                    Message = "Invalid password",
                    Success = false
                };
            }

            return new ServiceResponse()
            {
                Message = "Login successful",
                Success = true
            };
        }
    }
}

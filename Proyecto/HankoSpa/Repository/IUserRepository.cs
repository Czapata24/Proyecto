using Microsoft.AspNetCore.Identity;
using HankoSpa.Models;

namespace HankoSpa.Repository
{
    public interface IUserRepository
    {

       
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<User> GetUserAsync(string email);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<IQueryable<User>> GetUsersQueryableAsync();
        Task<int> UpdateUserAsync(User user);
        Task<bool> AssignCustomRoleAsync(User user, int customRolId);
        Task<bool> CustomRoleExistsAsync(string roleName);
        Task<IdentityResult> DeleteUserAsync(User user);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);


    }
}

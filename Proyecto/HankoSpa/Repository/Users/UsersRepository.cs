using HankoSpa.Data;
using HankoSpa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HankoSpa.Repository.Users;
using HankoSpa.DTOs;

namespace HankoSpa.Repository.Users
{
    public class UsersRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersRepository(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);

        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }
        public async Task<User> GetUserAsync(string email)
        {
            var user = await _context.Users
             .FirstOrDefaultAsync(x => x.Email == email);
            return user!;
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);

        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
           
        public async Task<int> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
           return  await _context.SaveChangesAsync();
        }

        public Task<IQueryable<User>> GetUsersQueryableAsync()
        {
            return Task.FromResult(_userManager.Users);
        }

        public async Task<bool> AssignCustomRoleAsync(User user, int customRolId)
        {
            var rol = await _context.CustomRoles.FindAsync(customRolId);
            if (rol == null || user == null)
                return false;

            user.CustomRolId = customRolId;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CustomRoleExistsAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return false;

            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Usuario no encontrado."
                });
            }

            var result = await _userManager.DeleteAsync(user);
            return result;
           
        }
    }
}
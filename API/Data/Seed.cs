using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedAccountTypes(DataContext context)
        {

            if (await context.AccountTypes.AnyAsync()) return;

            var accountTypeData = await System.IO.File.ReadAllTextAsync("Data/AccountTypeData.json");

            var accountTypes = JsonSerializer.Deserialize<List<AccountType>>(accountTypeData);

            foreach(var accountType in accountTypes)
            {
                context.AccountTypes.Add(accountType);
            }
        }
        public static async Task SeedUsers(DataContext context)
        {

            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
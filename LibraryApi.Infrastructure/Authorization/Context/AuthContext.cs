using LibraryApi.Infrastructure.Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Authorization.Context
{
    public class AuthContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }
    }
}

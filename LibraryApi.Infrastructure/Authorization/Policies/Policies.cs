using Microsoft.AspNetCore.Authorization;

namespace LibraryApi.Infrastructure.Authorization.Policies
{
    public static class Policies
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string AdminOrUser = "AdminOrUser";
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }
        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
        public static AuthorizationPolicy AdminOrUserPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(Admin, User)  
                .Build();
        }
    }
}


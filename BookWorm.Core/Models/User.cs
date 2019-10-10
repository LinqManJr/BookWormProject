using Microsoft.AspNetCore.Identity;

namespace BookWorm.Core.Models
{
    public class User : IdentityUser<int>
    {
        /*
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        */
    }

    public class Role : IdentityRole<int> { }
    public class UserClaims : IdentityUserClaim<int> { }
    public class UserRoles : IdentityUserRole<int> { }
    public class RoleClaims : IdentityUserClaim<int> { }
    public class UserLogins : IdentityUserLogin<int> { }
    public class UserTokens : IdentityUserToken<int> { }
}
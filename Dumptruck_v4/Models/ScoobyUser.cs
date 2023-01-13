using Microsoft.AspNetCore.Identity;

namespace Dumptruck_v4.Models {
    public class ScoobyUser : IdentityUser <int> { //Though actually I will want to use Microsoft IDentity I think
        public string? SillyMotto { get; set; } = "No motto";
    }

    public class CustomUserLogin: IdentityUserLogin<int> { }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }

    }

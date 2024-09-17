using System.ComponentModel;

namespace API.Enums;


public enum UserRole
{
    [Description("Admin")]
    Admin = 1,
    [Description("User")]
    User = 2,
}
using System;
using Microsoft.AspNetCore.Identity;

namespace API.Data.ModelsData;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

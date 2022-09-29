using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Memez.Models;

namespace Memez.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MemezUser class
public class MemezUser : IdentityUser
{
    public ICollection<Meme> Memes { get; set; }
}


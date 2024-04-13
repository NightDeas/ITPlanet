using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPlanet.Data.Models
{
    public class User : IdentityUser<int>
    {
        public override int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}

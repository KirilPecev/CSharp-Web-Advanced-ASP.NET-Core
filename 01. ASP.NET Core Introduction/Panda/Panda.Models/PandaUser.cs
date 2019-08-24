namespace Panda.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            this.Packages = new HashSet<Package>();
            this.Receipts = new HashSet<Receipt>();
        }

        public ICollection<Package> Packages { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}

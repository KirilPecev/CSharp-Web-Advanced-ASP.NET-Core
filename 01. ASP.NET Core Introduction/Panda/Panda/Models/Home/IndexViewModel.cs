namespace Panda.Models.Home
{
    using Panda.Models.Packages;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Pending = new List<PackageViewModel>();
            this.Shipped = new List<PackageViewModel>();
            this.Delivered = new List<PackageViewModel>();
        }
        public List<PackageViewModel> Pending { get; set; }

        public List<PackageViewModel> Shipped { get; set; }

        public List<PackageViewModel> Delivered { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendor.Logic.Interfaces
{
    public interface ILoaderFactory // required for unit testing to mock the loader factory
    {
        internal ILoader GetLoader();
    }
}

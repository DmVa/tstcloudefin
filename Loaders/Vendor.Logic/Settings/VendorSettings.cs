using System;
namespace Vendor.Logic.Settings
{
    public class VendorSettings
    {
        public LoaderTypeEnum LoaderType { get; set; }
        public FileLoaderSettings FileLoaderSettings { get; set; }
        public SqlServerLoaderSettings SqlLoaderSettings { get; set; }
    }
}

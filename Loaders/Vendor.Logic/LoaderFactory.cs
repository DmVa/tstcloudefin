using Microsoft.Extensions.Options;
using System;
using Vendor.Logic.Interfaces;
using Vendor.Logic.Settings;

namespace Vendor.Logic
{
    public class LoaderFactory : ILoaderFactory
    {
        private VendorSettings _settings { get; set; }

        public LoaderFactory(IOptions<VendorSettings> settings)
        {
            _settings = settings.Value;
        }

        ILoader ILoaderFactory.GetLoader()
        {
            if (_settings.LoaderType == LoaderTypeEnum.FileLoader)
            {
                return new FileLoaderAdapter(_settings.FileLoaderSettings.FilePath);
            }
            else if (_settings.LoaderType == LoaderTypeEnum.SqlServerLoader)
            {
                return new SqlServerLoaderAdapter(_settings.SqlLoaderSettings.Server, _settings.SqlLoaderSettings.User, _settings.SqlLoaderSettings.Password);
            }
            else
            {
                throw new NotImplementedException($"Loader type '{_settings.LoaderType}' is not implemented.");
            }
        }
    }
}

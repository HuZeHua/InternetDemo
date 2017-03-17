using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Config
{
    public class ApplicationConfig : ConfigurationSection
    {
        private const string RedisCacheConfigPropertyName = "redisCache";

        [ConfigurationProperty(RedisCacheConfigPropertyName)]
        public RedisCacheElement RedisCacheConfig
        {
            get { return (RedisCacheElement)base[RedisCacheConfigPropertyName]; }
            set { base[RedisCacheConfigPropertyName] = value; }
        }
    }
}

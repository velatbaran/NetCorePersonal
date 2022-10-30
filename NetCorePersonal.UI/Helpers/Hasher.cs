using NETCore.Encrypt.Extensions;

namespace NetCorePersonal.UI.Helpers
{
    public interface IHasher
    {
        string DoMD5Hashed(string password);
    }

    public class Hasher : IHasher
    {
        private readonly IConfiguration _configuration;

        public Hasher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DoMD5Hashed(string password)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = password + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }
    }
}

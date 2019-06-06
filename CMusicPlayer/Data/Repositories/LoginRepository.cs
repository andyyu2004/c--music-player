using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Data.Network;
using CMusicPlayer.Data.Network.ResponseTypes;
using CMusicPlayer.Internal.Types.Functional;
using Newtonsoft.Json;
using System.Security.Cryptography;
using ByteTerrace.Maths.Cryptography;
using CMusicPlayer.Configuration;
using CMusicPlayer.Data.Network.Types;
using CMusicPlayer.Util;
using static CMusicPlayer.Util.Constants;

namespace CMusicPlayer.Data.Repositories
{
    internal class LoginRepository
    {
        private readonly NetworkDataSource networkDataSource;
        private readonly Database database;

        public LoginRepository(NetworkDataSource networkDataSource, Database database)
        {
            this.networkDataSource = networkDataSource;
            this.database = database;
        }

        /// <summary>
        /// Generates User object with the hashed password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="saltHex"></param>
        /// <returns name="User"></returns>
        private static Try<User> GenerateUserObject(string email, string password, string saltHex)
        {
//            var prf = KeyDerivationPrf.HMACSHA256;
//            var hash = KeyDerivation.Pbkdf2(password, salt, prf, 10000, 64);
//            var hex = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            var salt = Encoding.UTF8.GetBytes(saltHex);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            using var sha256 = new HMACSHA256(passwordBytes);
            using var pbkdf2 = Pbkdf2.New(sha256, salt, 10000);
            var hex = BitConverter.ToString(pbkdf2.GetBytes(64)).Replace("-", string.Empty).ToLower();
            return new User
            {
                Email = email,
                Password = hex
            };
        }

        /// <summary>
        /// Executes Login Procedure
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Try<string>> ExecuteLogin(string email, string password)
        {
            var res = await networkDataSource.GetSalt(email);
            var loginResponseStr = await res
                .Bind<string>(saltRes => JsonConvert.DeserializeObject<SaltResponse>(saltRes).Salt)
                .Bind(salt => GenerateUserObject(email, password, salt))
                .BindAsync(user => networkDataSource.PostLoginWithCredentials(user));
            return loginResponseStr.Bind(ParseLoginResponse);
        }

        /// <summary>
        /// Parses Login Response And Saves Data Locally    
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        private static Try<string> ParseLoginResponse(string res)
        {
            Debug.WriteLine(res);
            var loginResObj = JsonConvert.DeserializeObject<LoginResponse>(res);
            var auth = Config.Settings[Authentication];
            auth[JwtToken] = loginResObj.Token;
            auth[UserName] = loginResObj.User;
            auth[UserId] = loginResObj.UserId;
            Config.Save();
            return loginResObj.Message;
        }
    }
}

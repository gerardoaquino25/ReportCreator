using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Data;
using System.Configuration;
using System.IO;

namespace ReportCreator.Entities.Authentication
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private class InternalUserData
        {
            public InternalUserData(int id, string username, string email, string hashedPassword, string[] roles)
            {
                Id = id;
                Username = username;
                Email = email;
                HashedPassword = hashedPassword;
                Roles = roles;
            }
            public InternalUserData(string username, string email, string hashedPassword, string[] roles)
            {
                Username = username;
                Email = email;
                HashedPassword = hashedPassword;
                Roles = roles;
            }
            public int Id
            {
                get;
                private set;
            }
            public string Username
            {
                get;
                private set;
            }

            public string Email
            {
                get;
                private set;
            }

            public string HashedPassword
            {
                get;
                private set;
            }

            public string[] Roles
            {
                get;
                private set;
            }
        }

        private IList<InternalUserData> GetUsers()
        {
            IList<InternalUserData> lista = new List<InternalUserData>();
            SqlCeConnection connection = new SqlCeConnection(ConfigurationManager.ConnectionStrings["ReportCreatorConnectionString"].ConnectionString);
            connection.Open();

            try
            {
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM usuario", connection);

                using (SqlCeDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new InternalUserData(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), new string[0]));
                    }
                }
            }
            catch (Exception e)
            {
            }

            connection.Close();

            return lista;
        }

        public User AuthenticateUser(string username, string clearTextPassword)
        {
            InternalUserData userData = GetUsers().FirstOrDefault(u => u.Username.Equals(username)
                && u.HashedPassword.Equals(Encrypt(clearTextPassword)));

            if (userData == null)
                throw new UnauthorizedAccessException("Acceso denegado, por favor ingrese credenciales válidas.");

            return new User(userData.Id, userData.Username, userData.Email, userData.Roles);
        }

        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");
        private const int keysize = 256;

        private string Encrypt(string pass)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(pass);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes("RO-SOFTWARE-GDA-2", null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes("RO-SOFTWARE-GDA-2", null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }

    public class User
    {
        public User(int id, string username, string email, string[] roles)
        {
            Id = id;
            Username = username;
            Email = email;
            Roles = roles;
        }

        public User(string username, string email, string[] roles)
        {
            Username = username;
            Email = email;
            Roles = roles;
        }

        public int Id
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string[] Roles
        {
            get;
            set;
        }
    }
}
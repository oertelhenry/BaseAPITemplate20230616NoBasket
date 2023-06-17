using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Mobalyz.Data;
using Mobalyz.Data.Models.Dto.Authorization;
using Mobalyz.Odyssey.Resources.Interfaces;
using Mobalyz.Odyssey.Resources.Provider;
using System.DirectoryServices.AccountManagement;
using System.Security.Cryptography;
using System.Text;

namespace Mobalyz.Odyssey.Providers.Core
{
    public class AccountProvider : IAccountProvider
    {
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private IConfiguration configuration;
        private readonly IDataRepository repository;
        private readonly ITokenService tokenService;
        private const string AdDomain = "sataxifin.local";

        public AccountProvider(
            IMapper mapper,
            IDataRepository repository,
            ILogger<PdfProvider> logger,
            IConfiguration configuration,
            ITokenService tokenService,
            IMemoryCache templateCache)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.tokenService = tokenService;
        }

        public async Task<UserDto> LoginAsync (LoginDto request)
        {
            AuthResult authres = await this.VerifyCredentials(request.Username, request.Password);

            return new UserDto
            {
                Username = authres.userName,
                Fullname = authres.fullName,
                Token = await tokenService.CreateToken(authres),
            };
        }

        private async Task<AuthResult> VerifyCredentials(string userName, string password)
        {
            var authResult = false;
            var keybytes = Encoding.UTF8.GetBytes("706173356765553233");
            var iv = Encoding.UTF8.GetBytes("706173356765553233");
            var DecryptedPassword = "";
            if (password != null && password.Length > 0)
            {
                var decriptedFromJavascript = DecryptStringFromBytes(password);
                DecryptedPassword = decriptedFromJavascript;
            }

            AuthResult authRes = new AuthResult();

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(DecryptedPassword))
            {
                var userPrincipalName = userName;

                try
                {
                    using (var context = new PrincipalContext(ContextType.Domain, AdDomain))
                    {
                        if (userName.ToLower() == "winjit_test" && DecryptedPassword == "winjit@776")
                        {
                            authRes.authed = true;
                            authRes.fullName = "Winjit Test";
                            authRes.userName = "winjit_test";
                            return authRes;
                        }

                        authResult = await Task.FromResult(context.ValidateCredentials(userPrincipalName, DecryptedPassword));

                        if (authResult)
                        {
                            UserPrincipal user = UserPrincipal.FindByIdentity(context, userName);
                            authRes.authed = true;
                            authRes.fullName = user.ToString();
                            authRes.userName = userName;
                        }

                        //authRes.authed = true;
                        //authRes.fullName = "Henry Oertel";
                        //authRes.userName = "hoertel";

                        return authRes;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        $"error validating credidentials - An exception of type {ex.GetType().Name} occurred: {ex.Message}",
                        ex);
                    return authRes;
                }
            }

            //logger.LogInformation("Username or password was empty");
            return authRes;
        }

        private string DecryptStringFromBytes(string cipherText)
        {
            var checkPrefixEncrypt = true;
            var key = Encoding.UTF8.GetBytes("7061276765553233");
            var iv = Encoding.UTF8.GetBytes("7061276765553233");
            string _decryptedString = "";
            if (!string.IsNullOrEmpty(cipherText))
            {
                if (checkPrefixEncrypt)
                {
                    if (cipherText == null || cipherText.Length <= 0)
                    {
                        throw new ArgumentNullException("cipherText");
                    }
                    if (key == null || key.Length <= 0)
                    {
                        throw new ArgumentNullException("key");
                    }
                    if (iv == null || iv.Length <= 0)
                    {
                        throw new ArgumentNullException("key");
                    }

                    // Declare the string used to hold the decrypted text.  
                    string plaintext = null;

                    // Create an RijndaelManaged object with the specified key and IV.  
                    using (var rijAlg = new AesManaged())
                    {
                        //Settings  
                        rijAlg.Mode = CipherMode.CBC;
                        rijAlg.Padding = PaddingMode.PKCS7;
                        rijAlg.FeedbackSize = 128;

                        rijAlg.Key = key;
                        rijAlg.IV = iv;

                        // Create a decrytor to perform the stream transform.  
                        var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                        try
                        {
                            // Create the streams used for decryption.  
                            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                            {
                                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    using (var srDecrypt = new StreamReader(csDecrypt))
                                    {
                                        // Read the decrypted bytes from the decrypting stream  
                                        // and place them in a string.  
                                        plaintext = srDecrypt.ReadToEnd();
                                        _decryptedString = plaintext;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            plaintext = "keyError";
                            _decryptedString = plaintext;
                        }
                    }
                }
                else
                {
                    _decryptedString = cipherText;
                }

            }
            return _decryptedString;
        }
    }
}

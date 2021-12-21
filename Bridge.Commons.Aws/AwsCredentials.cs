using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Bridge.Commons.Aws.Settings;

namespace Bridge.Commons.Aws
{
    /// <summary>
    ///     Credenciais
    /// </summary>
    public class AwsCredentials
    {
        #region Privates

        /// <summary>
        ///     Cria a credencial do usuário através do perfil ou do AccessKey e Secret Key
        /// </summary>
        private void CreateCredentials()
        {
            var chain = new CredentialProfileStoreChain();

            if (Profile.ProfileName != null && chain.TryGetAWSCredentials(Profile.ProfileName, out _credentials))
            {
                var credential = _credentials.GetCredentials();
                Profile.AccessKey = credential.AccessKey;
                Profile.SecretKey = credential.SecretKey;
            }
            else
            {
                _credentials = new BasicAWSCredentials(Profile.AccessKey, Profile.SecretKey);
            }
        }

        #endregion

        #region Properties

        private AWSCredentials _credentials;

        /// <summary>
        ///     Perfil da AWS
        /// </summary>
        public AwsProfile Profile { get; set; }

        /// <summary>
        ///     Se tem chave de acesso
        /// </summary>
        public bool HasAccessKey => !string.IsNullOrWhiteSpace(Profile.AccessKey) &&
                                    !string.IsNullOrWhiteSpace(Profile.SecretKey);

        /// <summary>
        ///     Credenciais
        /// </summary>
        public AWSCredentials Credentials => _credentials;

        /// <summary>
        ///     Credenciais imutáveis
        /// </summary>
        public ImmutableCredentials ImmutableCredentials => _credentials.GetCredentials();

        #endregion

        #region Constructors

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="profilename"></param>
        /// <param name="region"></param>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        public AwsCredentials(string profilename, string region, string accessKey, string secretKey)
        {
            Profile = new AwsProfile
            {
                ProfileName = profilename,
                Region = region,
                AccessKey = accessKey,
                SecretKey = secretKey
            };

            CreateCredentials();
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="settings"></param>
        public AwsCredentials(AwsProfile settings) :
            this(settings.ProfileName, settings.Region, settings.AccessKey, settings.SecretKey)
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        /// <param name="region"></param>
        public AwsCredentials(string accessKey, string secretKey, string region)
            : this(null, region, accessKey, secretKey)
        {
        }

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="profilename"></param>
        /// <param name="region"></param>
        public AwsCredentials(string profilename, string region)
            : this(profilename, region, null, null)
        {
        }

        #endregion
    }
}
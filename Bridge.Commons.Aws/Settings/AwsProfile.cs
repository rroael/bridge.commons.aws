using Amazon;

namespace Bridge.Commons.Aws.Settings
{
    /// <summary>
    ///     Perfil AWS
    /// </summary>
    public class AwsProfile
    {
        #region Properties

        /// <summary>
        ///     Nome do perfil
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        ///     Região
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        ///     Chave de Acesso
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        ///     Chave secreta
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        ///     Região buscada pelo sistema
        /// </summary>
        public RegionEndpoint RegionEndpoint => RegionEndpoint.GetBySystemName(Region);

        #endregion
    }
}
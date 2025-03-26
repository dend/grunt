using Den.Dev.Grunt.Models.Security;

namespace Den.Dev.Grunt.Authentication
{
    /// <summary>
    /// Interface representing the Proof-of-Possession signature provider.
    /// </summary>
    internal interface IPoPCryptoProvider
    {
        /// <summary>
        /// Gets the currently produced proof key for the provider.
        /// </summary>
        ProofKey ProofKey { get; }

        /// <summary>
        /// Signs the request data based on the existing key.
        /// </summary>
        /// <param name="data">Binary data to be signed.</param>
        /// <returns>If successful, returns data signed with the self-generated key.</returns>
        byte[] Sign(byte[] data);
    }
}

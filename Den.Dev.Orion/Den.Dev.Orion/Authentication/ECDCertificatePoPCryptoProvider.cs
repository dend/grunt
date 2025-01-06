// <copyright file="ECDCertificatePoPCryptoProvider.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Security.Cryptography;
using Den.Dev.Orion.Models.Security;
using Den.Dev.Orion.Util;

namespace Den.Dev.Orion.Authentication
{
    /// <summary>
    /// Implementation of the Proof-of-Possession provider that allows signing of binary data with a locally-generated key.
    /// </summary>
    public class ECDCertificatePoPCryptoProvider : IPoPCryptoProvider
    {
        private readonly ECDsa signer;
        private ProofKey? proofKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECDCertificatePoPCryptoProvider"/> class.
        /// </summary>
        public ECDCertificatePoPCryptoProvider()
        {
            var ecCurve = ECCurve.NamedCurves.nistP256;
            this.signer = ECDsa.Create(ecCurve);
        }

        /// <summary>
        /// Gets or sets the proof key associated with the provider. An existing key will be provided if previous runs were done.
        /// </summary>
        public ProofKey ProofKey
        {
            get => this.proofKey ??= this.GenerateNewProofKey();
            set => this.proofKey = value;
        }

        /// <summary>
        /// Signs binary data with the locally-generated key.
        /// </summary>
        /// <param name="data">Binary data to be signed.</param>
        /// <returns>If successful, returns the binary data signed with the local key.</returns>
        public byte[] Sign(byte[] data)
        {
            return this.signer.SignData(data, HashAlgorithmName.SHA256);
        }

        private ProofKey GenerateNewProofKey()
        {
            var parameters = this.signer.ExportParameters(false);
            return new ProofKey()
            {
                KeyType = "EC",
                X = parameters.Q.X != null ? Base64Encoder.Encode(parameters.Q.X) : null,
                Y = parameters.Q.Y != null ? Base64Encoder.Encode(parameters.Q.Y) : null,
                Curve = "P-256",
                Algorithm = "ES256",
                Use = "sig",
            };
        }
    }
}

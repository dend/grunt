// <copyright file="Key.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Signing key.
    /// </summary>
    /// <remarks>
    /// Additional details can be obtained by reading the <see href="https://datatracker.ietf.org/doc/html/rfc7518">RFC 7518 document</see> or a more recent <see href="https://stackoverflow.com/questions/70022898/what-does-e-aqab-mean-in-jwks">Stack Overflow question</see>.
    /// </remarks>
    [IsAutomaticallySerializable]
    public class Key
    {
        /// <summary>
        /// Gets or sets the key type.
        /// </summary>
        [JsonPropertyName("kty")]
        public string? KeyType { get; set; }

        /// <summary>
        /// Gets or sets they key use.
        /// </summary>
        public string? Use { get; set; }

        /// <summary>
        /// Gets or sets the key ID.
        /// </summary>
        [JsonPropertyName("kid")]
        public string? KeyId { get; set; }

        /// <summary>
        /// Gets or sets the <see href="https://datatracker.ietf.org/doc/html/rfc7518#section-6.3.1.1">key modulus</see>.
        /// </summary>
        [JsonPropertyName("n")]
        public string? Modulus { get; set; }

        /// <summary>
        /// Gets or sets the <see href="https://datatracker.ietf.org/doc/html/rfc7518#section-6.3.1.2">key exponent</see>.
        /// </summary>
        [JsonPropertyName("e")]
        public string? Exponent { get; set; }

        /// <summary>
        /// Gets or sets the <see href="https://en.wikipedia.org/wiki/X.509">X.509</see> certificate chain.
        /// </summary>
        [JsonPropertyName("x5c")]
        public List<string>? X509CertificateChain { get; set; }
    }
}

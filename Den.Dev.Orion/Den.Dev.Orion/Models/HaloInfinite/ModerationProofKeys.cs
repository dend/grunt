// <copyright file="ModerationProofKeys.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Proof keys used to sign moderation requests.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ModerationProofKeys
    {
        /// <summary>
        /// Gets or sets the list of moderation proof keys.
        /// </summary>
        public List<Key>? Keys { get; set; }
    }
}

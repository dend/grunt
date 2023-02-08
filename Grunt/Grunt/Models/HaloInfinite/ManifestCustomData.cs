// <copyright file="ManifestCustomData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Custom data included in a Halo Infinite build manifest.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ManifestCustomData
    {
        /// <summary>
        /// Gets or sets the game branch name.
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// Gets or sets the game build number.
        /// </summary>
        public string? BuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the build kind.
        /// </summary>
        public int Kind { get; set; }

        /// <summary>
        /// Gets or sets the content version.
        /// </summary>
        public string? ContentVersion { get; set; }

        /// <summary>
        /// Gets or sets the build GUID.
        /// </summary>
        public string? BuildGuid { get; set; }

        /// <summary>
        /// Gets or sets the build visibility.
        /// </summary>
        public int Visibility { get; set; }
    }
}

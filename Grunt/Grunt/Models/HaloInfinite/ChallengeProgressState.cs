// <copyright file="ChallengeProgressState.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// State tracker for challenge progress.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ChallengeProgressState
    {
        /// <summary>
        /// Gets or sets the path to the challenge.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the previous progress for the challenge.
        /// </summary>
        public int PreviousProgress { get; set; }

        /// <summary>
        /// Gets or sets the current progress for the challenge.
        /// </summary>
        public int Progress { get; set; }
    }
}

// <copyright file="ChallengeProgressState.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
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

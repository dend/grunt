// <copyright file="MatchSkillInfo.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Skill information related to a match.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MatchSkillInfo
    {
        /// <summary>
        /// Gets or sets a list of skill-related results for a match.
        /// </summary>
        public List<SkillResult>? Value { get; set; }
    }
}

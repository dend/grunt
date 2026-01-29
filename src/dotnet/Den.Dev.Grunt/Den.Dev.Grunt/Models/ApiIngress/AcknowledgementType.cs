// <copyright file="AcknowledgementType.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.ApiIngress
{
    /// <summary>
    /// Type of acknowledgement for an API request.
    /// </summary>
    [IsAutomaticallySerializable]
    public enum AcknowledgementType
    {
        /// <summary>
        /// No acknowledgement.
        /// </summary>
        NoAcknowledgement = 0,

        /// <summary>
        /// Reply acknowledging the request.
        /// </summary>
        Reply = 1,
    }
}

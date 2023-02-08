// <copyright file="AuthoringFavoritesContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for authoring favorites.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringFavoritesContainer : AuthoringResultContainerBase
    {
        /// <summary>
        /// Gets or sets the list of favorited assets.
        /// </summary>
        public List<FavoriteAsset>? Results { get; set; }
    }
}

// <copyright file="IsAutomaticallySerializableAttribute.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;

namespace Den.Dev.Orion.Models
{
    /// <summary>
    /// Determines whether an object can be automatically serialized by a Halo API client.
    /// </summary>
    public class IsAutomaticallySerializableAttribute : Attribute
    {
    }
}

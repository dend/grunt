// <copyright file="Transaction.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container class for a transaction.
    /// </summary>
    /// <remarks>
    /// Additional research is needed to understand the underlying data model.
    /// </remarks>
    [IsAutomaticallySerializable]
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the transaction adjustment source.
        /// </summary>
        public string? AdjustmentSource { get; set; }

        /// <summary>
        /// Gets or sets the balance adjustment based on the transaction.
        /// </summary>
        public int? BalanceAdjustment { get; set; }

        /// <summary>
        /// Gets or sets the final resulting balance.
        /// </summary>
        public int? ResultingBalance { get; set; }

        /// <summary>
        /// Gets or sets whether the transaction was finalized.
        /// </summary>
        public bool? Finalized { get; set; }

        /// <summary>
        /// Gets or sets the transaction ID.
        /// </summary>
        public Guid? TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the transaction date.
        /// </summary>
        public APIFormattedDate? TransactionDate { get; set; }

        /// <summary>
        /// Gets or sets the product reference.
        /// </summary>
        public string? ProductReference { get; set; }

        /// <summary>
        /// Gets or sets the units consumed.
        /// </summary>
        public int? UnitsConsumed { get; set; }

        /// <summary>
        /// Gets or sets the authenticated identities related to the transaction. Can contain both XUIDs as well as device identifiers.
        /// </summary>
        public List<string>? AuthenticatedIdentities { get; set; }
    }
}

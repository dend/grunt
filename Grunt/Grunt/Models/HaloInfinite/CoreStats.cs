// <copyright file="CoreStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Gets or sets core player stats.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CoreStats
    {
        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the personal score.
        /// </summary>
        public int PersonalScore { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds won.
        /// </summary>
        public int RoundsWon { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds lost.
        /// </summary>
        public int RoundsLost { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds tied.
        /// </summary>
        public int RoundsTied { get; set; }

        /// <summary>
        /// Gets or sets the number of kills.
        /// </summary>
        public int Kills { get; set; }

        /// <summary>
        /// Gets or sets the number of deaths.
        /// </summary>
        public int Deaths { get; set; }

        /// <summary>
        /// Gets or sets the number of assists.
        /// </summary>
        public int Assists { get; set; }

        /// <summary>
        /// Gets or sets the Kill/Death/Assist (KDA) ratio.
        /// </summary>
        public float KDA { get; set; }

        /// <summary>
        /// Gets or sets the number of suicides.
        /// </summary>
        public int Suicides { get; set; }

        /// <summary>
        /// Gets or sets the number of betrayals.
        /// </summary>
        public int Betrayals { get; set; }

        /// <summary>
        /// Gets or sets the average life duration.
        /// </summary>
        public string? AverageLifeDuration { get; set; }

        /// <summary>
        /// Gets or sets the number of grenade kills.
        /// </summary>
        public int GrenadeKills { get; set; }

        /// <summary>
        /// Gets or sets the number of headshot kills.
        /// </summary>
        public int HeadshotKills { get; set; }

        /// <summary>
        /// Gets or sets the number of melee kills.
        /// </summary>
        public int MeleeKills { get; set; }

        /// <summary>
        /// Gets or sets the number of power weapon kills.
        /// </summary>
        public int PowerWeaponKills { get; set; }

        /// <summary>
        /// Gets or sets the number of shots fired.
        /// </summary>
        public int ShotsFired { get; set; }

        /// <summary>
        /// Gets or sets the number of shots hit.
        /// </summary>
        public int ShotsHit { get; set; }

        /// <summary>
        /// Gets or sets the player shooting accuracy.
        /// </summary>
        public float Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the amound of damage dealt.
        /// </summary>
        public int DamageDealt { get; set; }

        /// <summary>
        /// Gets or sets the amount of damage taken.
        /// </summary>
        public int DamageTaken { get; set; }

        /// <summary>
        /// Gets or sets the number of call-out assists.
        /// </summary>
        public int CalloutAssists { get; set; }

        /// <summary>
        /// Gets or sets the number of vehicle destroys.
        /// </summary>
        public int VehicleDestroys { get; set; }

        /// <summary>
        /// Gets or sets the number of driver assists.
        /// </summary>
        public int DriverAssists { get; set; }

        /// <summary>
        /// Gets or sets the number of hijacks.
        /// </summary>
        public int Hijacks { get; set; }

        /// <summary>
        /// Gets or sets the number of EMP assists.
        /// </summary>
        public int EmpAssists { get; set; }

        /// <summary>
        /// Gets or sets the maximum killing spree.
        /// </summary>
        public int MaxKillingSpree { get; set; }

        /// <summary>
        /// Gets or sets the list of awarded medals.
        /// </summary>
        public List<Medal>? Medals { get; set; }

        /// <summary>
        /// Gets or sets the list of awarded personal scores.s
        /// </summary>
        public List<PersonalScore>? PersonalScores { get; set; }

        /// <summary>
        /// Gets or sets damage dealt.
        /// </summary>
        /// <remarks>
        /// This value is deprecated and no longer included in the API.
        /// </remarks>
        public float DeprecatedDamageDealt { get; set; }

        /// <summary>
        /// Gets or sets damage taken.
        /// </summary>
        /// <remarks>
        /// This value is deprecated and no longer included in the API.
        /// </remarks>
        public float DeprecatedDamageTaken { get; set; }
    }
}

// <copyright file="GameVariantCategory.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Category a game variant falls into.
    /// </summary>
    public enum GameVariantCategory
    {
        /// <summary>
        /// Unknown game variant category
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// No assigned game variant category
        /// </summary>
        None = 0,

        /// <summary>
        /// Campaign
        /// </summary>
        Campaign = 1,

        /// <summary>
        /// Forge
        /// </summary>
        Forge = 2,

        /// <summary>
        /// Academy
        /// </summary>
        Academy = 3,

        /// <summary>
        /// Academy tutorial
        /// </summary>
        AcademyTutorial = 4,

        /// <summary>
        /// Academy practice
        /// </summary>
        AcademyPractice = 5,

        /// <summary>
        /// Slayer
        /// </summary>
        MultiplayerSlayer = 6,

        /// <summary>
        /// Attrition
        /// </summary>
        MultiplayerAttrition = 7,

        /// <summary>
        /// Elimination
        /// </summary>
        MultiplayerElimination = 8,

        /// <summary>
        /// Fiesta
        /// </summary>
        MultiplayerFiesta = 9,

        /// <summary>
        /// SWAT
        /// </summary>
        MultiplayerSwat = 10,

        /// <summary>
        /// Strongholds
        /// </summary>
        MultiplayerStrongholds = 11,

        /// <summary>
        /// King of the Hill
        /// </summary>
        MultiplayerBastion = 12,

        /// <summary>
        /// King of the Hill
        /// </summary>
        MultiplayerKingOfTheHill = 13,

        /// <summary>
        /// Total Control
        /// </summary>
        MultiplayerTotalControl = 14,

        /// <summary>
        /// Capture the Flag
        /// </summary>
        MultiplayerCtf = 15,

        /// <summary>
        /// Assault
        /// </summary>
        MultiplayerAssault = 16,

        /// <summary>
        /// Extraction
        /// </summary>
        MultiplayerExtraction = 17,

        /// <summary>
        /// Oddball
        /// </summary>
        MultiplayerOddball = 18,

        /// <summary>
        /// Stockpile
        /// </summary>
        MultiplayerStockpile = 19,

        /// <summary>
        /// Juggernaut
        /// </summary>
        MultiplayerJuggernaut = 20,

        /// <summary>
        /// Regicide
        /// </summary>
        MultiplayerRegicide = 21,

        /// <summary>
        /// Infection
        /// </summary>
        MultiplayerInfection = 22,

        /// <summary>
        /// VIP
        /// </summary>
        MultiplayerEscort = 23,

        /// <summary>
        /// Escalation
        /// </summary>
        MultiplayerGunGame = 24,

        /// <summary>
        /// Grifball
        /// </summary>
        MultiplayerGrifball = 25,

        /// <summary>
        /// Race
        /// </summary>
        MultiplayerRace = 26,

        /// <summary>
        /// Multiplayer prototype
        /// </summary>
        MultiplayerPrototype = 27,

        /// <summary>
        /// Test
        /// </summary>
        Test = 28,

        /// <summary>
        /// Academy test
        /// </summary>
        TestAcademy = 29,

        /// <summary>
        /// Audio test
        /// </summary>
        TestAudio = 30,

        /// <summary>
        /// Campaign test
        /// </summary>
        TestCampaign = 31,

        /// <summary>
        /// Test game variant
        /// </summary>
        TestEngine = 32,

        /// <summary>
        /// Forge test
        /// </summary>
        TestForge = 33,

        /// <summary>
        /// Graphics test
        /// </summary>
        TestGraphics = 34,

        /// <summary>
        /// Multiplayer test
        /// </summary>
        TestMultiplayer = 35,

        /// <summary>
        /// Sandbox test
        /// </summary>
        TestSandbox = 36,

        /// <summary>
        /// Academy training
        /// </summary>
        AcademyTraining = 37,

        /// <summary>
        /// Academy weapon drill
        /// </summary>
        AcademyWeaponDrill = 38,

        /// <summary>
        /// Land Grab
        /// </summary>
        MultiplayerLandGrab = 39,
    }
}

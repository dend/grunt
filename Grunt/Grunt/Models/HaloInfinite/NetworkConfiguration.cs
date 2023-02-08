// <copyright file="NetworkConfiguration.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Game network configuration.
    /// </summary>
    /// <remarks>
    /// At this time, a lot of the individual property descriptions are best guesses inferred from their names.
    /// </remarks>
    [IsAutomaticallySerializable]
    public class NetworkConfiguration
    {
        /// <summary>
        /// Gets or sets the highlighted hopper send time in milliseconds.
        /// </summary>
        public int HighlightedHopperSendTimeMs { get; set; }

        /// <summary>
        /// Gets or sets the priority base for non-player motion.
        /// </summary>
        [JsonPropertyName("PrioritiyBaseNonPlayerMotion")]
        public float PriorityBaseNonPlayerMotion { get; set; }

        /// <summary>
        /// Gets or sets the priority maximum threshold.
        /// </summary>
        public int PriorityMaxThreshold { get; set; }

        /// <summary>
        /// Gets or sets the maximum priority.
        /// </summary>
        public float PriorityMax { get; set; }

        /// <summary>
        /// Gets or sets the medium base priority.
        /// </summary>
        public float PriorityMediumBase { get; set; }

        /// <summary>
        /// Gets or sets the medium relevance scale priority.
        /// </summary>
        public float PriorityMediumRelevanceScale { get; set; }

        /// <summary>
        /// Gets or sets the minimum base priority.
        /// </summary>
        public float PriorityMinimumBase { get; set; }

        /// <summary>
        /// Gets or sets the minimum relevance scale priority.
        /// </summary>
        public float PriorityMinimumRelevanceScale { get; set; }

        /// <summary>
        /// Gets or sets the player notification unlistening timeout in seconds.
        /// </summary>
        public int PlayerNotificationUnlistenTimeoutSecs { get; set; }

        /// <summary>
        /// Gets or sets the grief idle controller center threshold.
        /// </summary>
        public int GriefIdleControllerCenterThreshold { get; set; }

        /// <summary>
        /// Gets or sets the grief idle controller hyseresis threshold.
        /// </summary>
        public int GriefIdleControllerHysteresisThreshold { get; set; }

        /// <summary>
        /// Gets or sets the maximum time to join a UDP session after appearing in a game roster.
        /// </summary>
        public int MaxTimeToJoinUDPSessionAfterAppearingInRosterSeconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the transport logic should ignroe network status changes.
        /// </summary>
        public bool? TransportIgnoreNetworkStatusChanged { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of retries to resolve a DNS name.
        /// </summary>
        public int QosMaxNumRetriesResolveDnsName { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of retries to validate a host address.
        /// </summary>
        public int QosMaxNumRetriesValidateHostAddress { get; set; }

        /// <summary>
        /// Gets or sets the time in milliseconds between packet sends.
        /// </summary>
        public int QosTimeBetweenPacketSendsMs { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of retries to send a packet.
        /// </summary>
        public int QosMaxNumRetriesToSendPacket { get; set; }

        /// <summary>
        /// Gets or sets the packet receving timeout in milliseconds.
        /// </summary>
        public int QosPacketRecvTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of retries on packet receipt timeout.
        /// </summary>
        public int QosMaxNumRetriesOnPacketRecvTimeout { get; set; }

        /// <summary>
        /// Gets or sets the packet blackhole timeout in milliseconds.
        /// </summary>
        public int QosPacketBlackholeTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the fraction of minimum packets on packet receipt timeout.
        /// </summary>
        public float QosFractionMinPacketsOnPacketRecvTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a network pollster should be used.
        /// </summary>
        public bool? QosShouldUseNetworkPollster { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of transport resets.
        /// </summary>
        public int QosMaxNumTransportResets { get; set; }

        /// <summary>
        /// Gets or sets the minimum team balancer MMR.
        /// </summary>
        public float TeamBalancerMinMMR { get; set; }

        /// <summary>
        /// Gets or sets the maximum team balancer MMR.
        /// </summary>
        public float TeamBalancerMaxMMR { get; set; }

        /// <summary>
        /// Gets or sets the team balancer MMR quantization steps.
        /// </summary>
        public int TeamBalancerMMRQuantizationSteps { get; set; }

        /// <summary>
        /// Gets or sets the team balancer MMR exponent.
        /// </summary>
        public float TeamBalancerMMRExponent { get; set; }

        /// <summary>
        /// Gets or sets the maximum time in seconds to join a UDP session after appearing in roster after Join in Progress (JIP).
        /// </summary>
        public int MaxTimeToJoinUDPSessionAfterAppearingInRosterDuringJIPSeconds { get; set; }

        /// <summary>
        /// Gets or sets the timeout for host teardown.
        /// </summary>
        public int HostTeardownTimeoutSeconds { get; set; }

        /// <summary>
        /// Gets or sets the lobby connection blocking spinner maximum time in milliseconds.
        /// </summary>
        public int LobbyConnectionBlockingSpinnerMaxTimeMSec { get; set; }

        /// <summary>
        /// Gets or sets the minimum time between low-priority state updates in milliseconds.
        /// </summary>
        public int MinimumTimeBetweenLowPriStateUpdatesMsec { get; set; }

        /// <summary>
        /// Gets or sets the lobby connection delay on initial join in milliseconds.
        /// </summary>
        public int LobbyConnectionDelayOnInitialJoinMsec { get; set; }

        /// <summary>
        /// Gets or sets the number of idle seconds before booting a custom games.
        /// </summary>
        public int IdleSecondsBeforeBootCustomGames { get; set; }

        /// <summary>
        /// Gets or sets the number of idle seconds betore booting the campaign.
        /// </summary>
        public int IdleSecondsBeforeBootCampaign { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether chat should be torn down on entering gameplay. 
        /// </summary>
        public bool? ChatTeardownOnEnteringGameplay { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether chat should be torn down on exiting gameplay.
        /// </summary>
        public bool? ChatTeardownOnExitingGameplay { get; set; }

        /// <summary>
        /// Gets or sets the threshold in milliseconds for main thread hitch detection in release servers.
        /// </summary>
        public int MainThreadHitchDetectionInReleaseServerThresholdMs { get; set; }

        /// <summary>
        /// Gets or sets the maximum interval for UDP safety packets.
        /// </summary>
        public int UDPSafetyPacketMaximumInterval { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of players brute forced by the team balancer.
        /// </summary>
        public int TeamBalancerBruteForceMaxPlayers { get; set; }

        /// <summary>
        /// Gets or sets the seconds count for requisition catalog normal refresh.
        /// </summary>
        public int RequisitionCatalogNormalRefreshSeconds { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds between retries for requisition catalog retries.
        /// </summary>
        public int RequisitionCatalogSecondsBetweenRetries { get; set; }

        /// <summary>
        /// Gets or sets the retry count for the requisition catalog.
        /// </summary>
        public int RequisitionCatalogRetryCount { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds between requisition catalog retry periods.
        /// </summary>
        public int RequisitionCatalogSecondsBetweenRetryPeriods { get; set; }

        /// <summary>
        /// Gets or sets the seconds count for requisition inventory normal refresh.
        /// </summary>
        public int RequisitionInventoryNormalRefreshSeconds { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds between retries for requisition inventory retries.
        /// </summary>
        public int RequisitionInventorySecondsBetweenRetry { get; set; }

        /// <summary>
        /// Gets or sets the retry count for the requisition inventory.
        /// </summary>
        public int RequisitionInventoryRetryCount { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds between requisition inventory retry periods.
        /// </summary>
        public int RequisitionInventorySecondsBetweenRetryPeriods { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether rogue clients trigger immediate heartbeat.
        /// </summary>
        /// <remarks>
        /// This is not a typo. This is exactly how the property is named in the API.
        /// </remarks>
        public bool? RogueClientsTriggerImmediateHeartbeatsRogueClientsTriggerImmediateHeartbeats { get; set; }

        /// <summary>
        /// Gets or sets the maximum amount of time in milliseconds to wait for rogue clietns to appear in roster.
        /// </summary>
        public int MaxTimeToWaitForRogueClientsToAppearInRosterMs { get; set; }

        /// <summary>
        /// Gets or sets the time out in milliseconds for the intial UDP ACK.
        /// </summary>
        public int UDPInitialAckTimeoutMsec { get; set; }

        /// <summary>
        /// Gets or sets the maximum round trip time (RTT) in milliseconds for automatic congestion control.
        /// </summary>
        public int UDPBandwidthControlMaximumRttForAutomaticCongestionMsec { get; set; }

        /// <summary>
        /// Gets or sets the permitted round trip time (RTT) deviations from the predefined (locked) RTT.
        /// </summary>
        public float UDPBandwidthControlCongestingRttPermittedDeviationsFromLockedRtt { get; set; }

        /// <summary>
        /// Gets or sets the drift window latency deviation multiplier.
        /// </summary>
        public float UDPBandwidthControlDriftWindowLatencyDeviationMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the congested stream throttle bandwidth multiplier.
        /// </summary>
        public int UDPBandwidthControlThrottleCongestedStreamBandwidthMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the stream maximum bandwidth delta.
        /// </summary>
        public int UDPBandwidthControlStreamMaximumBandwidthMaximumDelta { get; set; }

        /// <summary>
        /// Gets or sets the stream maximum bandwidth skips.
        /// </summary>
        public int UDPBandwidthControlStreamMaximumBandwidthSkipMax { get; set; }

        /// <summary>
        /// Gets or sets the stream probe failure limit.
        /// </summary>
        public int UDPBandwidthControlStreamProbeFailureLimit { get; set; }

        /// <summary>
        /// Gets or sets the stream retry growth interval in milliseconds.
        /// </summary>
        public int UDPBandwidthControlStreamRetryGrowthIntervalMsec { get; set; }

        /// <summary>
        /// Gets or sets the time to redo all results in millseconds.
        /// </summary>
        public int QoSRedoAllResultsTimeMs { get; set; }

        /// <summary>
        /// Gets or sets the UDP stream minimum bytes per second (BPS).
        /// </summary>
        public int UDPStreamMinimumBps { get; set; }

        /// <summary>
        /// Gets or sets whether the UDP bandwidth control is enabled.
        /// </summary>
        public bool? UDPBandwidthControlEnabled { get; set; }

        /// <summary>
        /// Gets or sets the minimum control query time in milliseconds.
        /// </summary>
        public int UDPBandwidthControlQueryTimeMinimumMs { get; set; }

        /// <summary>
        /// Gets or sets the control query time after delay in milliseconds.
        /// </summary>
        public int UDPBandwidthControlQueryTimeAfterDelayMs { get; set; }

        /// <summary>
        /// Gets or sets the time to settle a control probe in milliseconds.
        /// </summary>
        public int UDPBandwidthControlProbeSettleTimeMs { get; set; }

        /// <summary>
        /// Gets or sets the minimum time to recover in milliseconds.
        /// </summary>
        public int UDPBandwidthControlRecoverMinimumTimeMs { get; set; }

        /// <summary>
        /// Gets or sets the minimum throttle rollback in milliseconds.
        /// </summary>
        public int UDPBandwidthControlThrottleMinimumRollbackMs { get; set; }

        /// <summary>
        /// Gets or sets the UDP probe growth rate.
        /// </summary>
        public float UDPBandwidthControlProbeGrowthRate { get; set; }

        /// <summary>
        /// Gets or sets the UDP pollster heartbeat timeout in milliseconds.
        /// </summary>
        public int UDPPollsterHeartbeatTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the minimum packet size.
        /// </summary>
        public int UDPBandwidthControlMinPacketSize { get; set; }

        /// <summary>
        /// Gets or sets the UDP connect request timeout in milliseconds.
        /// </summary>
        public int UDPConnectRequestTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the UDP establishment timeout in milliseconds.
        /// </summary>
        public int UDPEstablishTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the UDP session join initial update timeout in milliseconds.
        /// </summary>
        public int UDPSessionJoinInitialUpdateTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the simulation join timeout in milliseconds.
        /// </summary>
        public int SimulationJoinTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the simulation host join minimum wait time in milliseconds.
        /// </summary>
        public int SimulationHostJoinMinimumWaitTimeMs { get; set; }

        /// <summary>
        /// Gets or sets the simulation host join timeout in milliseconds.
        /// </summary>
        public int SimulationHostJoinTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the simulation join total wait timeout in milliseconds.
        /// </summary>
        public int SimulationJoinTotalWaitTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the timeout in milliseconds for the simulation to wait for all client activations.
        /// </summary>
        public int SimulationWaitForAllClientActivationTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the timeout in milliseconds for the simulation state change related to starting a pause.
        /// </summary>
        public int SimulationChangingStateStartPauseTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the timeout in milliseconds for the simulation state change related to ending a pause.
        /// </summary>
        public int SimulationChangingStateEndPauseTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the UDP connection active send timeout in milliseconds.
        /// </summary>
        public int UDPConnectionActiveSendTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the minimum active time in milliseconds to drop a UDP connection.
        /// </summary>
        public int UDPConnectionDropMinimumActiveTimeMs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use an ephemeral port.
        /// </summary>
        public bool? UDPUseEphemeralPort { get; set; }

        /// <summary>
        /// Gets or sets the maximum count for game variants.
        /// </summary>
        public int UgcMaxItemCountGameVariant { get; set; }

        /// <summary>
        /// Gets or sets the maximum count for map variants.
        /// </summary>
        public int UgcMaxItemCountMapVariant { get; set; }

        /// <summary>
        /// Gets or sets the maximum count for screenshots.
        /// </summary>
        public int UgcMaxItemCountScreenshot { get; set; }

        /// <summary>
        /// Gets or sets the maximum count for bookmarks.
        /// </summary>
        public int UgcMaxItemCountBookmark { get; set; }

        /// <summary>
        /// Gets or sets the maximum count for Forge object groups.
        /// </summary>
        public int UgcMaxItemCountForgeObjectGroup { get; set; }

        /// <summary>
        /// Gets or sets the maximum count for films.
        /// </summary>
        public int UgcMaxItemCountFilm { get; set; }

        /// <summary>
        /// Gets or sets the retry time in milliseconds for User-Generated Content (UGC) items.
        /// </summary>
        public int UgcItemCountRetryMs { get; set; }

        /// <summary>
        /// Gets or sets the retry count for User-Generated Content (UGC).
        /// </summary>
        public int UgcItemCountRetryCount { get; set; }

        /// <summary>
        /// Gets or sets the time in seconds to pause retry for User-Generated Content (UGC).
        /// </summary>
        public int UgcItemCountRetryPauseSecs { get; set; }

        /// <summary>
        /// Gets or sets the number of idle seconds to boot Forge.
        /// </summary>
        public int IdleSecondsBeforeBootForge { get; set; }

        /// <summary>
        /// Gets or sets the time in minutes to force a hopper refresh.
        /// </summary>
        public int ForceHopperRefreshTimeMinutes { get; set; }

        /// <summary>
        /// Gets or sets the time to wait before game sesison grain state validation is complete.
        /// </summary>
        public int TimeToWaitBeforeGameSessionGrainStateValidation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the exit experience failure telemetry is enabled.
        /// </summary>
        public bool? EnableExitExperienceFailureTelemetry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether throttle is allowed on packet loss.
        /// </summary>
        public bool? UDPBandwidthControlThrottleAllowedOnPacketLoss { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether projectile discrepancies are enabled.
        /// </summary>
        public bool? CellRecordProjectileDiscrepancies { get; set; }

        /// <summary>
        /// Gets or sets the simulation warp client delay in seconds.
        /// </summary>
        public int CellRecordSimulationWarpClientDelayInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the simulation warp host delay in seconds.
        /// </summary>
        public int CellRecordSimulationWarpHostDelayInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the histogram length in frames.
        /// </summary>
        public int CellSimulationHistogramLengthInFrames { get; set; }

        /// <summary>
        /// Gets or sets the histogram threshold in points.
        /// </summary>
        public float CellSimulationHistogramThresholdInPct { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the film playback speed needs to be adjusted.
        /// </summary>
        public bool? AdjustFilmPlaybackSpeed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether input polling is done on a separate thread.
        /// </summary>
        public bool? InputPollingOnSeparateThread { get; set; }

        /// <summary>
        /// Gets or sets the threshold for weapon age misprediction.
        /// </summary>
        public float CellSimulationWeaponAgeMispredictThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold in seconds for projectile creation delay.
        /// </summary>
        public float CellSimulationProjectileCreationDelayThresholdInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the bad ping latency threshold in milliseconds.
        /// </summary>
        public int QoSBadPingLatencyThresholdMsec { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the 2015 matchmaking configuration is enabled.
        /// </summary>
        public bool? Matchmaking2015Enabled { get; set; }

        /// <summary>
        /// Gets or sets the threshold for blend orientation delta.
        /// </summary>
        public float CellSimulationBlendOrientationDeltaThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold for blend position delta.
        /// </summary>
        public float CellSimulationBlendPositionDeltaThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold for nudge linear velocity delta.
        /// </summary>
        public float CellSimulationNudgeLinearVelocityDeltaThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold for nudge angular velocity delta.
        /// </summary>
        public float CellSimulationNudgeAngularVelocityDeltaThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold for loaded rounds misprediction.
        /// </summary>
        public int CellSimulationLoadedRoundsMispredictThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold for inventory rounds misprediction.
        /// </summary>
        public int CellSimulationInventoryRoundsMispredictThreshold { get; set; }

        /// <summary>
        /// Gets or sets the pickup delay threshold in seconds.
        /// </summary>
        public float CellSimulationPickupDelayThresholdInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the game time miss threshold in seconds.
        /// </summary>
        public float CellSimulationGameTimeMissThresholdInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the latency threshold in milliseconds.
        /// </summary>
        public float CellPerformanceLatencyThresholdMS { get; set; }

        /// <summary>
        /// Gets or sets the latency below threshold timeout value in milliseconds.
        /// </summary>
        public int CellPerformanceLatencyBelowThresholdTimeoutMS { get; set; }

        /// <summary>
        /// Gets or sets the latency tracked time for sends in milliseconds.
        /// </summary>
        public int CellPerformanceLatencyTrackedTimeForSendMS { get; set; }

        /// <summary>
        /// Gets or sets the latency maximum events to send.
        /// </summary>
        public int CellPerformanceLatencyMaxEventsToSend { get; set; }

        /// <summary>
        /// Gets or sets the total memory usage poll interval in seconds.
        /// </summary>
        public float TotalMemoryUsagePollIntervalInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the total memory usage update interval in seconds.
        /// </summary>
        public float TotalMemoryUsageUpdateIntervalInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the status telemetry update interval.
        /// </summary>
        public int CellPlayerStatusTelemetryUpdateInterval { get; set; }

        /// <summary>
        /// Gets or sets the ready room timer duration in seconds.
        /// </summary>
        public int ReadyRoomTimerDurationSeconds { get; set; }

        /// <summary>
        /// Gets or sets the fraction for paused game required machines.
        /// </summary>
        public float SimulationPauseGameRequiredMachinesFraction { get; set; }

        /// <summary>
        /// Gets or sets the fraction for join activation blocking machines.
        /// </summary>
        public float SimulationJoinActivationBlockingMachinesFraction { get; set; }

        /// <summary>
        /// Gets or sets the presence refresh time in seconds.
        /// </summary>
        public int PresenceRefreshTimeSeconds { get; set; }
    }
}

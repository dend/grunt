/**
 * API endpoint configuration from settings service.
 */
export interface Configuration {
  /** Authority definitions */
  authorities?: Record<string, Authority>;
  /** Retry policy definitions */
  retryPolicies?: Record<string, RetryPolicyConfig>;
  /** Settings values */
  settings?: SettingsConfig;
  /** Endpoint definitions */
  endpoints?: Record<string, OnlineUriReference>;
}

/**
 * Authority (API service) definition.
 */
export interface Authority {
  /** Authority identifier */
  authorityId?: string;
  /** URL scheme (1 = http, 2 = https) */
  scheme?: number;
  /** Hostname */
  hostname?: string;
  /** Port number */
  port?: number;
  /** Authentication methods */
  authenticationMethods?: number[];
}

/**
 * Retry policy configuration.
 */
export interface RetryPolicyConfig {
  /** Policy identifier */
  retryPolicyId?: string;
  /** Timeout in milliseconds */
  timeoutMs?: number;
  /** Retry options */
  retryOptions?: RetryOptionsConfig;
}

/**
 * Retry options.
 */
export interface RetryOptionsConfig {
  /** Maximum retry count */
  maxRetryCount?: number;
  /** Initial retry delay in milliseconds */
  retryDelayMs?: number;
  /** Retry delay growth factor */
  retryGrowth?: number;
  /** Random jitter to add in milliseconds */
  retryJitterMs?: number;
  /** Whether to retry on 404 */
  retryIfNotFound?: boolean;
}

/**
 * Online URI reference (endpoint definition).
 */
export interface OnlineUriReference {
  /** Endpoint identifier */
  endpointId?: string;
  /** Authority identifier */
  authorityId?: string;
  /** Path template */
  path?: string;
  /** Query string template */
  queryString?: string;
  /** Retry policy identifier */
  retryPolicyId?: string;
  /** Topic name */
  topicName?: string;
  /** Acknowledgement type */
  acknowledgementTypeId?: number;
  /** Whether auth lifetime extension is supported */
  authenticationLifetimeExtensionSupported?: boolean;
  /** Whether endpoint is clearance-aware */
  clearanceAware?: boolean;
}

/**
 * Settings configuration.
 */
export interface SettingsConfig {
  /** CELL config */
  cellConfig?: string;
  /** Client QoS timeout */
  clientQoSTimeoutMs?: string;
  /** Clearance audience */
  clearanceAudience?: string;
  /** Playfab title ID */
  playfabTitleId?: string;
  /** Title ID list */
  titleIdList?: string;
  /** XSTS audience URI */
  haloXSTSAudienceUri?: string;
  /** Product access list */
  productAccessList?: string;
}

/**
 * Flight feature flags.
 */
export interface FlightedFeatureFlags {
  /** Flight identifier */
  flightId?: string;
  /** Clearance identifier */
  clearanceId?: string;
  /** Feature flags */
  flags?: Record<string, boolean>;
}

/**
 * Player clearance/flight configuration ID.
 */
export interface PlayerClearance {
  /** Flight configuration identifier */
  flightConfigurationId?: string;
}

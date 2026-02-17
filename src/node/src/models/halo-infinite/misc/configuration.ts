/**
 * API endpoint configuration from settings service.
 */
export interface Configuration {
  /** Authority definitions */
  Authorities?: Record<string, Authority>;
  /** Retry policy definitions */
  RetryPolicies?: Record<string, RetryPolicyConfig>;
  /** Settings values */
  Settings?: SettingsConfig;
  /** Endpoint definitions */
  Endpoints?: Record<string, OnlineUriReference>;
}

/**
 * Authority (API service) definition.
 */
export interface Authority {
  /** Authority identifier */
  AuthorityId?: string;
  /** URL scheme (1 = http, 2 = https) */
  Scheme?: number;
  /** Hostname */
  Hostname?: string;
  /** Port number */
  Port?: number;
  /** Authentication methods */
  AuthenticationMethods?: number[];
}

/**
 * Retry policy configuration.
 */
export interface RetryPolicyConfig {
  /** Policy identifier */
  RetryPolicyId?: string;
  /** Timeout in milliseconds */
  TimeoutMs?: number;
  /** Retry options */
  RetryOptions?: RetryOptionsConfig;
}

/**
 * Retry options.
 */
export interface RetryOptionsConfig {
  /** Maximum retry count */
  MaxRetryCount?: number;
  /** Initial retry delay in milliseconds */
  RetryDelayMs?: number;
  /** Retry delay growth factor */
  RetryGrowth?: number;
  /** Random jitter to add in milliseconds */
  RetryJitterMs?: number;
  /** Whether to retry on 404 */
  RetryIfNotFound?: boolean;
}

/**
 * Online URI reference (endpoint definition).
 */
export interface OnlineUriReference {
  /** Endpoint identifier */
  EndpointId?: string;
  /** Authority identifier */
  AuthorityId?: string;
  /** Path template */
  Path?: string;
  /** Query string template */
  QueryString?: string;
  /** Retry policy identifier */
  RetryPolicyId?: string;
  /** Topic name */
  TopicName?: string;
  /** Acknowledgement type */
  AcknowledgementTypeId?: number;
  /** Whether auth lifetime extension is supported */
  AuthenticationLifetimeExtensionSupported?: boolean;
  /** Whether endpoint is clearance-aware */
  ClearanceAware?: boolean;
}

/**
 * Settings configuration.
 */
export interface SettingsConfig {
  /** CELL config */
  CellConfig?: string;
  /** Client QoS timeout */
  ClientQoSTimeoutMs?: string;
  /** Clearance audience */
  ClearanceAudience?: string;
  /** Playfab title ID */
  PlayfabTitleId?: string;
  /** Title ID list */
  TitleIdList?: string;
  /** XSTS audience URI */
  HaloXSTSAudienceUri?: string;
  /** Product access list */
  ProductAccessList?: string;
}

/**
 * Flight feature flags.
 */
export interface FlightedFeatureFlags {
  /** Flight identifier */
  FlightId?: string;
  /** Clearance identifier */
  ClearanceId?: string;
  /** Feature flags */
  Flags?: Record<string, boolean>;
}

/**
 * Player clearance/flight configuration ID.
 */
export interface PlayerClearance {
  /** Flight configuration identifier */
  FlightConfigurationId?: string;
}

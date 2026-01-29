/**
 * Types of user-generated content assets.
 *
 * Used with UGC modules to specify the type of asset.
 */
export const AssetKind = {
  /** Film/theater recording */
  Film: 'Film',
  /** Custom map/forge creation */
  Map: 'Map',
  /** Prefabricated object collection */
  Prefab: 'Prefab',
  /** User-created game variant */
  UgcGameVariant: 'UgcGameVariant',
  /** Map-mode pairing */
  MapModePair: 'MapModePair',
  /** Playlist definition */
  Playlist: 'Playlist',
  /** Engine game variant */
  EngineGameVariant: 'EngineGameVariant',
  /** Project (forge project) */
  Project: 'Project',
} as const;

/**
 * Type representing valid asset kind values.
 */
export type AssetKind = (typeof AssetKind)[keyof typeof AssetKind];

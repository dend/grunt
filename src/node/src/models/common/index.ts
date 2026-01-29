/**
 * Common types used across the Grunt library.
 */

export {
  HaloApiResult,
  RawResponse,
  isSuccess,
  isNotModified,
  isClientError,
  isServerError,
} from './api-result';

export { ApiContentType, getContentTypeHeader } from './api-content-type';

export type {
  HaloInfiniteClientOptions,
  WaypointClientOptions,
  RequestOptions,
} from './client-options';

/**
 * Code redemption result.
 */
export interface CodeRedemptionResult {
  /** The code that was redeemed */
  code?: string;
  /** Name of the offer/reward */
  offerName?: string;
  /** Whether redemption was successful */
  success?: boolean;
  /** Error message if failed */
  errorMessage?: string;
}

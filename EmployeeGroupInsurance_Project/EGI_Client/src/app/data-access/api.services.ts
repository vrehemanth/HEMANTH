/**
 * @deprecated
 * This barrel file exists purely for backward compatibility.
 * Import directly from the dedicated service files instead:
 *
 *   import { AdminService }        from './admin.service';
 *   import { AgentService }        from './agent.service';
 *   import { CustomerService }     from './customer.service';
 *   import { ClaimsOfficerService } from './claims-officer.service';
 *   import { PublicService }       from './public.service';
 */

export { AdminService }         from './admin.service';
export { AgentService }         from './agent.service';
export { CustomerService }      from './customer.service';
export { ClaimsOfficerService } from './claims-officer.service';
export { PublicService }        from './public.service';

// Keep API_BASE exported for any legacy consumers
export { environment as _env } from '../../environments/environment';
import { environment } from '../../environments/environment';
export const API_BASE = environment.apiBase;

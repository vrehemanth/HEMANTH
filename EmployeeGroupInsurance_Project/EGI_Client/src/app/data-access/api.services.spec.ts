import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AdminService, AgentService, CustomerService, ClaimsOfficerService, PublicService, API_BASE } from './api.services';

describe('ApiServices', () => {
    let httpTestingController: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [AdminService, AgentService, CustomerService, ClaimsOfficerService, PublicService]
        });
        httpTestingController = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        // Assert that there are no outstanding requests
        httpTestingController.verify();
    });

    it('should be created for AdminService', () => {
        const service = TestBed.inject(AdminService);
        expect(service).toBeTruthy();
    });

    it('AdminService should fetch all clients', () => {
        const service = TestBed.inject(AdminService);
        const mockClients = [{ id: '1', name: 'John Doe' }];

        service.getAllClients().subscribe((clients: any) => {
            expect(clients).toEqual(mockClients);
            expect(clients.length).toBe(1);
        });

        const req = httpTestingController.expectOne(`${API_BASE}/admin/dashboard/clients`);
        expect(req.request.method).toEqual('GET');
        req.flush(mockClients);
    });

    it('AgentService should fetch clients assigned to agent', () => {
        const service = TestBed.inject(AgentService);
        const mockClients = [{ id: '1', companyName: 'Acme Corp' }];

        service.getMyCustomers().subscribe((clients: any) => {
            expect(clients).toEqual(mockClients);
        });

        const req = httpTestingController.expectOne(`${API_BASE}/agent/dashboard/my-customers`);
        expect(req.request.method).toEqual('GET');
        req.flush(mockClients);
    });

    it('CustomerService should fetch customer policy', () => {
        const service = TestBed.inject(CustomerService);
        const mockPolicies = [{ id: 'pol-123', status: 'Active' }];

        service.getMyPolicies().subscribe((policies: any) => {
            expect(policies).toEqual(mockPolicies);
            expect(policies[0].status).toBe('Active');
        });

        const req = httpTestingController.expectOne(`${API_BASE}/customer/dashboard/my-policies`);
        expect(req.request.method).toEqual('GET');
        req.flush(mockPolicies);
    });

    it('ClaimsOfficerService should verify claims by policy ID', () => {
        const service = TestBed.inject(ClaimsOfficerService);
        const mockClaims = [{ claimId: 'c1', amount: 500 }];

        service.getClaimsByPolicy('pol-123').subscribe((claims: any) => {
            expect(claims).toEqual(mockClaims);
            expect(claims[0].amount).toBe(500);
        });

        const req = httpTestingController.expectOne(`${API_BASE}/claims-officer/dashboard/claims/policy/pol-123`);
        expect(req.request.method).toEqual('GET');
        req.flush(mockClaims);
    });

    it('PublicService should fetch public insurance plans from the backend', () => {
        const service = TestBed.inject(PublicService);
        const mockPlans = [{ planName: 'Gold', basePremium: 500 }];

        service.getInsurancePlans().subscribe((plans: any) => {
            expect(plans).toEqual(mockPlans);
            expect(plans[0].planName).toBe('Gold');
        });

        const req = httpTestingController.expectOne(`${API_BASE}/public/insurance-plans`);
        expect(req.request.method).toEqual('GET');
        req.flush(mockPlans);
    });

    it('AdminService should retrieve global statistical summary metrics', () => {
        const service = TestBed.inject(AdminService);
        const mockSummary = { activeUsers: 400 };

        service.getSummary().subscribe((summary: any) => {
            expect(summary).toEqual(mockSummary);
        });

        const req = httpTestingController.expectOne(`${API_BASE}/admin/dashboard/summary`);
        expect(req.request.method).toEqual('GET');
        req.flush(mockSummary);
    });

    it('AgentService should securely construct POST requests for Customer creation formData', () => {
        const service = TestBed.inject(AgentService);
        const fd = new FormData();
        fd.append('testKey', 'value123');

        service.createCustomer(fd).subscribe((r: any) => {
            expect(r.created).toBe(true);
        });

        const req = httpTestingController.expectOne(`${API_BASE}/agent/dashboard/create-customer`);
        expect(req.request.method).toEqual('POST');
        req.flush({ created: true });
    });

});

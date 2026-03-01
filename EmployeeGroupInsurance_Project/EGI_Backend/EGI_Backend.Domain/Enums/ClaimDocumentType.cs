namespace EGI_Backend.Domain.Enums
{
    public enum ClaimDocumentType
    {
        MedicalBill = 1,
        HospitalDischargeReport = 2,
        DoctorPrescription = 3,
        FIR = 4,                    // For Accident claims
        PostMortemReport = 5,       // For Life/Death claims
        DeathCertificate = 6,       // For Life claims
        AccidentReport = 7,         // For Accident claims
        DiagnosisReport = 8,        // For Critical Illness claims
        InsuranceClaimForm = 9,     // Standard claim form
        Other = 10
    }
}

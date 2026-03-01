namespace EGI_Backend.Domain.Enums
{
    public enum CoveredGroup
    {
        EmployeeOnly,               // Employee matches only
        EmployeeAndFamily,          // Employee, Spouse, Children
        EmployeeFamilyAndParents    // Employee, Spouse, Children, Parents
    }
}

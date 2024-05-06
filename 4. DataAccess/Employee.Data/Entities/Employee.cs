using System;
using System.Collections.Generic;

namespace Employee.Data.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public int DesignationId { get; set; }

    public int DepartmentId { get; set; }

    public int? KnowledgeOf { get; set; }

    public int? Salary { get; set; }

    public DateTime JoiningDate { get; set; }

    public int? ReportingPerson { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeEmail { get; set; } = null!;

    public string EmployeePassword { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual Designation Designation { get; set; } = null!;

    public virtual Knowledge? KnowledgeOfNavigation { get; set; }
}

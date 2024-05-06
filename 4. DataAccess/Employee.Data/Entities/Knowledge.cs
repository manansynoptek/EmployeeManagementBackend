using System;
using System.Collections.Generic;

namespace Employee.Data.Entities;

public partial class Knowledge
{
    public int KnowledgeId { get; set; }

    public string? KnowledgeName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

using System;
using System.Collections.Generic;

namespace jobEvent.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? Departmentdirector { get; set; }

    public int? Mainsupervisor { get; set; }

    public virtual Employee? Departmentdirector1 { get; set; }

    public virtual Department? DepartmentdirectorNavigation { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Department> InverseDepartmentdirectorNavigation { get; set; } = new List<Department>();

    public virtual ICollection<Department> Departmentdepends { get; set; } = new List<Department>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}

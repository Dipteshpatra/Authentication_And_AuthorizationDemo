using System;
using System.Collections.Generic;

namespace Authentication_And_Authorization.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string? EmpName { get; set; }

    public string? EmpRole { get; set; }

    public string? EmpPassword { get; set; }
}

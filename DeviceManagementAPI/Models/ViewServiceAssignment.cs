using System;
using System.Collections.Generic;

namespace DeviceManagementAPI.Models
{
    public partial class ViewServiceAssignment
    {
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public long? TotalAssignments { get; set; }
        public double? Year { get; set; }
        public double? Month { get; set; }
        public double? Week { get; set; }
    }
}

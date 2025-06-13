using System;
using System.Collections.Generic;

namespace DeviceManagementAPI.Models
{
    public partial class ViewAssignmentsPerDevice
    {
        public string? DeviceCode { get; set; }
        public string? DeviceName { get; set; }
        public long? TotalAssignments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spartan.Client.Models
{
    public class SearchViewModel
    {
        public List<EquipmentBreakdown> equipmentBreakdowns = new List<EquipmentBreakdown>();
    }

    public class EquipmentBreakdown
    {
        public uint ExternalId;
        public uint MeterReading;
        public string EquipmentDescription;
    }
}

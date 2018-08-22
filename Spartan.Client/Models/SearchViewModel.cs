using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spartan.Client.Models
{
    public class SearchViewModel
    {
        public List<EquipmentBreakdown> equipmentBreakdowns = null;
        public List<EquipmentType> equipmentTypes = null;
        public enum DisplayMode { equipment_breakdown = 0, equipment_types = 1};
        public DisplayMode displayMode = DisplayMode.equipment_breakdown;
    }
    

    public class EquipmentBreakdown
    {
        public uint ExternalId;
        public uint MeterReading;
        public string EquipmentDescription;
    }
}

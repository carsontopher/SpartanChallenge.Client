using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spartan.Client.Models
{
    public class SerialEquipment
    {
        public string Id;
        public uint ExternalId;
        public string EquiptmentTypeId;
        public uint MeterReading;
    }

    public class EquipmentType
    {
        public string Id;
        public uint ExternalId;
        public string Description;
    }


    public class Data
    {
        public List<SerialEquipment> serialisedEquipment;
        public List<EquipmentType> equipmentType;
    }

}

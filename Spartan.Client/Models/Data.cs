using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spartan.Client.Models
{
    public class SerialEquipment
    {
        // normal Id public key
        public string Id;
        // external reference number?
        public uint ExternalId;
        // foreign key link to equipment table
        public string EquipmentTypeId;
        // meter reading for this equipment
        public uint MeterReading;

        /// <summary>
        /// Find the equipment type from the serial equipment
        /// </summary>
        /// <param name="equipment"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static EquipmentType GetEquipmentType( SerialEquipment equipment, Data table )
        {
            foreach(var v in table.equipmentType)
            {
                if(v.Id == equipment.EquipmentTypeId)
                {
                    return v;
                }
            }

            // oh no, error D:
            return null;
        }
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Spartan.Client.Models;
using System.IO;

namespace Spartan.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string search, string options)
        {
            // load the json file from the hard drive
            Data jsonFile = JsonConvert.DeserializeObject<Data>(System.IO.File.ReadAllText("./EquipmentData.json"));
            // make a new view model, which also makes a new list in the view model to write to.
            SearchViewModel searchViewModel = new SearchViewModel();

            var list = new List<SerialEquipment>();

            // if our search is empty
            if (string.IsNullOrEmpty(search))
            {
                // return everything :D
                list = jsonFile.serialisedEquipment;
            }
            else
            {
                // otherwise actually search D:
                list = jsonFile.serialisedEquipment.FindAll(f => f.ExternalId.ToString().Contains(search));
            }
            
            // iterate over all the elements returned by the json file.
            foreach (var item in list)
            {
                // retrieve the equipment type from the type id (foreign key)
                var equipment = SerialEquipment.GetEquipmentType(item, jsonFile);

                // add the element with only the relevant data for the frontend into the view model
                searchViewModel.equipmentBreakdowns.Add(new EquipmentBreakdown
                {
                    ExternalId = item.ExternalId,
                    MeterReading = item.MeterReading,
                    EquipmentDescription = equipment.Description
                });
            }

            // pass data from the query back to the client.
            return View(searchViewModel);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

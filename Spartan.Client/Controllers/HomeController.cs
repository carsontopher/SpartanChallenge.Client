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
        public IActionResult Index(string search, int options)
        {
            // load the json file from the hard drive
            Data jsonFile = JsonConvert.DeserializeObject<Data>(System.IO.File.ReadAllText("./EquipmentData.json"));
            // make a new view model, which also makes a new list in the view model to write to.
            SearchViewModel searchViewModel = new SearchViewModel();
            // set the display mode for the frontend
            searchViewModel.displayMode = (SearchViewModel.DisplayMode) options;

            // switch based on the supplied display mode
            switch(searchViewModel.displayMode)
            {
                case SearchViewModel.DisplayMode.equipment_breakdown:
                    {
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

                        // make new instance of list in view model
                        searchViewModel.equipmentBreakdowns = new List<EquipmentBreakdown>();

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
                    }
                    break;
                case SearchViewModel.DisplayMode.equipment_types:

                    if (string.IsNullOrEmpty(search))
                    {


                        // set the equipment type list to the one directly from the file (since no data join is required)
                        searchViewModel.equipmentTypes = jsonFile.equipmentType;
                    }
                    else
                    {
                        // otherwise actually search D:
                        searchViewModel.equipmentTypes = jsonFile.equipmentType.FindAll(f => f.ExternalId.ToString().Contains(search));
                    }
                    break;
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

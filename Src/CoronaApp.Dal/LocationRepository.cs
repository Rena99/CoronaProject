using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoronaApp.Dal;
using CoronaApp.Services;
using Microsoft.EntityFrameworkCore;

namespace CoronaApp.Dal
{
    public class LocationRepository : ILocationRepository
    {
        public LocationRepository()
        {
        }
        private readonly CoronaContext context;
        public LocationRepository(CoronaContext context)
        {
            this.context = context;
        }
        public List<Location> GetAllList()
        {
            var listPatient = new List<Location>();
            try
            {

                foreach (var item in context.Patient.Include(x => x.Path))
                {
                    foreach (var item2 in item.Path)
                    {
                        listPatient.Add(item2);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); throw;
            }
            return listPatient;
        }

        public async Task<List<Location>> SearchBy(LocationSearch locationSearch)
        {
            var searchList = new List<Location>();
            //if (locationSearch.City != null)
            //{
            //    searchList = (List<Location>)SearchByDate(locationSearch);
            //}
            if (locationSearch.City == null)
            {
                searchList = SearchByDate(locationSearch);
            }
            else
            {
                searchList = await SearchByCity(locationSearch);
            }

            return searchList;
        }
        public async Task<List<Location>> SearchByCity(LocationSearch locationSearch)
        {
            var listOfSpecificCity = await context.Location.Where(l => l.City == locationSearch.City).ToListAsync();
            return listOfSpecificCity;
        }
        public List<Location> SearchByDate(LocationSearch locationSearch)
        {
            var listPatient = new List<Location>();
            foreach (var item in context.Patient.Include(x => x.Path))
            {
                foreach (var item2 in item.Path)
                {
                    if (item2.StartDate > locationSearch.StartDate /*&& item2.EndDate<locationSearch.EndDate*/ )
                    {
                        listPatient.Add(item2);
                    }
                }
            }
            //var listPatient = context.Location
            //    .Where(l => l.StartDate > locationSearch.StartDate &&
            //    l.EndDate < locationSearch.EndDate).ToList();
            return listPatient;
        }


        public async Task<ICollection<Location>> SearchByAllParams(LocationSearch locationSearch)
        {
            var listPatient = await context.Location.Where(l => l.City == locationSearch.City&&
            l.StartDate > locationSearch.StartDate &&
                l.EndDate < locationSearch.EndDate).ToListAsync();
            return listPatient;
        }
    }

}

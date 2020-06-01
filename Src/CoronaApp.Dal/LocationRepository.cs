
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoronaApp.Dal;
using CoronaApp.Services;
using Microsoft.EntityFrameworkCore;

namespace CoronaApp.Dal
{
    public class LocationRepository : ILocationRepository
    {
        private readonly string con;
        public LocationRepository(string _con)
        {
            _con = con;
        }
        public LocationRepository()
        {

        }
        ICollection<Location> ILocationRepository.GetAllList(List<Location> listPatient)
        {
            //var listPatient = new List<Location>();
            CoronaContext context = new CoronaContext();

            foreach (var item in context.Patient.Include(x => x.Path))
            {
                foreach (var item2 in item.Path)
                {
                    listPatient.Add(item2);
                }
            }
            return listPatient;
        }

        public ICollection<Location> SearchBy(LocationSearch locationSearch)
        {
            var searchList = new List<Location>();
            //if (locationSearch.City != null)
            //{
            //    searchList = (List<Location>)SearchByDate(locationSearch);
            //}
            if (locationSearch.City == null)
            {
                searchList = (List<Location>)SearchByDate(locationSearch);
            }
            else
            {
                searchList = (List<Location>)SearchByCity(locationSearch);
            }

            return searchList;
        }
        public ICollection<Location> SearchByCity(LocationSearch locationSearch)
        {
            CoronaContext context = new CoronaContext();
            var listOfSpecificCity = context.Location.Where(l => l.City == locationSearch.City).ToList();
            return listOfSpecificCity;
        }
        public ICollection<Location> SearchByDate(LocationSearch locationSearch)
        {
            var listPatient = new List<Location>();
            CoronaContext context = new CoronaContext();
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


        public ICollection<Location> SearchByAllParams(LocationSearch locationSearch)
        {
            CoronaContext context = new CoronaContext();
            var listPatient = context.Location.Where(l => l.City == locationSearch.City&&
            l.StartDate > locationSearch.StartDate &&
                l.EndDate < locationSearch.EndDate).ToList();
            return listPatient;
        }
    }

}

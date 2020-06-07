using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaApp.Services
{
   public class LocationService : ILocationService
    {
        public ILocationRepository ILocationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            ILocationRepository = locationRepository;
        }

        public ICollection<Location> Get(LocationSearch locationSearch)
        {
            return ILocationRepository.SearchBy(locationSearch);
        }

        public ICollection<Location> GetAllList(List<Location> locations)
        {
            return ILocationRepository.GetAllList(locations);
        }

      
    }
}

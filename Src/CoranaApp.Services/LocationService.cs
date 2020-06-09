using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaApp.Services
{
   public class LocationService : ILocationService
    {
        public ILocationRepository ILocationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            ILocationRepository = locationRepository;
        }

        public async Task<List<Location>> Get(LocationSearch locationSearch)
        {
            return await ILocationRepository.SearchBy(locationSearch);
        }

        public ICollection<Location> GetAllList(List<Location> locations)
        {
            return ILocationRepository.GetAllList(locations);
        }

      
    }
}

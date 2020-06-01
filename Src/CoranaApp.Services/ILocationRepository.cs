using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaApp.Services
{
    public interface ILocationRepository
    {
        ICollection<Location> SearchByCity(LocationSearch locationSearch);
        ICollection<Location> GetAllList(List<Location> locations);
        ICollection<Location> SearchByDate(LocationSearch locationSearch);
        ICollection<Location> SearchBy(LocationSearch locationSearch);
        ICollection<Location> SearchByAllParams(LocationSearch locationSearch);

    }
}

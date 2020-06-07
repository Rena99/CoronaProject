using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaApp.Services
{
    public interface ILocationService
    {
        ICollection<Location> GetAllList(List<Location> locations);
        ICollection<Location> Get(LocationSearch locationSearch);

    }
}

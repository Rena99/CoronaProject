using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaApp.Services
{
    public interface ILocationService
    {
        ICollection<Location> GetAllList(List<Location> locations);
        Task<List<Location>> Get(LocationSearch locationSearch);
            
    }
}

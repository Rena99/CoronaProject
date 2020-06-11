using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaApp.Services
{
    public interface ILocationService
    {
        List<Location> GetAllList();
        Task<List<Location>> Get(LocationSearch locationSearch);
            
    }
}

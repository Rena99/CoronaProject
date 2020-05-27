using CoronaApp.Dal;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaApp.Services
{
    public interface ILocationRepository
    {
        ICollection<Location> Get(LocationSearch locationSearch);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoronaApp.Services
{
    public interface ILocationRepository
    {
        Task<List<Location>> SearchByCity(LocationSearch locationSearch);
        ICollection<Location> GetAllList(List<Location> locations);
        List<Location> SearchByDate(LocationSearch locationSearch);
        Task<List<Location>> SearchBy(LocationSearch locationSearch);
        Task<ICollection<Location>> SearchByAllParams(LocationSearch locationSearch);

    }
}

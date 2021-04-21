﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.IServices
{
    public interface ITourRepository: IGenericRepository<Tour>
    {
        IEnumerable<Tour> GetByCategory(string category);
        int GetRemainingQuantity(int tourId);
        IEnumerable<Tour> Search(string tour);

    }
}

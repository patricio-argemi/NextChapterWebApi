using NextChapterWebApi.Models;
using System;

namespace NextChapterWebApi.Interfaces
{
    interface ISpecificPriceRepository
    {
        void Add(SpecificPrice newSpecificPrice);
        void Update(SpecificPrice updatedSpecificPrice);
        void Delete(SpecificPrice specificPriceToDelete);
        Tuple<decimal?, bool> GetByCustomerAndProductCode(SpecificPrice specificPrice);
    }
}

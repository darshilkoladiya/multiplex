using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IShowPriceService
    {
        IEnumerable<ShowPrice> GetAllShowPrice();
        ShowPrice GetShowPriceById(int id);
        void InsertShowPrice(ShowPrice showPrice);
        void UpdateShowPrice(ShowPrice showPrice);
        void DeleteShowPrice(int id);

        IEnumerable<ShowPrice> GetShowPriceByShowId(int id);
    }
}

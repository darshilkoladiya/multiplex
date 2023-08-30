using Data.Context;
using Data.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ShowPriceService : IShowPriceService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<ShowPrice> showPriceRepository;
        public ShowPriceService(IMultiRepository<ShowPrice> showPriceRepository)
        {
            this.showPriceRepository = showPriceRepository;
        }

        public void DeleteShowPrice(int id)
        {
            ShowPrice showPrice = GetShowPriceById(id);
            showPrice.IsDeleted = true;
            UpdateShowPrice(showPrice);
            //showPriceRepository.Remove(showPrice);
            //showPriceRepository.SaveChanges();
        }

        public IEnumerable<ShowPrice> GetAllShowPrice()
        {
            return showPriceRepository.GetAll();
        }

        public ShowPrice GetShowPriceById(int id)
        {
            return showPriceRepository.Get(id);
        }

        public void InsertShowPrice(ShowPrice showPrice)
        {
            showPriceRepository.Insert(showPrice);
        }

        public void UpdateShowPrice(ShowPrice showPrice)
        {
            showPriceRepository.Update(showPrice);
        }
            
        public IEnumerable<ShowPrice> GetShowPriceByShowId(int id)
        {
            var price = db.showPrices.Where(x => x.ShowId == id).ToList();
            return price;
        }
    }
}

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
   public class SeatTypeService : ISeatTypeService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<SeatType> seatTypeRepository;
        public SeatTypeService(IMultiRepository<SeatType> seatTypeRepository)
        {
            this.seatTypeRepository = seatTypeRepository;
        }
        public void DeleteSeatType(int id)
        {
            SeatType seatType = GetSeatTypeById(id);
            seatType.IsDeleted = true;
            UpdateSeatType(seatType);
            //seatTypeRepository.Remove(seatType);
            //seatTypeRepository.SaveChanges();
        }

        public IEnumerable<SeatType> GetSeatType()
        {
            return seatTypeRepository.GetAll();
        }

        public SeatType GetSeatTypeById(int id)
        {
            return seatTypeRepository.Get(id);
        }

        public void InsertSeatType(SeatType seatType)
        {
            seatTypeRepository.Insert(seatType);
        }

        public void UpdateSeatType(SeatType seatType)
        {
            seatTypeRepository.Update(seatType);
        }

        public IEnumerable<SeatType> GetSeatByScreenId(int id)
        {
            var type = db.seatTypes.Where(x => x.ScreenId == id).ToList();
            return type;
        }

    }
}

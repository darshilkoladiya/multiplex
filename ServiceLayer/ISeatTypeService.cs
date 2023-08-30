using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface ISeatTypeService
    {
        IEnumerable<SeatType> GetSeatType();
        SeatType GetSeatTypeById(int id);
        void InsertSeatType(SeatType seatType);
        void UpdateSeatType(SeatType seatType);
        void DeleteSeatType(int id);

        IEnumerable<SeatType> GetSeatByScreenId(int id);
    }
}

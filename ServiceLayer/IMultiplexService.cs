using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface IMultiplexService
    {
        IEnumerable<Multiplexes> GetMultiplex();
        Multiplexes GetMultiplexById(int id);
        void InsertMultiplex(Multiplexes multiplex);
        void UpdateMultiplex(Multiplexes multiplex);
        void DeleteMultiplex(int id);

        Multiplexes CheckData(Multiplexes multiplex);
    }
}

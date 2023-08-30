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
    public class MultiplexService : IMultiplexService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<Multiplexes> multiplexRepository;
        public MultiplexService(IMultiRepository<Multiplexes> multiplexRepository)
        {
            this.multiplexRepository = multiplexRepository;
        }

        public IEnumerable<Multiplexes> GetMultiplex()
        {
            return multiplexRepository.GetAll();
        }

        public Multiplexes GetMultiplexById(int id)
        {
            return multiplexRepository.Get(id);
        }

        public void InsertMultiplex(Multiplexes multiplex)
        {
            multiplexRepository.Insert(multiplex);
        }
        public void UpdateMultiplex(Multiplexes multiplex)
        {
            multiplexRepository.Update(multiplex);
        }

        public void DeleteMultiplex(int id)
        {

            Multiplexes multiplex = GetMultiplexById(id);
            multiplex.IsDeleted = true;
            UpdateMultiplex(multiplex);
            //multiplexRepository.Remove(multiplex);
            //multiplexRepository.SaveChanges();
        }

        public Multiplexes CheckData(Multiplexes multiplex)
        {
            var multi = db.multiplexes.Where(a => a.Name.Equals(multiplex.Name) && a.Address.Equals(multiplex.Address) && 
                                            a.CityId.Equals(multiplex.CityId) && a.StateId.Equals(multiplex.StateId)).FirstOrDefault();
            return multi;
        }
    }
}

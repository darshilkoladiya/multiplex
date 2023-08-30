using Data.Context;
using Data.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dropdown
{
    public class StateService : IStateService
    {

        private IMultiRepository<States> stateRepository;

        public StateService(IMultiRepository<States> stateRepository)
        {
            this.stateRepository = stateRepository;
        }

        public IEnumerable<States> GetAllState()
        {
            return stateRepository.GetAll();
        }

        public States GetStateById(int id)
        {
            return stateRepository.Get(id);
        }

        public void InsertState(States state)
        {
            stateRepository.Insert(state);
        }
        public void UpdateState(States state)
        {
            stateRepository.Update(state);
        }

        public void DeleteState(int id)
        {

            States states = GetStateById(id);
            states.IsDeleted = true;
            UpdateState(states);
            //multiplexRepository.Remove(state);
            //multiplexRepository.SaveChanges();
        }
    }
}

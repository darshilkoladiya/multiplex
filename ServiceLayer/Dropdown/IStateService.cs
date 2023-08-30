using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;

namespace ServiceLayer.Dropdown
{
  public interface IStateService
    {
        IEnumerable<States> GetAllState();
        States GetStateById(int id);
        void InsertState(States state);
        void UpdateState(States state);
        void DeleteState(int id);
    }
}

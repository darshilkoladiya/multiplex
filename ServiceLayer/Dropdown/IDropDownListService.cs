using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dropdown
{
    public interface IDropDownListService
    {
        ShowModel PrepareDropdownForMovie(int id);
    }
}

using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface ILanguageService
    {
        IEnumerable<Language> GetAllLanguage();
        Language GetLanguageById(int id);
        void InsertLanguage(Language language);
        void UpdateLanguage(Language language);
        void DeleteLanguage(int id);

    }
}

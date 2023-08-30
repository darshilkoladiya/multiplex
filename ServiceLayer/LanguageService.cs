using Data.Context;
using Data.Entity;
using Repository;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class LanguageService : ILanguageService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<Language> languageRepository;
        public LanguageService(IMultiRepository<Language> languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        public void DeleteLanguage(int id)
        {
            Language language = GetLanguageById(id);
            language.IsDeleted = true;
            UpdateLanguage(language);
        }

        public IEnumerable<Language> GetAllLanguage()
        {
            return languageRepository.GetAll();
        }

        public Language GetLanguageById(int id)
        {
            return languageRepository.Get(id);
        }


        public void InsertLanguage(Language language)
        {
            languageRepository.Insert(language);
        }

        public void UpdateLanguage(Language language)
        {
            languageRepository.Update(language);
        }


        

    }
}

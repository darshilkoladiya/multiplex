using Multiplex.Factory;
using Multiplex.Factory.ModelPrepare;
using Repository;
using ServiceLayer;
using ServiceLayer.Dropdown;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Multiplex
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ICustomerModelFactory, CustomerModelFactory>();
            container.RegisterType<ICustomerService, CustomerService>();

            container.RegisterType<IMultiplexFactory, MultiplexFactory>();
            container.RegisterType<IMultiplexModelFactory, MultiplexModelFactory>();
            container.RegisterType<IMultiplexService, MultiplexService>();
            container.RegisterType<IMovie_Multiplex_Serviece, Movie_Multiplex_Serviece>();

            container.RegisterType<ICityService, CityService>();
            container.RegisterType<IStateCityModelFactory, StateCityModelFactory>();
            container.RegisterType<IStateService, StateService>();

            container.RegisterType<IScreenModelFactory, ScreenModelFactory>();
            container.RegisterType<IScreenService, ScreenService>();
            container.RegisterType<ISeatTypeService, SeatTypeService>();

            container.RegisterType<IMovieModelFactory, MovieModelFactory>();
            container.RegisterType<IMovieService, MovieService>();
            container.RegisterType<IGenresService, GenresService>();
            container.RegisterType<ILanguageService, LanguageService>();
            container.RegisterType<IDropDownListService, DropDownListService>();
            container.RegisterType<IFormateService, FormateService>();
            container.RegisterType<IMovieLanguageService, MovieLanguageService>();

            container.RegisterType<IShowService, ShowService>();
            container.RegisterType<IShowPriceService, ShowPriceService>();

            container.RegisterType(typeof(IMultiRepository<>), typeof(MultiRepository<>));
            container.RegisterType(typeof(IMovieRepository<>), typeof(MovieRepository<>));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
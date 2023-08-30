using Data.Entity;
using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Multiplex.Factory.ModelPrepare
{
    public interface IMovieModelFactory
    {

        #region Main Tables 
        #region Movie
        PagedList<MovieModel> PrepareMovieForIndex(string searchString, string currentFilter, int? page);
        Movie PrepareMovieForCreateMovie(MovieModel model);
        MovieModel prepareMovieModelForEditMovie(int id);
        Movie PrepareMovieForEditPostMovie(MovieModel model);
        #endregion

        #region Language
        PagedList<Language> PrepareLanguageForList(string searchString, string currentFilter, int? page);
        Language PrepareLanguageForEdit(int id);
        Language PrepareLanguageForEditPost(Language model);
        #endregion

        #endregion

        #region Genres
        MovieListModel PrepareGenresMovieModelForList(int movieId);
        MovieListModel PrepareGenresMovieModelForCreateGenres(int id);
        #endregion

        #region Formate
        MovieListModel PrepareFormateMovieModelForList(int movieId);
        MovieListModel PrepareFormateMovieModelForCreateFormate(int id);
        #endregion

        MovieListModel PrepareLanguageMovieModelForList(int movieId);
        MovieListModel PrepareLanguageMovieModelForCreateLanguage(int id);


    }
}

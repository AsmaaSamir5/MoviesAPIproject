﻿namespace MoviesAPI.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(byte categoryId = 0);

        Task<Movie> GetById(int id);

        Task<Movie> Add(Movie movie);

        Movie Update(Movie movie);

        Movie Delete(Movie movie);

    }
}

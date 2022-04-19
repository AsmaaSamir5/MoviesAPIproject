using AutoMapper;

namespace MoviesAPI.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<MoviesDto, Movie>()
                .ForMember(src => src.Poster, option => option.Ignore());

        }
    }
}

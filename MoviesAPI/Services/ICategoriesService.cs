namespace MoviesAPI.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetById(byte id);

        Task<Category> Add(Category category);

        Category Update(Category category);

        Category Delete(Category category);

        Task<bool> IsValidCategory(byte id);

    }
}

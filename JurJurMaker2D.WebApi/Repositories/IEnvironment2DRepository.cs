namespace JurJurMaker2D.WebApi.Repositories
{
    public interface IEnvironment2DRepository
    {
        public void CreateAsync(Environment2D environment2D);
        public void UpdateAsync(Environment2D environment);
        public void DeleteAsync(Guid id);
        public Task<Environment2D?> ReadAsync(Guid id);
        public Task<IEnumerable<Environment2D?>> ReadAllAsync();
    }
}

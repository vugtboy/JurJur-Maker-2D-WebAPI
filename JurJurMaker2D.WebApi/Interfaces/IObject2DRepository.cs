﻿namespace JurJurMaker2D.WebApi.Interfaces
{
    public interface IObject2DRepository
    {
        public Task CreateAsync(Object2D Object2D);
        public void UpdateAsync(Object2D Object);
        public void DeleteAsync(Guid id);
        public Task<Object2D?> ReadAsync(Guid id);
        public Task<IEnumerable<Object2D?>> ReadAllAsync(Guid id);
    }
}

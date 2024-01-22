﻿namespace CursoBackend.Repository
{
    public interface IRepository<IEntity>
    {
        Task<IEnumerable<IEntity>> Get();

        Task<IEntity> GetById(int id);

        Task Add(IEntity entity);

        void Update(IEntity entity);

        void Delete(IEntity entity);

        Task Save();
    }
}

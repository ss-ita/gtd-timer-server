//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GtdTimerDAL.Repositories
{
    /// <summary>
    /// interface for Repository class
    /// </summary>
    /// <typeparam name="TEntity"> generic table </typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Method for getting all entities
        /// </summary>
        /// <returns> list of entities </returns>
        IEnumerable<TEntity> GetAllEntities();

        /// <summary>
        /// Method for getting all entities with some filter
        /// </summary>
        /// <param name="filter"> filter by which entities are checked</param>
        /// <returns> list of entities </returns>
        IEnumerable<TEntity> GetAllEntitiesByFilter(Func<TEntity, bool> filter);

        /// <summary>
        /// Method for getting entity by id
        /// </summary>
        /// <param name="id"> id of entity </param>
        /// <returns> returns entity </returns>
        TEntity GetByID(object id);

        /// <summary>
        /// Method for creating entity
        /// </summary>
        /// <param name="entity"> entity model </param>
        void Create(TEntity entity);

        /// <summary>
        /// Method to delete by id
        /// </summary>
        /// <param name="id"> id of entity </param>
        void Delete(object id);

        /// <summary>
        /// Method to delete by its fields
        /// </summary>
        /// <param name="entityToDelete"> entity to delete </param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Method to update entity
        /// </summary>
        /// <param name="entityToUpdate"> entity to update </param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Method to save changes
        /// </summary>
        void Save();
    }
}
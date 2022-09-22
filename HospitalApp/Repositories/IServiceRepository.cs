using HospitalApp.Models;
using System;
namespace HospitalApp.Repositories
{
    /// <summary>
    /// An Interface that will be implemented by Repository classes
    /// These classes will ave Business Logic in it
    /// TEntity: The Entoity Clas that will be a Type Parameter that will be used for usiness Logic and TPk is a Patameter that will be used for Search Operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IServiceRepository<TEntity, TPk> where TEntity : class
    {
        ResponseStatus<TEntity> GetRecords();
        ResponseStatus<TEntity> GetRecord(TPk id);
        ResponseStatus<TEntity> CreateRecord(TEntity entity);
        ResponseStatus<TEntity> UpdateRecord(TPk id, TEntity entity);
        ResponseStatus<TEntity> DeleteRecord(TPk id);
    }
}


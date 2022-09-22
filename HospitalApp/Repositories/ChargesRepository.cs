
using System;
using Application.Dal.Contract;
using Application.Entities;
using HospitalApp.Models;

namespace HospitalApp.Repositories
{
    /// <summary>
    /// A Charges Repository class that will be having a Business Logic
    /// for Performing Operations for Charges
    /// This will be registered in Dependency Container
    /// This will be Injected with Data Access layer for Charges
    /// </summary>
    public class ChargesRepository : IServiceRepository<Charges, int>
    {
        // Define a Dependency
        IDataAccess<Charges, int> deptDataAccess;

        /// <summary>
        /// Inject the Dependency 
        /// </summary>
        public ChargesRepository(IDataAccess<Charges, int> dataAccess)
        {
            deptDataAccess = dataAccess;
        }

        public ResponseStatus<Charges> CreateRecord(Charges entity)
        {
            ResponseStatus<Charges> response = new ResponseStatus<Charges>();
            //try
            {
                response.Record = deptDataAccess.Create(entity);
                response.Message = "Record is created successfully";
                response.StatusCode = 201;
            }
            /*catch (Exception ex)
            {
                throw ex;
            }*/
            return response;
        }

        public ResponseStatus<Charges> DeleteRecord(int id)
        {
            ResponseStatus<Charges> response = new ResponseStatus<Charges>();
            try
            {
                response.Record = deptDataAccess.Delete(id);
                response.Message = "Record is delete successfully";
                response.StatusCode = 203;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Charges> GetRecord(int id)
        {
            ResponseStatus<Charges> response = new ResponseStatus<Charges>();
            try
            {
                response.Record = deptDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Charges> GetRecords()
        {
            ResponseStatus<Charges> response = new ResponseStatus<Charges>();
            try
            {
                response.Records = deptDataAccess.Get();
                response.Message = "Records are read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Charges> UpdateRecord(int id, Charges entity)
        {
            ResponseStatus<Charges> response = new ResponseStatus<Charges>();
            try
            {
                response.Record = deptDataAccess.Update(id, entity);
                response.Message = "Record is updated successfully";
                response.StatusCode = 204;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

       
    }
}


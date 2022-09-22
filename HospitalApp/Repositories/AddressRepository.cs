using Application.Dal.Contract;
using Application.Entities;
using HospitalApp.Repositories;
using HospitalApp.Models;
using System;

namespace HospitalApp.Repositories
{
    /// <summary>
    /// A Address Repository class that will be having a Business Logic
    /// for Performing Operations for Address
    /// This will be registered in Dependency Container
    /// This will be Injected with Data Access layer for Address
    /// </summary>
    public class AddressRepository : IServiceRepository<Address, int>
    {
        // Define a Dependency
        IDataAccess<Address, int> deptDataAccess;

        /// <summary>
        /// Inject the Dependency 
        /// </summary>
        public AddressRepository(IDataAccess<Address, int> dataAccess)
        {
            deptDataAccess = dataAccess;
        }

        public ResponseStatus<Address> CreateRecord(Address entity)
        {
            ResponseStatus<Address> response = new ResponseStatus<Address>();
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

        public ResponseStatus<Address> DeleteRecord(int id)
        {
            ResponseStatus<Address> response = new ResponseStatus<Address>();
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

        public ResponseStatus<Address> GetRecord(int id)
        {
            ResponseStatus<Address> response = new ResponseStatus<Address>();
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
      

        public ResponseStatus<Address> GetRecords()
        {
            ResponseStatus<Address> response = new ResponseStatus<Address>();
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

        public ResponseStatus<Address> UpdateRecord(int id, Address entity)
        {
            ResponseStatus<Address> response = new ResponseStatus<Address>();
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


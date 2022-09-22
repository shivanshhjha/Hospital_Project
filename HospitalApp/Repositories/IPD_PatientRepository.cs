using Application.Entities;
using System;
using Application.Dal.Contract;
using HospitalApp.Models;

namespace HospitalApp.Repositories
{
    /// <summary>
    /// A IPD_Patient Repository class that will be having a Business Logic
    /// for Performing Operations for IPD_Patient
    /// This will be registered in Dependency Container
    /// This will be Injected with Data Access layer for IPD_Patient
    /// </summary>
    public class IPD_PatientRepository : IServiceRepository<IPD_Patient, int>
    {
        // Define a Dependency
        IDataAccess<IPD_Patient, int> deptDataAccess;

        /// <summary>
        /// Inject the Dependency 
        /// </summary>
        public IPD_PatientRepository(IDataAccess<IPD_Patient, int> dataAccess)
        {
            deptDataAccess = dataAccess;
        }

        public ResponseStatus<IPD_Patient> CreateRecord(IPD_Patient entity)
        {
            ResponseStatus<IPD_Patient> response = new ResponseStatus<IPD_Patient>();
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

        public ResponseStatus<IPD_Patient> DeleteRecord(int id)
        {
            ResponseStatus<IPD_Patient> response = new ResponseStatus<IPD_Patient>();
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

        public ResponseStatus<IPD_Patient> GetRecord(int id)
        {
            ResponseStatus<IPD_Patient> response = new ResponseStatus<IPD_Patient>();
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

        public ResponseStatus<IPD_Patient> GetRecords()
        {
            ResponseStatus<IPD_Patient> response = new ResponseStatus<IPD_Patient>();
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

        public ResponseStatus<IPD_Patient> UpdateRecord(int id, IPD_Patient entity)
        {
            ResponseStatus<IPD_Patient> response = new ResponseStatus<IPD_Patient>();
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


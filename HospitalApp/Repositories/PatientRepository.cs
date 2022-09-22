using Application.Dal.Contract;
using Application.Entities;
using HospitalApp.Repositories;
using HospitalApp.Models;
using System;

namespace HospitalApp.Repositories
{
    /// <summary>
    /// A Patient Repository class that will be having a Business Logic
    /// for Performing Operations for Patient
    /// This will be registered in Dependency Container
    /// This will be Injected with Data Access layer for Patient
    /// </summary>
    public class PatientRepository : IServiceRepository<Patient, int>
    {
        // Define a Dependency
        IDataAccess<Patient, int> deptDataAccess;

        /// <summary>
        /// Inject the Dependency 
        /// </summary>
        public PatientRepository(IDataAccess<Patient, int> dataAccess)
        {
            deptDataAccess = dataAccess;
        }

        public ResponseStatus<Patient> CreateRecord(Patient entity)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
            try
            {
                response.Record = deptDataAccess.Create(entity);
                response.Message = "Record is created successfully";
                response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Patient> DeleteRecord(int id)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
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

        public ResponseStatus<Patient> GetRecord(int id)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
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
        public ResponseStatus<Patient> GetRecord(string email)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
            try
            {
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Patient> GetRecords()
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
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

        public ResponseStatus<Patient> UpdateRecord(int id, Patient entity)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
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


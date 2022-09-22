
using System;
using Application.Dal.Contract;
using Application.Entities;
using HospitalApp.Models;

namespace HospitalApp.Repositories
{
    /// <summary>
    /// A Doctor Repository class that will be having a Business Logic
    /// for Performing Operations for Doctor
    /// This will be registered in Dependency Container
    /// This will be Injected with Data Access layer for Doctor
    /// </summary>
    public class DoctorRepository : IServiceRepository<Doctor, int>
    {
        // Define a Dependency
        IDataAccess<Doctor, int> deptDataAccess;

        /// <summary>
        /// Inject the Dependency 
        /// </summary>
        public DoctorRepository(IDataAccess<Doctor, int> dataAccess)
        {
            deptDataAccess = dataAccess;
        }

        public ResponseStatus<Doctor> CreateRecord(Doctor entity)
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
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

        public ResponseStatus<Doctor> DeleteRecord(int id)
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
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

        public ResponseStatus<Doctor> GetRecord(int id)
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
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

        public ResponseStatus<Doctor> GetRecords()
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
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

        public ResponseStatus<Doctor> UpdateRecord(int id, Doctor entity)
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
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


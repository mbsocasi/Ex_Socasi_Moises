using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;


namespace BLL
{
    public class HospitalesLogic
    {
        public Hospitales Create(Hospitales hospital)
        {
            if (hospital == null)
                throw new ArgumentNullException("El objeto hospital no puede ser nulo.");

            using (var repository = RepositoryFactory.CreateRepository())
            {
                try
                {
                    // Validación para evitar duplicados
                    var existingHospital = repository.Retrieve<Hospitales>(h => h.Nombre == hospital.Nombre);
                    if (existingHospital != null)
                        throw new Exception("El hospital ya existe.");

                    return repository.Create(hospital);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al crear el hospital: {ex.Message}", ex);
                }
            }
        }
        public Hospitales RetrieveById(int id)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                try
                {
                    return repository.Retrieve<Hospitales>(h => h.HospitalID == id);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al obtener el hospital con ID {id}: {ex.Message}", ex);
                }
            }
        }

        public List<Hospitales> RetrieveAll()
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                try
                {
                    return repository.Filter<Hospitales>(h => h.HospitalID > 0);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al obtener todos los hospitales: {ex.Message}", ex);
                }
            }
        }

        public bool Update(Hospitales hospital)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                try
                {
                    // Buscar el hospital existente en la base de datos
                    var existingHospital = repository.Retrieve<Hospitales>(h => h.HospitalID == hospital.HospitalID);

                    if (existingHospital == null)
                    {
                        throw new Exception("El hospital no existe.");
                    }

                    // Actualizar los campos necesarios
                    existingHospital.Nombre = hospital.Nombre;
                    existingHospital.Direccion = hospital.Direccion;
                    existingHospital.Telefono = hospital.Telefono;

                    // Guardar los cambios
                    return repository.Update(existingHospital);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al actualizar el hospital: {ex.Message}");
                }
            }
        }

        public bool Delete(int id)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                try
                {
                    var hospital = repository.Retrieve<Hospitales>(h => h.HospitalID == id);
                    if (hospital == null)
                        throw new Exception("El hospital no existe.");

                    return repository.Delete(hospital);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al eliminar el hospital con ID {id}: {ex.Message}", ex);
                }
            }
        }
    }

}

using System.Web.Http;
using Entities;
using BLL;
using System;

namespace Service.Controllers
{
    public class HospitalesController : ApiController
    {
        [HttpPost]
        [Route("api/Hospitales")]
        public IHttpActionResult CreateHospital(Hospitales hospital)
        {
            if (hospital == null)
                return BadRequest("El hospital no puede ser nulo.");

            var _hospitalesLogic = new HospitalesLogic();

            try
            {
                var createdHospital = _hospitalesLogic.Create(hospital);
                return Created($"api/Hospitales/{createdHospital.HospitalID}", createdHospital);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el hospital: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("api/Hospitales/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var _hospitalesLogic = new HospitalesLogic();
            var result = _hospitalesLogic.Delete(id);

            if (result)
                return Ok("Hospital eliminado correctamente.");
            else
                return NotFound();
        }

        // Obtener todos los hospitales
        [HttpGet]
        [Route("api/Hospitales")]
        public IHttpActionResult GetAll()
        {
            var _hospitalesLogic = new HospitalesLogic();
            try
            {
                var hospitales = _hospitalesLogic.RetrieveAll();
                if (hospitales == null || hospitales.Count == 0)
                    return NotFound();

                return Ok(hospitales);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Obtener hospital por ID
        [HttpGet]
        [Route("api/Hospitales/{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var _hospitalesLogic = new HospitalesLogic();
            try
            {
                var hospital = _hospitalesLogic.RetrieveById(id);
                if (hospital == null)
                    return NotFound();

                // Crear un objeto anónimo para devolver solo las propiedades necesarias
                var response = new
                {
                    hospital.HospitalID,
                    hospital.Nombre,
                    hospital.Direccion,
                    hospital.Telefono
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Actualizar un hospital
        [HttpPut]
        [Route("api/Hospitales/{id:int}")]
        public IHttpActionResult UpdateHospital(int id, Hospitales hospital)
        {
            if (hospital == null)
                return BadRequest("El hospital no puede ser nulo.");

            if (id != hospital.HospitalID)
                return BadRequest("El ID del hospital no coincide con el ID de la URL.");

            var _hospitalesLogic = new HospitalesLogic();

            try
            {
                var updated = _hospitalesLogic.Update(hospital);
                if (updated)
                    return Ok("Hospital actualizado correctamente.");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el hospital: {ex.Message}");
            }
        }
        public class HomeController : ApiController
        {
            [HttpGet]
            [Route("")]
            public IHttpActionResult GetDefault()
            {
                return Ok("Bienvenido a la API de Hospitales.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WepAPI.Controllers
{
    [RoutePrefix("api/Unidad")]
    public class UnidadController : ApiController
    {
        [Route("Add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ML.Unidad unidad)
        {
            var result = BL.Unidad.Add(unidad);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("Update/{idUnidad?}")]
        [HttpPut]
        public IHttpActionResult Update(int idUnidad, [FromBody] ML.Unidad unidad)
        {
            unidad.IdUnidad = idUnidad;
            var result = BL.Unidad.Update(unidad);
            if (result)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("Delete/{idUnidad?}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idUnidad)
        {
            var result = BL.Unidad.Delete(idUnidad);
            if (result)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var list = BL.Unidad.GetAll();
            if (list != null)
            {
                return Content(HttpStatusCode.OK, list);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, list);
            }
        }
        [Route("GetById/{idUnidad?}")]
        [HttpGet]
        public IHttpActionResult GetById(int idUnidad)
        {
            var list = BL.Unidad.GetById(idUnidad);
            if (list != null)
            {
                return Content(HttpStatusCode.OK, list);

            }
            else
            {
                return Content(HttpStatusCode.BadRequest, list);
            }
        }
    }
}

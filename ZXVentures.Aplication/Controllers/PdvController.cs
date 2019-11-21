using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ZXVentures.Domain.Entities;
using ZXVentures.Domain.Interfaces;
using ZXVentures.Domain.Model;

namespace ZXVentures.Aplication.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]/[action]")]
    [ApiController]
    public class PdvController : ControllerBase
    {
        private readonly IServicePdv _servicePdv;

        public PdvController(IServicePdv pdvService)
        {
            _servicePdv = pdvService;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var pdv = _servicePdv.GetById(id).Result;

            if (pdv == null) return NotFound();

            return Ok(pdv);
        }

        [HttpPost(Name = "GetByLocation")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult GetByLocation([FromBody] FilterPdvLocation filter)
        {
            if (ModelState.IsValid)
                try
                {
                    var pdv = _servicePdv.GetByLocation(filter.Longitude, filter.Latitude).Result;
                    if (pdv == null) return NotFound();
                    return Ok(pdv);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            return BadRequest();
        }

        [HttpPost(Name = "InsertPdv")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult InsertPdv([FromBody] Pdv pdv)
        {
            if (ModelState.IsValid)
                try
                {
                    if (_servicePdv.Post(pdv).Exception == null)
                        return Ok(pdv.partnerId);
                    throw _servicePdv.Post(pdv).Exception;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            return BadRequest();
        }
    }
}
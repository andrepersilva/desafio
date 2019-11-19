using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZXVentures.Domain.Entities;
using ZXVentures.Domain.Interfaces;
using ZXVentures.Domain.Model;
using ZXVentures.Service;

namespace ZXVentures.Aplication.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]/[action]")]
    [ApiController]
    public class PdvController : ControllerBase
    {
        private readonly IServicePdv _pdvService;

        public PdvController(IServicePdv pdvService)
        {
            _pdvService = pdvService;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        
        public IActionResult GetById(int id)
        {
 
            var pdv = _pdvService.GetById(id).Result;

            if (pdv == null) return NotFound();

            return Ok( pdv );
 
 

        }

        [HttpPost(Name = "GetByLocation")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult GetByLocation([FromBody] FilterPdvLocation filter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pdv = _pdvService.GetByLocation(longitude: filter.longitude, latitude: filter.latitude).Result;
                    if (pdv == null) return NotFound();
                    return Ok(pdv);
                }
                catch (Exception ex )
                {
                    return BadRequest(ex.Message);
                }

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
                    if (_pdvService.Post(pdv).Exception == null)
                        return Ok(pdv.partnerId);
                    else
                        throw _pdvService.Post(pdv).Exception;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            return BadRequest();
        }
    }
}
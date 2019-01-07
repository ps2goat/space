using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceApi.Dto;
using SpaceApi.Entities;
using SpaceApi.Models;
using SpaceApi.Services;

namespace SpaceApi.Controllers
{
    [Route("api/launchpad")]
    [ApiController]
    public class LaunchPadController : ControllerBase
    {
        private readonly ILaunchPadService _launchPadService;

        public LaunchPadController(ILaunchPadService launchPadService)
        {
            _launchPadService = launchPadService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LaunchPadDto>>> Get([FromQuery] string fullname = null, [FromQuery] string status = null)
        {
            LaunchPadFilterOptions options = new LaunchPadFilterOptions
            {
                FullName = fullname,
                Status = status
            };

            List<LaunchPad> fullInfo = await _launchPadService.GetLaunchPads(options);

            return fullInfo.Select(l => new LaunchPadDto
            {
                Id = l.Id,
                Name = l.FullName,
                Status = l.Status
            }).ToList();
        }
    }
}

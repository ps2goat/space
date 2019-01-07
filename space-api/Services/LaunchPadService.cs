using SpaceApi.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using SpaceApi.Repositories;
using SpaceApi.Models;

namespace SpaceApi.Services
{
    public class LaunchPadService : ILaunchPadService
    {

        private readonly ILogger _logger;
        private readonly ILaunchPadRepository _launchPadRepo;

        public LaunchPadService(ILogger<LaunchPadService> logger, ILaunchPadRepository launchPadRepo)
        {
            _logger = logger;
            _launchPadRepo = launchPadRepo;
        }

        public async Task<List<LaunchPad>> GetLaunchPads(LaunchPadFilterOptions options)
        {
            return await _launchPadRepo.GetFilteredLaunchPads(options);
        }
    }
}
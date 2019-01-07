using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpaceApi.Entities;
using SpaceApi.Models;
using SpaceApi.Services;

namespace SpaceApi.Repositories
{
    public class LaunchPadRepository : ILaunchPadRepository
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IReusableHttpClient _client;
        private readonly SemaphoreSlim _lock;
        private readonly string _launchPadDataUrl;

        private List<LaunchPad> _launchPadCache;

        public LaunchPadRepository(ILogger<LaunchPadRepository> logger, IConfiguration config, IReusableHttpClient client)
        {
            _logger = logger;
            _config = config;
            _client = client;
            _lock = new SemaphoreSlim(1);
            _launchPadDataUrl = config.GetValue<string>("LaunchPadDataUrl");

            if (string.IsNullOrWhiteSpace(_launchPadDataUrl))
            {
                throw new Exception("'LaunchPadDataUrl' config value is missing.");
            }
        }

        public virtual async Task<List<LaunchPad>> GetAllLaunchPads()
        {
            if (_launchPadCache?.Any() == true)
            {
                _logger.LogDebug("Returning cached launchpads");

                return _launchPadCache;
            }

            await _lock.WaitAsync();
            
            if (_launchPadCache?.Any() == true)
            {
                _logger.LogDebug("Returning cached launchpads, after waiting for the lock.");

                return _launchPadCache;
            }

            try
            {
                _logger.LogDebug("Retrieving launchpads from the SpaceX API.");
                _launchPadCache = await _client.GetAsync<List<LaunchPad>>(_launchPadDataUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving launchpads.");
            }
            finally
            {
                _logger.LogDebug("Releasing the lock from LaunchPadRepository.");

                _lock.Release();
            }

            return _launchPadCache;            
        }
        
        public async Task<List<LaunchPad>> GetFilteredLaunchPads(LaunchPadFilterOptions options)
        {
            List<LaunchPad> toReturn = await GetAllLaunchPads();

            if (options?.HasFilters() != true)
            {
                _logger.LogDebug("No filtering necessary; no filters were supplied.");

                return toReturn;
            }

            _logger.LogDebug($"Applying filters. FullName = {options.FullName}, status = {options.Status}");

            toReturn = toReturn.Where(l => 
            {
                return (options.FullName == null || l.FullName.Contains(options.FullName, StringComparison.InvariantCultureIgnoreCase)) &&
                    (options.Status == null || l.Status.Contains(options.Status, StringComparison.InvariantCultureIgnoreCase));
            }).ToList();

            return toReturn;
        }
    }
}
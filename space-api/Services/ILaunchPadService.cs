using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceApi.Entities;
using SpaceApi.Models;

namespace SpaceApi.Services
{
    public interface ILaunchPadService
    {
        Task<List<LaunchPad>> GetLaunchPads(LaunchPadFilterOptions options);
    }
}
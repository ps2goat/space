using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceApi.Entities;
using SpaceApi.Models;

namespace SpaceApi.Repositories
{
    public interface ILaunchPadRepository
    {
        Task<List<LaunchPad>> GetAllLaunchPads();

        Task<List<LaunchPad>> GetFilteredLaunchPads(LaunchPadFilterOptions options);
    }
}
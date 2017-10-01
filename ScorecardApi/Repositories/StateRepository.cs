using Microsoft.Extensions.Configuration;
using ScorecardApi.Models;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;

namespace ScorecardApi.Repositories
{
    public interface IStateRepository
    {
        IEnumerable<State> GetStates();
    }

    public class StateRepository : IStateRepository
    {
        private IConfiguration _configuration;

        public StateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<State> GetStates()
        {
            //TODO: Ideally a repository wouldn't need to know that Service Fabric is being used, however Servivce Fabric stores
            //the configuration package on the activation context.  At a minimum, figure out how to setup dependency injection of
            //the service fabric activation context.
            var configurationPackage = FabricRuntime.GetActivationContext().GetConfigurationPackageObject("Config");
            var states = configurationPackage.Settings.Sections["UnitedStates"];

            return states.Parameters.Select(x => new State { Name = x.Name, Abbreviation = x.Value });
        }
    }
}
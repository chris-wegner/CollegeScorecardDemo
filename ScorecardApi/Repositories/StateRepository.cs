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
        public IEnumerable<State> GetStates()
        {
            //TODO: Figure out how to setup dependency injection of the service fabric activation context.
            var configurationPackage = FabricRuntime.GetActivationContext().GetConfigurationPackageObject("Config");
            var states = configurationPackage.Settings.Sections["UnitedStates"];

            return states.Parameters.Select(x => new State { Name = x.Name, Abbreviation = x.Value });
        }
    }
}
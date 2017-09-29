using Microsoft.AspNetCore.Mvc;
using ScorecardApi.Repositories;

namespace ScorecardApi.Controllers
{
    [Route("api/[controller]")]
    public class StatesController : Controller
    {
        private IStateRepository _stateRepository;

        public StatesController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        // GET: api/states
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_stateRepository.GetStates());
        }
    }
}

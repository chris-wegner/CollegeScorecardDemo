using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ScorecardApi.Repositories;
using System.Threading.Tasks;
using ScorecardApi.Models;

namespace ScorecardApi.Controllers
{
    [Route("api/[controller]")]
    public class SchoolsController : Controller
    {
        private ISchoolRepository _schoolRepository;

        public SchoolsController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        // GET: api/schools
        [HttpGet]
        public async Task<SchoolQueryResult> Get(string name, string state, int? page)
        {
            return await _schoolRepository.GetSchools(name, state, page);
        }
    }
}

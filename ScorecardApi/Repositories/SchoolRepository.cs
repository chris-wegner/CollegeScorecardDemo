using AutoMapper;
using Newtonsoft.Json;
using ScorecardApi.Models;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScorecardApi.Repositories
{
    public interface ISchoolRepository
    {
        Task<SchoolQueryResult> GetSchools(string name, string state, int? page);
    }

    public class SchoolRepository : ISchoolRepository
    {
        public async Task<SchoolQueryResult> GetSchools(string name, string state, int? page)
        {
            var url = GetSchoolsUrl(name, state, page);

            string resultJson;
            using (var client = new HttpClient())
            {
                resultJson = await client.GetStringAsync(url);
            }

            var result = SchoolQueryResult.FromJson(resultJson);

            //Avoid serializing the source API's json property names, so re-map the query results to a new object. 
            Mapper.Initialize(cfg => cfg.CreateMap<QuerySchool, ResultSchool>());
            result.ResultSchools = result.QuerySchools.Select(x => Mapper.Map<ResultSchool>(x)).ToArray();
            result.QuerySchools = null;

            return result;
        }

        internal string GetSchoolsUrl(string name, string state, int? page)
        {
            //TODO: Ideally a repository wouldn't need to know that Service Fabric is being used, however Servivce Fabric stores
            //the configuration package on the activation context.  At a minimum, figure out how to setup dependency injection of
            //the service fabric activation context.
            var configurationPackage = FabricRuntime.GetActivationContext().GetConfigurationPackageObject("Config");
            var apiSection = configurationPackage.Settings.Sections["ScorecardApi"];
            var baseUrl = apiSection.Parameters.Single(x => x.Name == "BaseUrl").Value;
            var baseQueryString = apiSection.Parameters.Single(x => x.Name == "BaseQueryString").Value;
            var queryFields = apiSection.Parameters.Single(x => x.Name == "QueryFields").Value;
            var querySort = apiSection.Parameters.Single(x => x.Name == "QuerySort").Value;
            var pageSize = apiSection.Parameters.Single(x => x.Name == "PageSize").Value;
            var apiKey = apiSection.Parameters.Single(x => x.Name == "ApiKey").Value;

            var nameQueryString = !string.IsNullOrEmpty(name) ? $"&school.name={name}" : string.Empty;
            var stateQueryString = !string.IsNullOrEmpty(state) ? $"&school.state={state}" : string.Empty;
            var pageSizeQueryString = page != null && page >= 0 ? $"&_per_page={pageSize}" : string.Empty;
            var pageQueryString = page != null && page >= 0 ? $"&page={page-1}" : string.Empty;
            var apiKeyQueryString = $"&api_key={apiKey}";

            var url = $"{baseUrl}{baseQueryString}{nameQueryString}{stateQueryString}{queryFields}{querySort}{pageSizeQueryString}{pageQueryString}{apiKeyQueryString}";

            return url;
        }
    }
}
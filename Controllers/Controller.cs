namespace MovieSearch.Controller;

using System.Threading.Tasks;
using RestSharp;


public class QueryClient
{
    private RestClient _client;

    public QueryClient(string apiKey)
    {
        var options = new RestClientOptions("https://api.themoviedb.org/3/search");
        _client = new RestClient(options);
        var headers = new Dictionary<string, string>
        {
            { "accept", "application/json" },
            { "Authorization", apiKey }
        };
        _client.AddDefaultHeaders(headers);
    }

    public async Task<ApiResult<ResponseMovieData>> GetMovieTitles(string queryType, string query)
    {
        if (query is null)
        {
            return new ApiResult<ResponseMovieData>() { Data = { } };
        }
        var request = new RestRequest(queryType);
        request.AddParameter("query", query);
        try
        {
            var response = await _client.ExecuteGetAsync<ResponseMovieData>(request);
            if (response.IsSuccessful)
            {
                var data = response.Data;
                if (data?.results != null)
                {
                    return new ApiResult<ResponseMovieData>() { Data = data };
                }
            }
            return new ApiResult<ResponseMovieData>() { ErrorMessage = response.ErrorMessage };
        }
        catch (Exception error)
        {
            return new ApiResult<ResponseMovieData>() { ErrorMessage = error.Message };
        }
    }
    public async Task<ApiResult<ResponseTvData>> GetTvTitles(string queryType, string query)
    {
        if (query is null)
        {
            return new ApiResult<ResponseTvData>() { Data = { } };
        }
        var request = new RestRequest(queryType);
        request.AddParameter("query", query);
        try
        {
            var response = await _client.ExecuteGetAsync<ResponseTvData>(request);
            if (response.IsSuccessful)
            {
                var data = response.Data;
                if (data?.results != null)
                {
                    return new ApiResult<ResponseTvData>() { Data = data };
                }
            }
            return new ApiResult<ResponseTvData>() { ErrorMessage = response.ErrorMessage };
        }
        catch (Exception error)
        {
            return new ApiResult<ResponseTvData>() { ErrorMessage = error.Message };
        }
    }
    public async Task<ApiResult<ResponsePersonData>> GetPersonTitles(string queryType, string query)
    {
        if (query is null)
        {
            return new ApiResult<ResponsePersonData>() { Data = { } };
        }
        var request = new RestRequest(queryType);
        request.AddParameter("query", query);
        try
        {
            var response = await _client.ExecuteGetAsync<ResponsePersonData>(request);
            if (response.IsSuccessful)
            {
                var data = response.Data;
                if (data?.results != null)
                {
                    return new ApiResult<ResponsePersonData>() { Data = data };
                }
            }
            return new ApiResult<ResponsePersonData>() { ErrorMessage = response.ErrorMessage };
        }
        catch (Exception error)
        {
            return new ApiResult<ResponsePersonData>() { ErrorMessage = error.Message };
        }
    }

    private string GetImageLink(string url)
    {
        const string BASE_LINK = "https://image.tmdb.org/t/p/original";
        return BASE_LINK + url;
    }
}
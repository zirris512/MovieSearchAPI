namespace MovieSearch.Controller;

public record BaseItemData
{
    public int id { get; set; }
    public string? poster_path { get; set; }
    public string? overview { get; set; }
    public string? poster_path_full => string.IsNullOrEmpty(poster_path)
        ? null
        : $"https://image.tmdb.org/t/p/w500{poster_path}";
}

public record QueryMovieItem : BaseItemData
{
    public string? title { get; set; }
    public string? release_date { get; set; }
};
public record ResponseMovieData(List<QueryMovieItem> results);
public record QueryTvItem : BaseItemData
{
    public string? name { get; set; }
    public string? first_air_date { get; set; }
};
public record ResponseTvData(List<QueryTvItem> results);
public record QueryPersonItem : BaseItemData
{
    public string? title { get; set; }
    public string? release_date { get; set; }
    public string? media_type { get; set; }
};
public record PersonData(string? name, List<QueryPersonItem> known_for);
public record ResponsePersonData(List<PersonData> results);
public class ApiResult<T>
{
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSuccessful => ErrorMessage == null;
}
namespace ScientificLabManagementApp.Domain;
public class AllResourceParameters
{
    const int MaxPageSize = 20;
    public string? Filter { get; set; } = null;
    public string OrderBy { get; set; } = "CreatedAt";
    public string? SearchQuery { get; set; } = null;

    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}

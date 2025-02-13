namespace ScientificLabManagementApp.Application;
public class CompanyDto : IEntityHaveId
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string Website { get; set; }
    public string city_url { get; set; }
    public string city_name { get; set; }
    public string country_name { get; set; }
    public string country_url { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }

}



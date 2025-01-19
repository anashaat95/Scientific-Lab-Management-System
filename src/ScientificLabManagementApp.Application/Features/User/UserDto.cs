namespace ScientificLabManagementApp.Application;
public class UserDto 
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string Email { get; set; }
    public string email_confirmed { get; set; }
    public string phone_number { get; set; }
    public string two_factor_enabled { get; set; }
    public string? image_url { get; set; }
    public string company_url { get; set; }
    public string company_name { get; set; }

    public string department_url { get; set; }
    public string department_name { get; set; } = null!;

    public string lab_url { get; set; }
    public string lab_name { get; set; } = null!;

    public string? google_scholar_url { get; set; }
    public string? academia_url { get; set; }
    public string? scopus_url { get; set; }
    public string? researcher_gate_url { get; set; }
    public string? expertise_area { get; set; }

    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}

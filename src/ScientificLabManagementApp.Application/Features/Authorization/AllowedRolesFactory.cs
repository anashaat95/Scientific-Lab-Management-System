namespace ScientificLabManagementApp.Application;

internal static class AllowedRolesFactory
{
    public static IList<string> AdminLevel
    => new List<string>() { enUserRoles.Admin.ToString() };
    public static IList<string> LabSupervisorLevel
                => new List<string>() { enUserRoles.Admin.ToString(), enUserRoles.LabSupervisor.ToString() };

    public static IList<string> ResearcherLevel
                => new List<string>() { enUserRoles.Admin.ToString(), enUserRoles.LabSupervisor.ToString(),
                                        enUserRoles.Researcher.ToString()};

    public static IList<string> TechnicianLevel
                => new List<string>() { enUserRoles.Admin.ToString(), enUserRoles.LabSupervisor.ToString(),
                                         enUserRoles.Technician.ToString()};

    public static IList<string> ResearcherAndTechnicianLevel
                => new List<string>() { enUserRoles.Admin.ToString(), enUserRoles.LabSupervisor.ToString(),
                                        enUserRoles.Researcher.ToString(), enUserRoles.Technician.ToString()};

    public static IList<string> AnyUserLevel
                => new List<string>() { enUserRoles.Admin.ToString(), enUserRoles.LabSupervisor.ToString(),
                                        enUserRoles.Researcher.ToString(), enUserRoles.Technician.ToString(),
                                        enUserRoles.User.ToString()};
}

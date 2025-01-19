namespace ScientificLabManagementApp.Application;
public interface IRoleAuthorizRequest
{
   IEnumerable<string> AllowedRoles { get;}
}


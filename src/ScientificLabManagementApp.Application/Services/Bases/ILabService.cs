namespace ScientificLabManagementApp.Application;
public interface ILabService
{
    Task<LabDto> GetOneDtoByNameAsync(string name);
}

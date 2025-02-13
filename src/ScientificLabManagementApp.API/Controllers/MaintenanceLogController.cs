namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class MaintenanceLogController :
    ControllerBaseWithEndpoints<
        MaintenanceLogDto, GetOneMaintenanceLogByIdQuery, GetManyMaintenanceLogQuery,
        MaintenanceLogCommandData, AddMaintenanceLogCommandData, UpdateMaintenanceLogCommandData,
        AddMaintenanceLogCommand, UpdateMaintenanceLogCommand, DeleteMaintenanceLogCommand>

{
}
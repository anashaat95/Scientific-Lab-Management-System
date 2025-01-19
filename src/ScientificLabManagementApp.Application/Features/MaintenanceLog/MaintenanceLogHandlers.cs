namespace ScientificLabManagementApp.Application;
public class GetManyMaintenanceLogHandler : GetManyQueryHandlerBase<GetManyMaintenanceLogQuery, MaintenanceLog, MaintenanceLogDto> { }

public class GetOneMaintenanceLogByIdHandler : GetOneQueryHandlerBase<GetOneMaintenanceLogByIdQuery, MaintenanceLog, MaintenanceLogDto> { }

public class AddMaintenanceLogHandler : AddCommandHandlerBase<AddMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto> { }

public class DeleteMaintenanceLogHandler : DeleteCommandHandlerBase<DeleteMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto> { }

public class UpdateMaintenanceLogHandler : UpdateCommandHandlerBase<UpdateMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto> { }

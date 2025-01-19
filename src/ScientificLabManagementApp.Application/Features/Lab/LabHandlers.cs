namespace ScientificLabManagementApp.Application;
public class GetManyLabHandler : GetManyQueryHandlerBase<GetManyLabQuery, Lab, LabDto> { }

public class GetOneLabByIdHandler : GetOneQueryHandlerBase<GetOneLabByIdQuery, Lab, LabDto> { }

public class AddLabHandler : AddCommandHandlerBase<AddLabCommand, Lab, LabDto> { }

public class DeleteLabHandler : DeleteCommandHandlerBase<DeleteLabCommand, Lab, LabDto> { }

public class UpdateLabHandler : UpdateCommandHandlerBase<UpdateLabCommand, Lab, LabDto> { }

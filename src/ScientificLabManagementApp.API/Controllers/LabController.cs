namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class LabController :
    ControllerBaseWithEndpoints<
        LabDto, GetOneLabByIdQuery, GetManyLabQuery,
        LabCommandData, AddLabCommandData,  UpdateLabCommandData,
        AddLabCommand, UpdateLabCommand, DeleteLabCommand>

{
}
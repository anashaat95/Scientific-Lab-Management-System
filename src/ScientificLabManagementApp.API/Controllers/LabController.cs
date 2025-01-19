namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class LabController :
    ControllerBaseWithEndpoints<
        LabDto, GetOneLabByIdQuery, GetManyLabQuery,
        LabCommandData, AddLabCommand, UpdateLabCommand, DeleteLabCommand>

{
}
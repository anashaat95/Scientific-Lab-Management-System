namespace ScientificLabManagementApp.API;

public abstract class ControllerBaseWithEndpoints<TDto, TGetOneQuery, TGetManyQuery, TCommandData, TAddCommandData, TUpdateCommandData, TAddCommand, TUpdateCommand, TDeleteCommand> : ApiControllerBase
    
    where TGetOneQuery : GetOneQueryBase<TDto>
    where TGetManyQuery : GetManyQueryBase<TDto>
    where TAddCommand : AddCommandBase<TDto, TAddCommandData>
    where TUpdateCommand : UpdateCommandBase<TDto, TUpdateCommandData>
    where TDeleteCommand : DeleteCommandBase<TDto>
    where TCommandData : class
    where TAddCommandData : class, TCommandData
    where TUpdateCommandData : class, TCommandData
    where TDto : class 
{
    [HttpGet]
    [HttpHead]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll([FromQuery]TGetManyQuery query)
    {
        var response = await Mediator.Send(query);
        return Result.Create(response);
    }

    // GET api/<Controller>/5
    [HttpGet("{Id}")]
    public virtual async Task<ActionResult<TDto>> Get(TGetOneQuery query)
    {
        var response = await Mediator.Send(query);
        return Result.Create(response);
    }

    // POST api/<Controller>
    [HttpPost]
    public virtual async Task<ActionResult<TDto>> Post(TAddCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    // PUT api/<Controller>/5
    [HttpPut("{Id}")]
    public virtual async Task<ActionResult<TDto>> Put(TUpdateCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }


    // DELETE api/<Controller>/5
    [HttpDelete("{Id}")]
    public virtual async Task<ActionResult> Delete(TDeleteCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    [HttpOptions()]
    public virtual IActionResult GetOptions()
    {
        Response.Headers["Allow"] = "GET,POST,PUT,DELETE,OPTIONS";
        return Ok();
    }
}

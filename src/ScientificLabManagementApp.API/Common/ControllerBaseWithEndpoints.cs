﻿namespace ScientificLabManagementApp.API;

public abstract class ControllerBaseWithEndpoints<TDto, TGetOneQuery, TGetManyQuery, TCommandData, TAddCommand, TUpdateCommand, TDeleteCommand> : ApiControllerBase
    
    where TGetOneQuery : GetOneQueryBase<TDto>
    where TGetManyQuery : GetManyQueryBases<TDto>
    where TAddCommand : AddUpdateCommandBase<TDto, TCommandData>
    where TUpdateCommand : AddUpdateCommandBase<TDto, TCommandData>
    where TDeleteCommand : DeleteCommandBase<TDto>
    where TCommandData : class
    where TDto : class 
{
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        Console.WriteLine(HttpContext);
        var response = await Mediator.Send(Activator.CreateInstance<TGetManyQuery>());
        return Result.Create(response);
    }

    // GET api/<Controller>/5
    [HttpGet("{Id}")]
    public virtual async Task<ActionResult<TDto>> Get(TGetOneQuery command)
    {
        var response = await Mediator.Send(command);
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
}

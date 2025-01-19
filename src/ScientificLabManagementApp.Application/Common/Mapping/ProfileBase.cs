﻿namespace ScientificLabManagementApp.Application;

public abstract class ProfileBase<TEntity, TDto, TCommandData> : Profile
    where TEntity : IEntityHaveId   
    where TDto : class
    where TCommandData : class
{
    public abstract IMappingExpression<TEntity, TDto> ApplyEntityToDtoMapping();
    public virtual IMappingExpression<TSource, TEntity> ApplyCommandToEntityMapping<TSource>() where TSource : AddUpdateCommandBase<TDto, TCommandData>
    {
        return CreateMap<TSource, TEntity>();
    }
}

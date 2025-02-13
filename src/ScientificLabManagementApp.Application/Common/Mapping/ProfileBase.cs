namespace ScientificLabManagementApp.Application;

public abstract class ProfileBase<TEntity, TDto, TDataBase> : Profile
    where TEntity : IEntityHaveId   
    where TDto : class
    where TDataBase : class
{
    public abstract IMappingExpression<TEntity, TDto> ApplyEntityToDtoMapping();
    public virtual IMappingExpression<TSource, TEntity> ApplyCommandToEntityMapping<TSource, TData>()
        where TSource : AddUpdateCommandBase<TDto, TData>
        where TData : class, TDataBase
    {
        return CreateMap<TSource, TEntity>();
    }
}

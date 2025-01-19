namespace ScientificLabManagementApp.Application;
public class RoleMappingProfile : ProfileBase<ApplicationRole, RoleDto, RoleCommandData>
{
    public RoleMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyCommandToEntityMapping<AddRoleCommand>();
        ApplyCommandToEntityMapping<UpdateRoleCommand>();
    }

    public override IMappingExpression<ApplicationRole, RoleDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<ApplicationRole, RoleDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(x => x.created_at, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(x => x.updated_at, opt => opt.MapFrom(src => src.UpdatedAt));
    }
    public override IMappingExpression<TSource, ApplicationRole> ApplyCommandToEntityMapping<TSource>()
    {
        return CreateMap<TSource, ApplicationRole>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name));
    }
}
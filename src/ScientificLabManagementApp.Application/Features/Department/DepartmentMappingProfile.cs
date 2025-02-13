
namespace ScientificLabManagementApp.Application;
public class DepartmentMappingProfile : ProfileBase<Department, DepartmentDto, DepartmentCommandData>
{
    public DepartmentMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyCommandToEntityMapping<AddDepartmentCommand, AddDepartmentCommandData>();
        ApplyCommandToEntityMapping<UpdateDepartmentCommand, UpdateDepartmentCommandData>();
    }
    public override IMappingExpression<Department, DepartmentDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<Department, DepartmentDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(x => x.company_url, opt => opt.MapFrom(src => ApiUrlFactory<Company>.Create(src.CompanyId)))
                .ForMember(x => x.company_name, opt => opt.MapFrom(src => src.Company.Name));
    }
    public override IMappingExpression<TSource, Department> ApplyCommandToEntityMapping<TSource, TData>()
    {
        return CreateMap<TSource, Department>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Data.Location))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Data.company_id));
    }


}

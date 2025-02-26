namespace ScientificLabManagementApp.Application;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<SignupCommand, ApplicationUser>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                 .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phone_number))
                 .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url))
                 .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.company_id))
                 .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.department_id))
                 .ForMember(dest => dest.LabId, opt => opt.MapFrom(src => src.lab_id))
                 .ForMember(dest => dest.GoogleScholarUrl, opt => opt.MapFrom(src => src.google_scholar_url))
                 .ForMember(dest => dest.AcademiaUrl, opt => opt.MapFrom(src => src.academia_url))
                 .ForMember(dest => dest.ScopusUrl, opt => opt.MapFrom(src => src.scopus_url))
                 .ForMember(dest => dest.ResearcherGateUrl, opt => opt.MapFrom(src => src.researcher_gate_url))
                 .ForMember(dest => dest.ExpertiseArea, opt => opt.MapFrom(src => src.expertise_area))
                 ;

        CreateMap<RefreshToken, RefreshToken>();
        CreateMap<UpdateProfileCommand, ApplicationUser>()
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                 .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.phone_number))
                 .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url))
                 .ForMember(dest => dest.GoogleScholarUrl, opt => opt.MapFrom(src => src.google_scholar_url))
                 .ForMember(dest => dest.AcademiaUrl, opt => opt.MapFrom(src => src.academia_url))
                 .ForMember(dest => dest.ScopusUrl, opt => opt.MapFrom(src => src.scopus_url))
                 .ForMember(dest => dest.ResearcherGateUrl, opt => opt.MapFrom(src => src.researcher_gate_url))
                 .ForMember(dest => dest.ExpertiseArea, opt => opt.MapFrom(src => src.expertise_area))
                 ;
        CreateMap<UpdateUsernameCommand, ApplicationUser>();

        CreateMap<SendUpdateEmailCommand, ApplicationUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.new_email));

        CreateMap<UpdateEmailCommand, ApplicationUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.new_email));


        CreateMap<ChangePasswordCommand, ApplicationUser>();

    }
}
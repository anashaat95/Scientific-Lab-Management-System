
namespace ScientificLabManagementApp.Application;

public class LabService : ILabService
{
    protected readonly IGenericRepository<Lab> _labRepository;
    protected readonly IMapper _mapper;

    public LabService(IGenericRepository<Lab> labRepository, IMapper mapper)
    {
        _labRepository = labRepository;
        _mapper = mapper;
    }

    public async Task<LabDto> GetOneDtoByNameAsync(string name)
    {
        var result = await _labRepository
                           .GetQueryableEntityAsync(e => e.Name.ToLower() == name.ToLower())
                           .ProjectTo<LabDto>(_mapper.ConfigurationProvider)
                           .FirstOrDefaultAsync();

        return result;
    }
}

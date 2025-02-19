namespace ScientificLabManagementApp.Application;

public class GetManyLabQuery : GetManyQueryBases<LabDto>{}
public class GetManyLabSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetOneLabByIdQuery : GetOneQueryBase<LabDto>{}

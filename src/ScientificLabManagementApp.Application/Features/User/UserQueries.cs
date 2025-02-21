namespace ScientificLabManagementApp.Application;

public class GetManyUserQuery : GetManyQueryBases<UserDto>{}
public class GetManyUserSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetManyTechnicianQuery : GetManyQueryBases<UserDto>{}
public class GetManyTechnicianSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto>{}
public class GetManySupervisorQuery : GetManyQueryBases<UserDto>{}
public class GetManySupervisorSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetManyResearcherQuery : GetManyQueryBases<UserDto>{}
public class GetManyResearcherSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetOneUserByIdQuery : GetOneQueryBase<UserDto>{}
public class GetOneUserByEmailQuery : GetOneQueryByEmailBase<UserDto>{}
public class ExistOneUserByEmailQuery : ExistOneQueryByEmailBase{}

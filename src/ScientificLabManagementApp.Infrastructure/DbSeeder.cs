using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ScientificLabManagementApp.Infrastructure;

public class DbSeeder
{
    private ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;
    private IConfiguration _configurations;
    public DbSeeder(IServiceProvider serviceProvider, IConfiguration configurations)
    {
        _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        _roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        _configurations = configurations;
    }
    public async Task Run()
    {
        var adminUser = await _userManager.FindByEmailAsync("anashaat95@mans.edu.eg");

        if (adminUser != null)
            return;

        _context.Database.EnsureCreated();


        var admin = new ApplicationRole { Name = enUserRoles.Admin.ToString() };
        var labSupervisior = new ApplicationRole { Name = enUserRoles.LabSupervisor.ToString() };
        var researcher = new ApplicationRole { Name = enUserRoles.Researcher.ToString() };
        var technician = new ApplicationRole { Name = enUserRoles.Technician.ToString() };

        if (!await _roleManager.Roles.AnyAsync())
        {
            await _roleManager.CreateAsync(admin);
            await _roleManager.CreateAsync(labSupervisior);
            await _roleManager.CreateAsync(researcher);
            await _roleManager.CreateAsync(technician);
        }

        var egypt = new Country() { Name = "Egypt" };
        await _context.Countries.AddAsync(egypt);

        var mansoura = new City() { Name = "Mansoura" };
        await _context.Cities.AddAsync(mansoura);

        var mansouraUniversity = new Company
        {
            Name = "Mansoura University",
            City = mansoura,
            Country = egypt,
            Street = "Al-Gomhoria Street",
            ZipCode = "35742",
            Website = "https://www.mans.edu.eg/"
        };
        await _context.Companies.AddAsync(mansouraUniversity);


        var pharmaceutics = new Department { Name = "Pharmaceutics", Location = "3rd Floor, Building B", Company = mansouraUniversity };
        await _context.Departments.AddAsync(pharmaceutics);



        var ahmed = new ApplicationUser
        {
            UserName = "anashaat95",
            FirstName = "Ahmed",
            LastName = "Nashaat",
            PhoneNumber = "01069427021",
            DepartmentId = pharmaceutics.Id,
            CompanyId = mansouraUniversity.Id,
            Email = "anashaat95@mans.edu.eg",
            EmailConfirmed = true,
            ExpertiseArea = "Pharmaceutics and Nanotechnology",
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var ahmedPassword = _configurations["TEST_PASSWORD"];
        var result = await _userManager.CreateAsync(ahmed, ahmedPassword);
        
        ahmed = await _userManager.FindByEmailAsync(ahmed.Email);
        await _userManager.AddToRolesAsync(ahmed, new List<string>() { enUserRoles.Admin.ToString(), enUserRoles.Researcher.ToString() });
        
        ahmed = await _userManager.FindByEmailAsync(ahmed.Email);

        var pharmaceuticsLab = new Lab { Name = "Pharmaceutics Lab", Capacity = 20, OpeningTime = new TimeOnly(9, 0), ClosingTime = new TimeOnly(16, 0), Department = pharmaceutics, Supervisior = ahmed };
        await _context.Labs.AddAsync(pharmaceuticsLab);

        var sigmaCentrifuge = new Equipment
        {
            Name = "Sigma High Speed Centrifuge",
            TotalQuantity = 1,
            Type = enEquipmentType.Centrigue,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "CE16-4x100RD",
            Specifications = "Sigma centrifuge Refrigerated 2-16P",
            Description = "Used for separating nano preparations",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var labofugeCentrifuge = new Equipment
        {
            Name = "Labofuge Minor Centrifuge",
            TotalQuantity = 1,
            Type = enEquipmentType.Centrigue,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "LM-001",
            Specifications = "Model: Labofuge with stepless speed control",
            Description = "Used for separating nano preparations",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var stuartWaterBath = new Equipment
        {
            Name = "Stuart Water Bath and Pump",
            TotalQuantity = 1,
            Type = enEquipmentType.WaterBath,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "RE400/MS",
            Specifications = "Model: Stuart RE400/MS with water bath and pump",
            Description = "Used for thin film layer formation",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var sonicHomogenizer = new Equipment
        {
            Name = "Sonic Vibro Cell Ultrasonic Homogenizer",
            TotalQuantity = 1,
            Type = enEquipmentType.Homogenizer,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "SVC-001",
            Specifications = "Model: Sonic vibro cell ultrasonic homogenizer",
            Description = "Used for nano-emulsion formation and nanoparticle size reduction",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var pHMeter = new Equipment
        {
            Name = "pH Meter",
            TotalQuantity = 1,
            Type = enEquipmentType.pHMeter,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "P901",
            Specifications = "Model: Consort (Belgium) P 901",
            Description = "Used for measuring pH of solutions",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var daDissolutionTester = new Equipment
        {
            Name = "DA-6D Dissolution Tester",
            TotalQuantity = 1,
            Type = enEquipmentType.DissolutionTester,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "DA-6D",
            Specifications = "Model: Scientific Tablet Dissolution Test Apparatus DA-6D",
            Description = "Used for measuring drug release rate from tablets",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var uvVisSpectormeter = new Equipment
        {
            Id = Guid.NewGuid().ToString(),
            Name = "UV/Vis Spectrophotometer",
            TotalQuantity = 1,
            Type = enEquipmentType.UvVisSpectrometer,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "UV-2950",
            Specifications = "Model: UV/Vis UV-2950",
            Description = "Used for spectral analysis",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var denverBalance = new Equipment
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Analytical Balance",
            TotalQuantity = 1,
            Type = enEquipmentType.Balance,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "M214",
            Specifications = "Model: DENVER instrument M214 COLO",
            Description = "Precision weighing instrument",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var tbTabletHardnessTester = new Equipment
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Tablet Hardness Tester",
            TotalQuantity = 1,
            Type = enEquipmentType.HardnessTester,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "TB24-43370",
            Specifications = "Model: TB 24 No. 43370",
            Description = "Used for measuring tablet hardness",
            CanBeLeftOverNight = false,
            CompanyId = mansouraUniversity.Id
        };

        var oven = new Equipment
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Laboratory Oven",
            TotalQuantity = 1,
            Type = enEquipmentType.Oven,
            Status = enEquipmentStatus.Available,
            PurchaseDate = DateTime.Now.AddYears(-2),
            SerialNumber = "FCO-65D",
            Specifications = "Model: Taisite Blast Drying Oven FCO-65D",
            Description = "Used for stability testing of pharmaceutical preparations at different temperatures",
            CanBeLeftOverNight = true,
            CompanyId = mansouraUniversity.Id
        };

        var equipments = new List<Equipment> { sigmaCentrifuge, labofugeCentrifuge, stuartWaterBath, sonicHomogenizer, pHMeter,
                                               daDissolutionTester, uvVisSpectormeter, denverBalance, tbTabletHardnessTester,
                                               oven };

        await _context.Equipments.AddRangeAsync(equipments);


        var booking = new Booking
        {
            StartDateTime = new DateTime(2024, 12, 7, 9, 0, 0),
            EndDateTime = new DateTime(2024, 12, 7, 12, 0, 0),
            IsOnOverNight = false,
            Notes = "I Will use only 6 tubes at speed 15000 and for 2 hours",
            Status = enBookingStatus.Confirmed,
            UserId = ahmed.Id,
            EquipmentId = sigmaCentrifuge.Id,
        };

        await _context.Bookings.AddAsync(booking);

        await _context.SaveChangesAsync();
    }
}

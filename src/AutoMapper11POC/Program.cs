using AutoMapper;
using AutoMapper11POC.Helpers;
using AutoMapper11POC.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

// The initial "bootstrap" logger is able to log errors during start-up. It's completely replaced by the
//   logger configured in `UseSerilog()` in HostBuilderHelper, once configuration and dependency-injection
//   have both been set up successfully.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var host = HostBuilderHelper.BuildHost(args);

    using (var serviceScope = host.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var entities = Enumerable.Range(0, 10)
            .Select((_, ix) => new SavedItemSearchEntity
            {
                Manufacturer = $"Manufacturer{ix}",
                Importer = $"Importer{ix}",
                Model = $"Model{ix}",
                Serial = $"Serial{ix}",
            })
            .ToList();


        var mapper = services.GetRequiredService<IMapper>();

        var models = mapper.Map<SavedItemSearchModel[]>(entities.OrderBy(s => s.Manufacturer));
    }

    return 0;
}
catch (Exception ex)
{
    // Log the exception and let the application terminate. Try to write to Serilog, but in case it's not
    //   yet configured, also write out to the console.
    Log.Fatal(ex, "Host terminated unexpectedly");

    return -1;
}
finally
{
    Log.CloseAndFlush();
}

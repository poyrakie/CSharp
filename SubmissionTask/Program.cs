using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubmissionTask.Interfaces;
using SubmissionTask.Repositories;
using SubmissionTask.Services;
///<summary>
/// Startpunkt för applikationen. Gör en setup för dependency injection container.
/// registrerar services och repositories, bygger host och startar applikationen.
/// Main menu visas efter initialiseringen av nödvändiga services.
///</summary>
var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton<IContactService, ContactService>();
    services.AddSingleton<IContactRepository, ContactRepository>();
    services.AddSingleton<IFileService, FileService>();
    services.AddSingleton<IMenuService, MenuService>();

}).Build();
builder.Start();
Console.Clear();

var menuService = builder.Services.GetRequiredService<IMenuService>();
menuService.ShowMainMenu();
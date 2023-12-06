using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubmissionTask.Interfaces;
using SubmissionTask.Models;
using SubmissionTask.Repositories;
using SubmissionTask.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton<IContactService, ContactService>();
    services.AddSingleton<IContactRepository, ContactRepository>();
    services.AddSingleton<IFileService, FileService>();
    services.AddSingleton<IMenuService, MenuService>();
    services.AddSingleton<IContact, Contact>();
    services.AddSingleton<IAddress, Address>();

    
}).Build();
builder.Start();
Console.Clear();

var menuService = builder.Services.GetRequiredService<IMenuService>();
menuService.ShowMainMenu();
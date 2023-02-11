using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Helpers;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using PlacowkaOswiatowa.Infrastructure.Repository;
using PlacowkaOswiatowa.Infrastructure.Repository.EntityConfiguration;
using PlacowkaOswiatowa.ViewModels;
using Serilog;
using System;
using System.Windows;

namespace PlacowkaOswiatowa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string connectionString;
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();   
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configure =>
                {
                    configure.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                        optional: true)
                    .AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    //dodanie profilu AutoMapera
                    var mapperConfig = new MapperConfiguration(mc =>
                        mc.AddProfile(new MappingProfile()));
                    var mapper = mapperConfig.CreateMapper();

                    connectionString = context.Configuration.GetConnectionString("Default");

                    //tutaj może być problem, jeżeli ten kontekst będzie wstrzykiwany do serwisów
                    //które mają czas życia Transient, bo to wymusi czas życia kontekst na Transient
                    //chociaż domyślnie jest singletonem
                    services.AddDbContext<AplikacjaDbContext>(options =>
                    {
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("Default"));
                        options.LogTo(Console.WriteLine);
                    }, ServiceLifetime.Transient);

                    services.AddSingleton(new AplikacjaDbContextFactory(connectionString));

                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton(mapper);
                    //Zmiana z Singleton na Transient
                    //w celu uniknięcia deadlocków
                    services.AddTransient<IPlacowkaRepository, PlacowkaRepository>();
                    services.AddSingleton<LoginViewModel>();
                    services.AddSingleton<WszyscyPracownicyViewModel>();
                    services.AddSingleton<WszyscyUczniowieViewModel>();
                    services.AddTransient<NowyPracownikViewModel>();
                    services.AddTransient<NowyUczenViewModel>();
                    services.AddTransient<ZarobkiPracownikaViewModel>();
                    services.AddTransient<UrlopPracownikaViewModel>();
                    services.AddSingleton<WszystkieAdresyViewModel>();
                    services.AddSingleton<WszystkiePrzedmiotyViewModel>();
                    services.AddSingleton<WszystkieStanowiskaViewModel>();
                    services.AddSingleton<WszystkieOcenyViewModel>();
                    services.AddSingleton<WszystkieUrlopyViewModel>();
                    services.AddTransient<NowaUmowaViewModel>();
                    services.AddSingleton<WszystkieUmowyViewModel>();
                    services.AddTransient<NowyAdresViewModel>();
                    services.AddTransient<RejestracjaViewModel>();
                })
                .ConfigureLogging((context, config) =>
                {
                    //dodanie konfiguracji logowania Serilog
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .CreateLogger();

                    config.AddSerilog(Log.Logger);
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            Log.Logger.Information("Aplikacja jest uruchamiana.");

            var window = new MainWindow
            {
                DataContext = _host.Services.GetRequiredService<MainWindowViewModel>()
            };

            window.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            Log.Logger.Information("Aplikacja kończy działanie");
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}

using FutbolSolution.Core.DTOs.InjuresSuspensionDTOs;
using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.DTOs.RefereeDTOs;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using FutbolSolution.Core.Validations;
using FutbolSolution.Repository;
using FutbolSolution.Repository.Repositories;
using FutbolSolution.Service.Mappers;
using FutbolSolution.Service.Mappers.FutbolSolution.Service.Mappers;
using FutbolSolution.Service.Services;
using FutbolSolution.WPF.Configuration;
using FutbolSolution.WPF.Services.Navigation;
using FutbolSolution.WPF.Services.Search;
using FutbolSolution.WPF.ViewModels.AnalysisViewModel;
using FutbolSolution.WPF.ViewModels.CoefficientViewModel;
using FutbolSolution.WPF.ViewModels.InjuresSuspensionViewModel;
using FutbolSolution.WPF.ViewModels.MainViewModel;
using FutbolSolution.WPF.ViewModels.MatchView;
using FutbolSolution.WPF.ViewModels.MatchViewModel;
using FutbolSolution.WPF.ViewModels.PlayerViewModel;
using FutbolSolution.WPF.ViewModels.RefereeViewModel;
using FutbolSolution.WPF.ViewModels.SearchViewModel;
using FutbolSolution.WPF.ViewModels.TeamViewModel;
using FutbolSolution.WPF.Views;
using FutbolSolution.WPF.Views.AnalysisView;
using FutbolSolution.WPF.Views.CoefficientView;
using FutbolSolution.WPF.Views.InjuresSuspensionView;
using FutbolSolution.WPF.Views.MainView;
using FutbolSolution.WPF.Views.MatchView;
using FutbolSolution.WPF.Views.PlayerView;
using FutbolSolution.WPF.Views.RefereeView;
using FutbolSolution.WPF.Views.TeamView;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace FutbolSolution.WPF
{
    public static class Bootstrapper
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Initialize()
        {
            var services = new ServiceCollection();
            var configuration = ConfigureAppSettings();

            // Configure services
            ConfigureServices(services, configuration);

            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();
        }

        private static IConfiguration ConfigureAppSettings()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return configurationBuilder.Build();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register application settings
            services.Configure<AppSettings>(options =>
            {
                options.ConnectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            });

            // Register the AppDbContext with the connection string from configuration
            var connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            services.AddTransient<AppDbContext>(provider => new AppDbContext(connectionString));

            // Register navigation service
            services.AddSingleton<INavigationService, NavigationService>();

            // Register views and their view models
            services.AddTransient<MainView>();
            services.AddTransient<MainViewModel>();
       

            // Add any other services here
            //Search 
            services.AddSingleton<ISearchService, SearchService>();
            services.AddSingleton<SearchService>();
            services.AddSingleton<SearchViewModel>();

            services.AddTransient<MainPlayerView>();
            services.AddTransient<MainPlayerViewModel>();
            services.AddSingleton<IMapper<BasePlayerDTO, Player>, PlayerMapper>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<CreatePlayerView>();
            services.AddTransient<CreatePlayerViewModel>();
            services.AddTransient<CheckPlayerView>();
            services.AddTransient<CheckPlayerViewModel>();

            services.AddSingleton<IMapper<BasePlayerDTO, PlayerStats>, PlayerStatsMapper>();
            services.AddTransient<IPlayerStatsRepository, PlayerStatsRepository>();
            services.AddTransient<IPlayerStatsService, PlayerStatsService>();

            services.AddSingleton<IMapper<BaseInjuriesSuspensionsDTO, InjuriesSuspensions>, InjuriesSuspensionsMapper>();
            services.AddTransient<IInjuriesSuspensionsRepository, InjuriesSuspensionsRepository>();
            services.AddTransient<IInjuresSuspensionService, InjuriesSuspensionService>();

            services.AddTransient<CheckPlayerView>();
            services.AddTransient<CheckPlayerViewModel>();

            services.AddTransient<CreateInjuresSuspensionView>();
            services.AddTransient<CreateInjuresSuspensionViewModel>();

            services.AddTransient<MainTeamView>();
            services.AddTransient<MainTeamViewModel>();

            services.AddSingleton<IMapper<BaseTeamDTO, Team>, TeamMapper>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<ITeamStatisticsRepository, TeamStatisticsRepository>();
            services.AddSingleton<IMapper<BaseTeamDTO, TeamStatistics>, TeamStatsMapper>();
            services.AddTransient<ITeamStatisticsService, TeamStatisticService>();

            services.AddTransient<CreateTeamView>();
            services.AddTransient<CreateTeamViewModel>();

            services.AddTransient<UpdateTeamView>();
            services.AddTransient<UpdateTeamViewModel>();

            services.AddTransient<CheckTeamView>();
            services.AddTransient<CheckTeamViewModel>();

           
            services.AddTransient<UpdateTeamStatsViewModel>();

            services.AddTransient<UpdateTeamStatsView>();

            services.AddTransient<MainPlayerView>();
            services.AddTransient<MainPlayerViewModel>();

            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<IMatchService, MatchService>();

            services.AddTransient<MainRefereeView>();
            services.AddTransient<MainRefereeViewModel>();

            services.AddTransient<CreateRefereeView>();
            services.AddTransient<CreateRefereeViewModel>();

            services.AddTransient<UpdateRefereeView>();
            services.AddTransient<UpdateRefereeViewModel>();

            services.AddSingleton<IMapper<BaseRefereeDTO, Referee>, RefereeMapper>();
            services.AddTransient<IRefereeRepository, RefereeRepository>();
            services.AddTransient<IRefereeService, RefereeService>();

            services.AddTransient<MainMatchView>();
            services.AddTransient<MainMatchViewModel>();
            services.AddSingleton<IMapper<BaseMatchDTO, Match>, MatchMapper>();
            services.AddSingleton<IMapper<BaseMatchDTO, MatchStatistics>, MatchStatsMapper>();
            services.AddTransient<IMatchRepository,MatchRepository>();
            services.AddTransient<IMatchService,MatchService>();
            services.AddTransient<IMatchStatisticsRepository, MatchStatisticsRepository>();
            services.AddTransient<IMatchStatisticsService, MatchStatisticsService>();
            services.AddTransient<CreateMatchView>();
            services.AddTransient<CreateMatchViewModel>();
            services.AddTransient<UpdateMatchView>();
            services.AddTransient<UpdateMatchViewModel>();

            services.AddTransient<CheckMatchView>();
            services.AddTransient<CheckMatchViewModel>();

            services.AddTransient<UpdateMatchStatsView>();
            services.AddTransient<UpdateMatchStatsViewModel>();

            services.AddTransient<IPlayerStatsService, PlayerStatsService>();
            services.AddTransient<UpdatePlayerStatsView>();
            services.AddTransient<UpdatePlayerStatsViewModel>();

            services.AddTransient<UpdatePlayerView>();
            services.AddTransient<UpdatePlayerViewModel>();


            services.AddTransient<CreateInjuresSuspensionView>();
            services.AddTransient<CreateInjuresSuspensionViewModel>();

            services.AddTransient<IPlayerTeamLink, PlayerTeamLinkService>();
            services.AddSingleton<IMapper<PlayerTeamLinkDTO, PlayerTeamLink>,PlayerTeamLinkMapper>();
            services.AddTransient<IPlayerTeamLinkRepository, PlayerTeamLinkRepository>();


            services.AddTransient<AnalysisView>();
            services.AddTransient<AnalysisViewModel>();

            services.AddTransient<CoefficientView>();
            services.AddTransient<CoefficientViewModel>();

            services.AddTransient<MatchStatsValidator>();
            services.AddTransient<TeamStatisticsValidator>();
        }
    }
}

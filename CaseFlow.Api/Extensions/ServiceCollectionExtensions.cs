using CaseFlow.Application.Interfaces;
using CaseFlow.Application.Services;
using CaseFlow.Domain.Interfaces;
using CaseFlow.Infrastructure.Repository;

namespace CaseFlow.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ICaseService, CaseService>();
        services.AddScoped<ICaseFileService, CaseFileService>();
        services.AddScoped<ICaseStatusHistoryService, CaseStatusHistoryService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IUserSettingsService, UserSettingsService>();
        return services;
    }

    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICaseRepository, CaseRepository>();
        services.AddScoped<ICaseFileRepository, CaseFileRepository>();
        services.AddScoped<ICaseStatusHistoryRepository, CaseStatusHistoryRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
        return services;
    }
}

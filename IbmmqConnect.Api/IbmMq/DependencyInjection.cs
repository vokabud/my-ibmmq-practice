namespace IbmmqConnect.Api.IbmMq;

public static class DependencyInjection
{
    public static void AddIbmMq(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<Configuration>("IbmMq", configuration.GetSection("ibmmq"));
        services.AddScoped<IProducer, Producer>();
    }
}

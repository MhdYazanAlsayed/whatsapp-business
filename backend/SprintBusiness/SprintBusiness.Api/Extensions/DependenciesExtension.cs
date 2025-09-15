using Scrutor;
using SprintBuisness.Contracts.Markers;
using System.Reflection;

namespace SprintBusiness.Api.Extensions
{
    public static class DependenciesExtension
    {
        public static IServiceCollection AddDependencies (this IServiceCollection services)
        {
            var assemblies = LoadAssemblies();

            services.Scan(scan => scan
            .FromAssemblies(assemblies)
             .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
             .UsingRegistrationStrategy(RegistrationStrategy.Skip)
             .AsImplementedInterfaces()
             .WithSingletonLifetime());

            services.Scan(scan => scan
             .FromAssemblies(assemblies)
             .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
             .UsingRegistrationStrategy(RegistrationStrategy.Skip)
             .AsImplementedInterfaces()
             .WithScopedLifetime());

            services.Scan(scan => scan
             .FromAssemblies(assemblies)
             .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
             .UsingRegistrationStrategy(RegistrationStrategy.Skip)
             .AsImplementedInterfaces()
             .WithTransientLifetime());

            //services.AddAutoMapper(typeof(FuelMasterMapper));

            return services;
        }

        public static Assembly[] LoadAssemblies()
        {
            AppDomain.CurrentDomain.Load("SprintBuisness.Application");
            AppDomain.CurrentDomain.Load("SprintBuisness.Contracts");
            AppDomain.CurrentDomain.Load("SprintBusiness.Domain");
            AppDomain.CurrentDomain.Load("SprintBusiness.Features");
            AppDomain.CurrentDomain.Load("SprintBuisness.EntityframeworkCore");
            AppDomain.CurrentDomain.Load("SprintBusiness.Shared");
            AppDomain.CurrentDomain.Load("SprintBusiness.Whatsapp");

            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}

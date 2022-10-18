using Microsoft.EntityFrameworkCore;
using GenericRepositoryBuilder;
using System.Reflection;

namespace StoneDesafio.Services
{
    public static class IServiceExtension
    {
        public static IServiceCollection AddGenericRepository<TInterface, TContext>(this IServiceCollection serviceCollection) 
            where TInterface : class where TContext : DbContext
        {
            Builder.BuildRepository<TInterface, TContext>();
            serviceCollection.AddScoped(factory =>
            {
                var dbContext = factory.GetRequiredService<TContext>();
                return Builder.GetScopedRepository<TInterface>(dbContext);
            });
            return serviceCollection;
        }
    }
}

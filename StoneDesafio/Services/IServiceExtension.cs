using Microsoft.EntityFrameworkCore;
using GenericRepositoryBuilder;
using System.Reflection;

namespace StoneDesafio.Services
{
    public static class IServiceExtension
    {
        private static object repository;

        public static IServiceCollection AddGenericRepository<TGeneric, TContext>(this IServiceCollection serviceCollection) 
            where TGeneric : class where TContext : DbContext
        {
            serviceCollection.AddScoped(factory =>
            {
                var dbContext = factory.GetRequiredService<TContext>();
                if (repository == null)
                {
                    var repoBuilder = new Builder(typeof(TGeneric));
                    repository = repoBuilder.Build(dbContext);

                    return (TGeneric)repository;
                }
                
                var dbField = repository.GetType().GetRuntimeFields().Single();
                dbField.SetValue(repository,dbContext);
                
                return (TGeneric) repository;
            });

            return serviceCollection;
        }
    }
}

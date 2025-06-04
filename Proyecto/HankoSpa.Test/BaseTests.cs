using AutoMapper;
using HankoSpa.Data;
using HankoSpa.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HankoSpa.Test
{
    public class BaseTests
    {
        protected AppDbContext BuidContext(string dbName)
        {
            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(dbName)
                .Options;
            AppDbContext dataContext = new AppDbContext(options);
            return dataContext;

        }
        protected IMapper ConfigureAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            return config.CreateMapper();
        }

        protected WebApplicationFactory<Program> BuildWebApplicationFactory(string dbName)
        {
            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>();

            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    ServiceDescriptor? descriptor = services.FirstOrDefault(db => db.ServiceType == typeof(AppDbContext));

                    if (descriptor is not null)
                    {
                        services.Remove(descriptor);
                    }
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseInMemoryDatabase(dbName));
                });
            });
            return factory;
        }
    }
}

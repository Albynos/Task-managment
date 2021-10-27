using System;
using Application;
using AutoMapper;
using Infrastructure.Persistence;

namespace Tests.Queries
{
    public class TestQueryBase : IDisposable
    {
        protected ApplicationDbContext Context;
        protected IMapper Mapper;

        public TestQueryBase()
        {
            Context = TaskDbContextFactory.Create();
            var configurationBuilder = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplyMappingProfile(typeof(ApplyMappingProfile).Assembly));
            });
            Mapper = configurationBuilder.CreateMapper();
        }

        public void Dispose()
        {
            TaskDbContextFactory.Destroy(Context);
        }
    }
}
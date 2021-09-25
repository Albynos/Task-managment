using System;
using Infrastructure.Persistence;
using NUnit.Framework;

namespace Tests.Commands
{
    public class TestCommandBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;
        
        public TestCommandBase()
        {
            Context = TaskDbContextFactory.Create();
        }

        public void Dispose()
        {
            TaskDbContextFactory.Destroy(Context);
        }
    }
}
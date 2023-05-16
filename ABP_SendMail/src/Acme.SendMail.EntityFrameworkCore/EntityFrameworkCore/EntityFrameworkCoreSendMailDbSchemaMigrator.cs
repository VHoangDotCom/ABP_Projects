using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.SendMail.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.SendMail.EntityFrameworkCore;

public class EntityFrameworkCoreSendMailDbSchemaMigrator
    : ISendMailDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSendMailDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SendMailDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SendMailDbContext>()
            .Database
            .MigrateAsync();
    }
}

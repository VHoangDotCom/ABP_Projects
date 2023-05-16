using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.SendMail.Data;

/* This is used if database provider does't define
 * ISendMailDbSchemaMigrator implementation.
 */
public class NullSendMailDbSchemaMigrator : ISendMailDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

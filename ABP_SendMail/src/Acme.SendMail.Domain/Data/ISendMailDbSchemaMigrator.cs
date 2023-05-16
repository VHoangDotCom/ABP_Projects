using System.Threading.Tasks;

namespace Acme.SendMail.Data;

public interface ISendMailDbSchemaMigrator
{
    Task MigrateAsync();
}

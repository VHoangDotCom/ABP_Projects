using Acme.SendMail.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Acme.SendMail.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SendMailEntityFrameworkCoreModule),
    typeof(SendMailApplicationContractsModule)
    )]
public class SendMailDbMigratorModule : AbpModule
{

}

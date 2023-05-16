using Acme.SendMail.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.SendMail;

[DependsOn(
    typeof(SendMailEntityFrameworkCoreTestModule)
    )]
public class SendMailDomainTestModule : AbpModule
{

}

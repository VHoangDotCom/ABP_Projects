using Volo.Abp.Modularity;

namespace Acme.SendMail;

[DependsOn(
    typeof(SendMailApplicationModule),
    typeof(SendMailDomainTestModule)
    )]
public class SendMailApplicationTestModule : AbpModule
{

}

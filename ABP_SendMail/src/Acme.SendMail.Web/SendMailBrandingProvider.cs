using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Acme.SendMail.Web;

[Dependency(ReplaceServices = true)]
public class SendMailBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SendMail";
}

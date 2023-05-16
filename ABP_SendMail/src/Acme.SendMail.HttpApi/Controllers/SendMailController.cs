using Acme.SendMail.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.SendMail.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SendMailController : AbpControllerBase
{
    protected SendMailController()
    {
        LocalizationResource = typeof(SendMailResource);
    }
}

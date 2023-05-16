using Acme.SendMail.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Acme.SendMail.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class SendMailPageModel : AbpPageModel
{
    protected SendMailPageModel()
    {
        LocalizationResourceType = typeof(SendMailResource);
    }
}

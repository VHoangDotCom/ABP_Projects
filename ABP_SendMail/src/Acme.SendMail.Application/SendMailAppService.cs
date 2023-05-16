using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acme.SendMail.Localization;
using Acme.SendMail.Mails;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;

namespace Acme.SendMail;

/* Inherit your application services from this class.
 */
public abstract class SendMailAppService : ApplicationService
{
    private readonly IBackgroundJobManager _backgroundJobManager;
    public SendMailAppService(IBackgroundJobManager backgroundJobManager)
    {
        LocalizationResource = typeof(SendMailResource);
        _backgroundJobManager = backgroundJobManager;
    }

    public async Task RegisterAsync(string userName, string emailAddress, string password)
    {
        //TODO: Create new user in the database...

        await _backgroundJobManager.EnqueueAsync(
            new EmailSendingArgs
            {
                EmailAddress = emailAddress,
                Subject = "You've successfully registered!",
                Body = "..."
            }
        );
    }

}

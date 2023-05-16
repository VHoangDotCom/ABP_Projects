using Acme.SendMail.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.SendMail.Permissions;

public class SendMailPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SendMailPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SendMailPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SendMailResource>(name);
    }
}

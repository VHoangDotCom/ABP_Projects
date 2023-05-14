using Microsoft.AspNetCore.Mvc;

namespace HangFire_AspNET.Services
{
    public interface IServiceManagement 
    {
        void SendMail();
        void UpdateDatabase();
        void GenerateMerchandise();
        void SyncData();
    }
}

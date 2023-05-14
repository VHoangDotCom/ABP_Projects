namespace HangFire_AspNET.Services
{
    public class ServiceManagement : IServiceManagement
    {
        public void GenerateMerchandise()
        {
            Console.WriteLine($"Generate Merchandise: Long running task {DateTime.Now.ToString("yyy-MM-dd HH:mm:ss")}");
        }

        public void SendMail()
        {
            Console.WriteLine($"Send Email: short running task {DateTime.Now.ToString("yyy-MM-dd HH:mm:ss")}");
        }

        public void SyncData()
        {
            Console.WriteLine($"Sync Data: short running task {DateTime.Now.ToString("yyy-MM-dd HH:mm:ss")}");
        }

        public void UpdateDatabase()
        {
            Console.WriteLine($"Update Database: Long running task {DateTime.Now.ToString("yyy-MM-dd HH:mm:ss")}");
        }
    }
}

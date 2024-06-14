
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Login;
using WuwaDB.DBAccess.Repository;

namespace WuwaDB.Services
{
    public class LoginUrlService : IHostedService
    {
       
        private readonly ILogger<LoginUrlService> _logger;
        private readonly IServiceProvider _serviceProvider;
        public LoginUrlService(ILogger<LoginUrlService> logger,IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var AdminRepository = scope.ServiceProvider.GetRequiredService<AdminRepository>();
                var UserRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
                Initialize(AdminRepository, UserRepository);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private async void Initialize(AdminRepository adminRepository, UserRepository userRepository)
        {
            try
            {
                var InfoUrl = await userRepository.GetDataAsync<Login_Info>();
                if (InfoUrl is not null)
                {
                    if (DateTime.UtcNow > InfoUrl.LastUpdated.AddDays(1))
                    {
                        InfoUrl.LoginUrl = Guid.NewGuid();
                        InfoUrl.LastUpdated = DateTime.UtcNow;
                        await adminRepository.UpdatesAsync(InfoUrl);
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to Update");
            }
        }
    }
}

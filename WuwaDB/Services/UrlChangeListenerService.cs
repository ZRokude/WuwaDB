using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
namespace WuwaDB.Services
{
    public class UrlChangeListenerService : IDisposable
    {
        private readonly LastestUrl _lastestUrl;
        private readonly NavigationManager _navigationManager;
        private bool _isSubscribed;

        public UrlChangeListenerService(LastestUrl lastestUrl, NavigationManager navigationManager)
        {
            _lastestUrl = lastestUrl;
            _navigationManager = navigationManager;
            Initialize();
        }

        private void Initialize()
        {
            _navigationManager.LocationChanged += OnLocationChanged;
            _isSubscribed = true;
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            _lastestUrl.UpdatePreviousUrl(e.Location);
        }

        public void Dispose()
        {
            if (_isSubscribed)
            {
                _navigationManager.LocationChanged -= OnLocationChanged;
                _isSubscribed = false;
            }
        }
    }
}

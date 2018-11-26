using System;

namespace Tracker.Interfaces
{
    public interface ISetupService
    {
        void RegisterUriScheme();
        void SetupAppByUri(Uri uri);
    }
}
using System;

namespace TrackerLib.Interfaces
{
    public interface ISetupService
    {
        void RegisterUriScheme();
        void SetupAppByUri(Uri uri);
    }
}
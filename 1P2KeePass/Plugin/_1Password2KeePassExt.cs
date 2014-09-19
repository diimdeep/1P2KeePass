using KeePass.Plugins;

namespace _1Password2KeePass
{
    public class _1Password2KeePassExt : KeePass.Plugins.Plugin
    {
        private IPluginHost _pluginHost = null;
        private _1PasswordPifFormatProvider _1PasswordPifFormatProvider;


        public override bool Initialize(IPluginHost host)
        {
            _pluginHost = host;

            _1PasswordPifFormatProvider = new _1PasswordPifFormatProvider();
            _pluginHost.FileFormatPool.Add(_1PasswordPifFormatProvider);
            return true;
        }

        public override void Terminate()
        {
            _pluginHost.FileFormatPool.Remove(_1PasswordPifFormatProvider);			
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinJobScheduleCore
{
    public class IoC
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private static IoC _instance;
        public static IoC Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IoC();
                return _instance;
            }
        }

        private IoC()
        {

        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}

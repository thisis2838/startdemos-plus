using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using startdemos_plus.Utils;

namespace startdemos_plus.Globals
{
    public static class Events
    {
        public static EventHandler<CommonEventArgs> BeganScanningProcess;
        public static EventHandler<CommonEventArgs> StoppedScanningProcess;
        public static EventHandler<CommonEventArgs> FoundGameProcess;
        public static EventHandler<CommonEventArgs> OnWorkerUpdate;
        public static EventHandler<CommonEventArgs> LostGameProcess;
        public static EventHandler<CommonEventArgs> DemoQueueStarted;
        public static EventHandler<CommonEventArgs> DemoQueueFinished;
        public static EventHandler<CommonEventArgs> GotDemos;
        public static EventHandler<CommonEventArgs> ChecksModified;
        public static EventHandler<CommonEventArgs> SessionStarted;
        public static EventHandler<CommonEventArgs> SessnionStopped;
    }
}

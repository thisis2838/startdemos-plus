using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using startdemos_plus.Src.DemoChecking;

namespace startdemos_plus.UI
{
    public class UIDemoCheckProfile
    {
        public string Name { get; set; }
        public List<DemoCheck> Checks { get; private set; }

        public UIDemoCheckProfile(string name, List<DemoCheck> checks = null)
        {
            Name = name;
            Checks = checks == null ? new List<DemoCheck>() : checks;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagementStudio.Classes
{
    public class AppSets
    {
        string AppPath;
        public AppSets() {
            AppPath = AppContext.BaseDirectory;
        }

        public void Init() { 
        
        }
    }
}

using DatabaseManagementStudio.Classes.Pack.Driver;
using DatabaseManagementStudio.Classes.Pack.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DatabaseManagementStudio.Classes.Pack
{
    internal class Pack
    {
        public IFace iFace = new IFace();
        public DriverList driverList = new DriverList();
        public Pack() { 
            
        }
    }
}

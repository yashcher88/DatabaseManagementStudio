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
    public class Pack
    {
        public AppSets appSets;
        public IFace iFace;
        public DriverList driverList;
        public Pack() {
            appSets = new AppSets();
            iFace = new IFace();
            driverList = new DriverList();
        }
    }
}

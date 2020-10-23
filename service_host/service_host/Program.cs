using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lib_wfc;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace service_host
{
    class Program
    {

        

        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(lib_wfc.server));

            //ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();

            //behavior.HttpGetEnabled = true;
            //host.Description.Behaviors.Add(behavior);
            host.AddServiceEndpoint(typeof(lib_wfc.functions), new BasicHttpBinding(), "");


            host.Open();

            Console.WriteLine("Server zapyschen");

            Console.ReadLine();

            host.Close();
        }
    }
}

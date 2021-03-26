using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfServiceLibrary;

namespace ConsoleAppHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a URI to serve as the base address.
            // 创建基址的 URI
            // http://localhost:8000/WcfServiceTest/ 与 WcfServiceLibrary项目的App.config的baseAddress填写的URI的前一部分一致
            Uri baseAddress = new Uri("http://localhost:8000/WcfServiceTest/");

            // Step 2: Create a ServiceHost instance.
            // 创建用于承载服务的类实例
            ServiceHost selfHost = new ServiceHost(typeof(CalculatorService), baseAddress);

            try
            {
                // Step 3: Add a service endpoint.
                // 创建服务终结点， CalculatorService是WcfServiceLibrary项目的方法实现的类
                // 呼应WcfServiceLibrary项目的App.config的baseAddress
                selfHost.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "CalculatorService");

                // Step 4: Enable metadata exchange.
                // 启用元数据交换
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5: Start the service.
                // 打开服务主机以侦听传入消息
                selfHost.Open();
                Console.WriteLine("The service is ready.");

                // Close the ServiceHost to stop the service.
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}

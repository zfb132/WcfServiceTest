using System.ServiceModel;

// IService1.cs 文件包含服务协定的默认定义
namespace WcfServiceLibrary
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    /// <summary>
    /// 本协定定义了一个在线计算器
    /// <para>请注意， ICalculator 接口用特性标记 ServiceContractAttribute （简化为 ServiceContract ）</para>
    /// <para>此属性定义一个命名空间以消除协定名称的歧义</para>
    /// <para>此代码用属性标记每个计算器操作 OperationContractAttribute （简化为 OperationContract ）</para>
    /// </summary>
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double n1, double n2);
        [OperationContract]
        double Subtract(double n1, double n2);
        [OperationContract]
        double Multiply(double n1, double n2);
        [OperationContract]
        double Divide(double n1, double n2);
    }
}

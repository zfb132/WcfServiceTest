## 教程
[官方中文教程](https://docs.microsoft.com/zh-cn/dotnet/framework/wcf/getting-started-tutorial)  

按顺序介绍  
* 整个解决方案名称为`WcfServiceTest`  
* 服务协定的项目名称为`WcfServiceLibrary`  
* 托管服务的控制台应用程序项目名称为`ConsoleAppHost`  
* WCF客户端的控制台应用程序项目名称为`ConsoleAppClient`

### 1. 定义、实现和配置服务协定
在解决方案`WcfServiceTest`中创建名为`WcfServiceLibrary`的`WCF 服务库`项目  
其中  
* `IService1.cs`文件包含服务协定的默认定义
* `Service1.cs`文件包含服务协定的默认实现
* `App.config`文件包含用 VISUAL Studio WCF 服务主机工具加载默认服务所需的配置信息

### 2. 托管和运行基本服务
#### 2.1 创建和配置用于托管服务的控制台应用程序项目  
1. 在解决方案`WcfServiceTest`中创建名为`ConsoleAppHost`的`控制台应用 (.NET Framework)`项目
2. 在`ConsoleAppHost`项目，添加引用
    * 项目 --> 解决方案 --> `WcfServiceLibrary`
    * 程序集 --> 框架 --> `System.ServiceModel`


#### 2.2 添加用于托管服务的代码  
1. 创建类的实例 Uri 以承载服务的基址，基址的格式如下所示：  
`<transport>://<machine-name or domain><:optional port #>/<optional URI segment>`  
计算器服务的基址使用`HTTP`传输、机器名（域名或ip）`localhost`、端口`8000`和`URI`段`WcfServiceTest`
2. 创建用于`ServiceHost`承载服务的类的实例。 构造函数采用两个参数：实现服务协定的类的类型和服务的基址
3. 创建`ServiceEndpoint`实例。服务终结点由地址、绑定和服务协定组成，它的构造函数由服务协定接口类型、绑定和地址组成。服务协定是在服务类型中定义和实现的 `ICalculator`；此示例的绑定为`WSHttpBinding`，它是内置绑定并连接到符合`WS-*`规范的终结点；将地址追加到基址以标识终结点。该代码将地址指定为`CalculatorService`，将终结点的完全限定地址指定为 `http://localhost:8000/WcfServiceTest/CalculatorService/`
4. 启用元数据交换。客户端使用元数据交换生成代理来调用服务操作。若要启用元数据交换，需创建一个`ServiceMetadataBehavior`实例，将其`HttpGetEnabled`属性设置为`true`，然后将该`ServiceMetadataBehavior`对象添加到实例的集合中`Behaviors ServiceHost`
5. 打开`ServiceHost`以侦听传入的消息。应用程序等待用户按`enter`。在应用程序实例化后`ServiceHost`，它将执行`try/catch`块

**注意**：添加`WCF`服务库时，如果你通过启动服务主机进行调试，则`Visual Studio`会为你托管它。若要避免冲突，可以阻止`Visual Studio`承载`WCF`服务库  
* 在解决方案资源管理器中选择`WcfServiceLibrary`项目，然后从快捷菜单中选择`属性`。
* 选择`Wcf 选项`并取消选中`在调试同一解决方案中的另一个项目时启动 WCF 服务主机`

#### 2.3 验证服务是否正常运行  
#### 2.4 服务托管计划步骤  

### 3. 创建WCF客户端
通过从 WCF 服务中检索元数据来创建客户端，使用 Visual Studio 添加服务引用，后者从服务的 MEX 终结点中获取元数据。 然后，Visual Studio 将使用你选择的语言生成客户端代理的托管源代码文件。 它还会创建一个客户端配置文件（App.config）。 此文件使客户端应用程序能够连接到终结点上的服务
#### 3.1 创建并配置 WCF 客户端的控制台应用程序项目
1. 在解决方案`WcfServiceTest`中创建名为`ConsoleAppClient`的`控制台应用 (.NET Framework)`项目
2. 在`ConsoleAppClient`项目，添加引用
    * 程序集 --> 框架 --> `System.ServiceModel`

3. 在`ConsoleAppClient`项目，添加服务引用：点击`发现`，选择`CalculatorService`，保留默认生成的命名空间，选择`确定`

#### 3.2 核对客户端配置文件
`ConsoleAppClient`项目的`App.config`文件，一般不用修改，核对下即可

### 4. 使用WCF客户端
`ConsoleAppClient`项目的`Program.cs`文件的`using ConsoleAppClient.ServiceReference1;`代码的功能  
导入`Visual Studio`生成的带有添加服务引用函数的代码。此代码实例化`WCF 代理`，并调用计算器服务公开的每个服务操作。然后，它会关闭代理并结束程序

### 5. 测试WCF客户端
保存并生成解决方案，然后运行测试，有两种方法：  
* 从 Visual Studio 测试应用程序
    * 选择`WcfServiceLibrary`项目，然后从快捷菜单中选择`设为启动项目`。
    * 从`启动项目`中，从下拉列表中选择`WcfServiceLibrary`，然后选择`运行`或按`F5`

* 在命令提示符下测试应用程序
    * 以管理员身份打开命令提示符，然后导航到解决方案目录
    * 若要启动服务：输入  
    `ConsoleAppHost\bin\Debug\ConsoleAppHost.exe`  
    * 若要启动客户端，则打开另一个命令提示符，导航到解决方案目录，然后输入  
    `ConsoleAppClient\bin\Debug\ConsoleAppClient.exe`

输出示例：  
`ConsoleAppHost.exe`的输出  
```txt
The service is ready.
Press <Enter> to terminate the service.

Received Add(100,15.99)
Return: 115.99
Received Subtract(145,76.54)
Return: 68.46
Received Multiply(9,81.25)
Return: 731.25
Received Divide(22,7)
Return: 3.14285714285714
```
`ConsoleAppClient.exe`的输出  
```txt
Add(100,15.99) = 115.99
Subtract(145,76.54) = 68.46
Multiply(9,81.25) = 731.25
Divide(22,7) = 3.14285714285714

Press <Enter> to terminate the client.
```
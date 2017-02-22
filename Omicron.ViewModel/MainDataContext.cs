using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb;
using BingLibrary.hjb.Intercepts;
using System.ComponentModel.Composition;
using SxjLibrary;
using System.Windows;
using System.Collections.ObjectModel;
using Omicron.Model;


namespace Omicron.ViewModel
{
    [BingAutoNotify]
    public class MainDataContext : DataSource
    {
        public virtual string AboutPageVisibility { set; get; } = "Collapsed";
        public virtual string HomePageVisibility { set; get; } = "Visible";
        public virtual string AxisDebugPageVisibility { set; get; } = "Collapsed";
        public virtual string Msg { set; get; } = "";
        public virtual TwinCATCoil1 ActPos1 { set; get; }
        public virtual TwinCATCoil1 PowerOn1 { set; get; }
        public virtual TwinCATCoil1 Error1 { set; get; }
        public virtual TwinCATCoil1 M2 { set; get; } //轴1励磁
        public virtual TwinCATCoil1 M4 { set; get; } //轴1报警复位
        public virtual TwinCATCoil1 M0 { set; get; } //轴1回原点
        public virtual TwinCATCoil1 Mode1 { set; get; } //轴1点动模式
        public virtual TwinCATCoil1 M16 { set; get; } //轴1Jog+
        public virtual TwinCATCoil1 M17 { set; get; } //轴1Jog-

        public virtual TwinCATCoil1 M20 { set; get; } //轴1运动到点
        public virtual TwinCATCoil1 P1 { set; get; } //轴1运动到点
        public virtual TwinCATCoil1 V1 { set; get; } //轴1运动到点

        public virtual string PowerOnStr1 { set; get; }
        public virtual string ErrorStr1 { set; get; }

        public virtual double Position1 { set; get; }
        public virtual double Position2 { set; get; }
        public virtual double Position3 { set; get; }
        public virtual double Position4 { set; get; }
        public virtual double Position5 { set; get; }

        public virtual double TargetPosition1 { set; get; } = 0;

        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        TwinCATAds _TwinCATAds = new TwinCATAds();

        private string iniPosition = System.Environment.CurrentDirectory + "\\Position.ini";

        #region 构造函数
        public MainDataContext()
        {
            ActPos1 = new TwinCATCoil1(new TwinCATCoil("MAIN.ActPos1", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            PowerOn1 = new TwinCATCoil1(new TwinCATCoil("MAIN.PowerOn", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            Error1 = new TwinCATCoil1(new TwinCATCoil("MAIN.Error1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            M2 = new TwinCATCoil1(new TwinCATCoil("MAIN.M2", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            M4 = new TwinCATCoil1(new TwinCATCoil("MAIN.M4", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            M0 = new TwinCATCoil1(new TwinCATCoil("MAIN.M0", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            Mode1 = new TwinCATCoil1(new TwinCATCoil("MAIN.Mode1", typeof(ushort), TwinCATCoil.Mode.Notice), _TwinCATAds);
            M16 = new TwinCATCoil1(new TwinCATCoil("MAIN.M16", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            M17 = new TwinCATCoil1(new TwinCATCoil("MAIN.M17", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            M20 = new TwinCATCoil1(new TwinCATCoil("MAIN.M20", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            P1 = new TwinCATCoil1(new TwinCATCoil("MAIN.P1", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            V1 = new TwinCATCoil1(new TwinCATCoil("MAIN.V1", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            _TwinCATAds.StartNotice();
            UIUpdate();


            Position1 = double.Parse(Inifile.INIGetStringValue(iniPosition, "Axis1", "Position1", "0"));
            Position2 = double.Parse(Inifile.INIGetStringValue(iniPosition, "Axis1", "Position2", "0"));
            Position3 = double.Parse(Inifile.INIGetStringValue(iniPosition, "Axis1", "Position3", "0"));
            Position4 = double.Parse(Inifile.INIGetStringValue(iniPosition, "Axis1", "Position4", "0"));
            Position5 = double.Parse(Inifile.INIGetStringValue(iniPosition, "Axis1", "Position5", "0"));
            //WaitNet();
            //CommandFromRemote();

        }
        #endregion
        #region UI更新
        public async void UIUpdate()
        {
            while (true)
            {
                Task taskFunc = Task.Run(() =>
                {
                    //Home_Finish_Value = true;
                    //AxisState_ = (Int32)AxisState.Value;
                    //a = (double)ActPos1.Value;
                    try
                    {
                        PowerOnStr1 = (bool)PowerOn1.Value ? "On" : "Off";
                        ErrorStr1 = (bool)Error1.Value ? "Error" : "Normal";
                    }
                    catch (Exception)
                    {

                    }

                });
                
                await Task.Delay(100);
                await taskFunc;
            }
        }
        #endregion
        //private async void WaitNet()
        //{
        //    var b = await tcpIpClient.Connect(ip,2001);
        //    if (b)
        //    {
        //        Msg = messagePrint.AddMessage("已连接服务器");
        //    }
        //    else
        //    {
        //        Msg = messagePrint.AddMessage("连接服务器超时");
        //    }
        //}
        //private async void CommandFromRemote()
        //{
        //    while (true)
        //    {
        //        if (tcpIpClient.tcpConnected)
        //        {
        //            var s = await tcpIpClient.ReceiveAsync();
        //            string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        //            Msg = messagePrint.AddMessage("接收到命令 " + ss[0]);
        //            FuncNum.Value = short.Parse(ss[0]);
        //        }
        //    }
        //}
        public void ChoseHomePage()
        {
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Visible";
            AxisDebugPageVisibility = "Collapsed";
            //Msg = messagePrint.AddMessage("111");          
        }
        public void ChoseAboutPage()
        {
            AboutPageVisibility = "Visible";
            HomePageVisibility = "Collapsed";
            AxisDebugPageVisibility = "Collapsed";
        }
        public void ChooseAxisDebugPage()
        {
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            AxisDebugPageVisibility = "Visible";
        }
        public void FunctionTest()
        {
            double a;
            a = (double)ActPos1.Value;
        }
        public void SevorON()
        {
            M2.Value = !(bool)M2.Value;
        }
        public async void SevorReset()
        {
            M4.Value = false;
            await Task.Delay(200);
            M4.Value = true;
        }
        public async void SevorHomeAction()
        {
            M0.Value = false;
            await Task.Delay(200);
            M0.Value = true;
        }
        public void SevorJogForwardFastStart()
        {
            Mode1.Value = 1;
            M16.Value = true;
        }
        public void SevorJogForwardStop()
        {
            M16.Value = false;
        }
        public void SevorJogOppositeFastStart()
        {
            Mode1.Value = 1;
            M17.Value = true;
        }
        public void SevorJogOppositeStop()
        {
            M17.Value = false;
        }
        public void SevorJogForwardSlowStart()
        {
            Mode1.Value = 0;
            M16.Value = true;
        }
        public void SevorJogOppositeSlowStart()
        {
            Mode1.Value = 0;
            M17.Value = true;
        }

        public void SevorPTP()
        {
            P1.Value = TargetPosition1;
            V1.Value = 450;
            M20.Value = true;
        }

        public void MoveToPosition(object p)
        {
            switch (p.ToString())
            {
                case "1":
                    TargetPosition1 = Position1;
                    SevorPTP();
                    break;
                case "2":
                    TargetPosition1 = Position2;
                    SevorPTP();
                    break;
                case "3":
                    TargetPosition1 = Position3;
                    SevorPTP();
                    break;
                case "4":
                    TargetPosition1 = Position4;
                    SevorPTP();
                    break;
                case "5":
                    TargetPosition1 = Position5;
                    SevorPTP();
                    break;
                default:
                    break;
            }
        }
        public void GetPosition(object p)
        {
            switch (p.ToString())
            {
                case "1":
                    Position1 = (double)ActPos1.Value;
                    Inifile.INIWriteValue(iniPosition, "Axis1", "Position1", Position1.ToString());
                    break;
                case "2":
                    Position2 = (double)ActPos1.Value;
                    Inifile.INIWriteValue(iniPosition, "Axis1", "Position2", Position2.ToString());
                    break;
                case "3":
                    Position3 = (double)ActPos1.Value;
                    Inifile.INIWriteValue(iniPosition, "Axis1", "Position3", Position3.ToString());
                    break;
                case "4":
                    Position4 = (double)ActPos1.Value;
                    Inifile.INIWriteValue(iniPosition, "Axis1", "Position4", Position4.ToString());
                    break;
                case "5":
                    Position5 = (double)ActPos1.Value;
                    Inifile.INIWriteValue(iniPosition, "Axis1", "Position5", Position5.ToString());
                    break;
            }
        }
        //public void StartHomeAction()
        //{
        //    Home_Start.Value = !((bool)Home_Start.Value);
        //}
        //public async void ResetAction()
        //{
        //    Reset_Start.Value = true;
        //    await Task.Delay(100);
        //    Reset_Start.Value = false;
        //}
        //public async void StartMoveAction()
        //{

        //    FuncNum.Value = PositionComboBox[PositionComboBoxSelectedIndex];


        //}

        //[Export(MEF.Contracts.ActionMessage)]
        //[ExportMetadata(MEF.Key, "winclose")]
        //public async void WindowClose()
        //{
        //    mydialog.changeaccent("Red");
        //    var r = await mydialog.showconfirm("确定要关闭程序吗？");
        //    if (r)
        //    {
        //        System.Windows.Application.Current.Shutdown();
        //    }
        //    else
        //    {
        //        mydialog.changeaccent("Cobalt");
        //    }
        //}
    }
}
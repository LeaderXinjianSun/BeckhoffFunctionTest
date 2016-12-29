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
        public virtual string Msg { set; get; } = "";
        public virtual TwinCATCoil1 Home_Start { set; get; }
        public virtual TwinCATCoil1 Home_Finish { set; get; }
        public virtual TwinCATCoil1 Move_Finish { set; get; }
        public virtual TwinCATCoil1 FuncNum { set; get; }
        public virtual TwinCATCoil1 BeckhoffConnect { set; get; }
        public virtual TwinCATCoil1 X_Position { set; get; }
        public virtual TwinCATCoil1 Axis1_Error { set; get; }
        public virtual TwinCATCoil1 Reset_Start { set; get; }
        public virtual TwinCATCoil1 Move_Start { set; get; }
        public virtual short[] PositionComboBox { set; get; } = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public virtual int PositionComboBoxSelectedIndex { set; get; } = 0;
        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        TwinCATAds _TwinCATAds = new TwinCATAds();
        #region 构造函数
        public MainDataContext()
        {
            Home_Start = new TwinCATCoil1(new TwinCATCoil("MAIN.Home_Start", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            Home_Finish = new TwinCATCoil1(new TwinCATCoil("MAIN.Home_Finish", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            Move_Finish = new TwinCATCoil1(new TwinCATCoil("MAIN.Move_Finish", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FuncNum = new TwinCATCoil1(new TwinCATCoil("MAIN.FuncNum", typeof(short), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BeckhoffConnect = new TwinCATCoil1(new TwinCATCoil("MAIN.BeckhoffConnect", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            X_Position = new TwinCATCoil1(new TwinCATCoil("MAIN.X_Position", typeof(float), TwinCATCoil.Mode.Notice), _TwinCATAds);
            Axis1_Error = new TwinCATCoil1(new TwinCATCoil("MAIN.Axis1_Error", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            Reset_Start = new TwinCATCoil1(new TwinCATCoil("MAIN.Reset_Start", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            Move_Start = new TwinCATCoil1(new TwinCATCoil("MAIN.Move_Start", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            _TwinCATAds.StartNotice();
            //UIUpdate();
            
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
                });
                
                await Task.Delay(100);
                await taskFunc;
            }
        }
        #endregion
        public void ChoseHomePage()
        {
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Visible";
            //Msg = messagePrint.AddMessage("111");
            
        }
        public void ChoseAboutPage()
        {
            AboutPageVisibility = "Visible";
            HomePageVisibility = "Collapsed";
        }
        public void StartHomeAction()
        {
            Home_Start.Value = !((bool)Home_Start.Value);
        }
        public async void ResetAction()
        {
            Reset_Start.Value = true;
            await Task.Delay(100);
            Reset_Start.Value = false;
        }
        public async void StartMoveAction()
        {

            FuncNum.Value = PositionComboBox[PositionComboBoxSelectedIndex];
   
            
        }

        [Export(MEF.Contracts.ActionMessage)]
        [ExportMetadata(MEF.Key, "winclose")]
        public async void WindowClose()
        {
            mydialog.changeaccent("Red");
            var r = await mydialog.showconfirm("确定要关闭程序吗？");
            if (r)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                mydialog.changeaccent("Cobalt");
            }
        }
    }
}
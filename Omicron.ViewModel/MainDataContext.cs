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
        public virtual bool Home_Finish_Value { set; get; } = false;
        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        TwinCATAds _TwinCATAds = new TwinCATAds();
        #region 构造函数
        public MainDataContext()
        {
            Home_Start = new TwinCATCoil1(new TwinCATCoil("MAIN.Home_Start", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            Home_Finish = new TwinCATCoil1(new TwinCATCoil("MAIN.Home_Finish", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            UIUpdate();
            Console.WriteLine("123");
        }
        #endregion
        #region UI更新
        public async void UIUpdate()
        {
            while (true)
            {
                Task taskFunc = Task.Run(() =>
                {
                    Home_Finish_Value = true;
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
            Home_Start.Value = true;
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
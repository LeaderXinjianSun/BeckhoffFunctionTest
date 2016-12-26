using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Omicron.View
{
    /// <summary>
    /// SteelTestUnitControl.xaml 的交互逻辑
    /// </summary>
    public partial class SteelTestUnitControl : UserControl
    {
        public SteelTestUnitControl()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ResultNumProperty =
            DependencyProperty.Register("ResultNum", typeof(int), typeof(SteelTestUnitControl), new PropertyMetadata(
                new PropertyChangedCallback((d, e) =>
                {
                    var steelTestUnitControl = d as SteelTestUnitControl;
                    var result = (int)(e.NewValue);
                    switch (result)
                    {
                        case 3:
                            steelTestUnitControl.ResultDisplay.Background = Brushes.LightGray;
                            steelTestUnitControl.ResultDisplay.Content = "Unknow";
                            break;
                        case 0:
                            steelTestUnitControl.ResultDisplay.Background = Brushes.Green;
                            steelTestUnitControl.ResultDisplay.Content = "OK";
                            break;
                        case 1:
                            steelTestUnitControl.ResultDisplay.Background = Brushes.Red;

                            steelTestUnitControl.ResultDisplay.Content = "NG";
                            break;
                        case 2:
                            steelTestUnitControl.ResultDisplay.Background = Brushes.Yellow;
                            steelTestUnitControl.ResultDisplay.Content = "Testing";
                            break;
                        default:
                            break;
                    }
                }
                  )
                ));

        public int ResultNum
        {
            get { return (int)GetValue(ResultNumProperty); }
            set { SetValue(ResultNumProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemNumProperty =
            DependencyProperty.Register("SelectedItemNum", typeof(int), typeof(SteelTestUnitControl), new PropertyMetadata(
                new PropertyChangedCallback((d, e) =>
                {
                    var steelTestUnitControl = d as SteelTestUnitControl;
                    var result = (int)(e.NewValue);
                    steelTestUnitControl.StackPanel1.Visibility = Visibility.Collapsed;
                    steelTestUnitControl.StackPanel2.Visibility = Visibility.Collapsed;
                    steelTestUnitControl.StackPanel3.Visibility = Visibility.Collapsed;
                    steelTestUnitControl.StackPanel4.Visibility = Visibility.Collapsed;
                    switch (result)
                    {
                        case 1:
                            steelTestUnitControl.StackPanel1.Visibility = Visibility.Visible;
                            break;
                        case 2:
                            steelTestUnitControl.StackPanel1.Visibility = Visibility.Visible;
                            steelTestUnitControl.StackPanel2.Visibility = Visibility.Visible;
                            break;
                        case 3:
                            steelTestUnitControl.StackPanel1.Visibility = Visibility.Visible;
                            steelTestUnitControl.StackPanel2.Visibility = Visibility.Visible;
                            steelTestUnitControl.StackPanel3.Visibility = Visibility.Visible;
                            break;
                        case 4:
                            steelTestUnitControl.StackPanel1.Visibility = Visibility.Visible;
                            steelTestUnitControl.StackPanel2.Visibility = Visibility.Visible;
                            steelTestUnitControl.StackPanel3.Visibility = Visibility.Visible;
                            steelTestUnitControl.StackPanel4.Visibility = Visibility.Visible;
                            break;
                        default:
                            break;
                    }
                }
                  )
                ));
        public int SelectedItemNum
        {
            get { return (int)GetValue(SelectedItemNumProperty); }
            set { SetValue(SelectedItemNumProperty, value); }
        }
    }
}

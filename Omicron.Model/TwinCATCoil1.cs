using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omicron.Model
{
    public class TwinCATCoil1
    {
        private TwinCATCoil _TwinCATCoil;
        readonly TwinCATAds _TwinCATAds;
        public event EventHandler CoilChanged;
        public TwinCATCoil1(TwinCATCoil mTwinCATCoil, TwinCATAds mTwinCATAds)
        {
            _TwinCATAds = mTwinCATAds;
            if (mTwinCATCoil != null)
            {
                _TwinCATCoil = mTwinCATCoil;
                _TwinCATCoil.CoilChanged += _TwinCATCoil_CoilChanged;
                _TwinCATAds.AddCoil(_TwinCATCoil);
            }
        }

        private void _TwinCATCoil_CoilChanged(object sender, EventArgs e)
        {
            // setProperty("TwinCATCoilViewModel",Value);
            //this.NotifyPropertyChanged("Value");
            if (CoilChanged != null)
                CoilChanged(this, null);
        }
        public object Value
        {
            get { return _TwinCATCoil.CoilValue; }
            set
            {
                _TwinCATCoil.UpdateCoilValue(value);
            }
        }
        /// <summary>
        /// 写入数组值
        /// </summary>
        public void Update()
        {
            _TwinCATCoil.UpdateCoilValueEx();
        }
    }
}

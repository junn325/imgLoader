using System;
using System.Threading;

namespace imgLoader_WPF
{
    internal class IndexItem
    {
        internal delegate void NoParam();
        internal delegate void IntOne(int value);
        //internal delegate void DblOne(double value);
        internal delegate void VisOne(System.Windows.Visibility value);

        internal Processor Proc;

        internal string Title { get; set; }
        internal string Author { get; set; }
        internal string SiteName { get; set; }
        internal int ImgCount { get; set; }
        internal string Number { get; set; }
        internal string[] Tags { get; set; }
        public int Vote { get; set; }
        internal int View { get; set; }
        internal bool IsRead { get; set; }
        internal string Route { get; set; }

        internal DateTime Date;

        //internal Thread ThrLoad;

        //internal bool Selected = false;
        internal bool Show = true;
        internal bool IsDownloading;
        internal bool IsError;

        internal NoParam RefreshInfo;
        internal NoParam ShownChang;

        internal VisOne ProgPanelVis;
        //internal VisOne TagPanelVis;

        internal IntOne ProgBarMax;
        internal NoParam ProgBarVal;

        //internal DblOne SizeChange;
    }
}
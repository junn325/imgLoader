using System;
using System.Threading;

namespace imgLoader_WPF
{
    internal class IndexItem
    {
        public delegate void NoParam();
        public delegate void IntOne(int value);
        //public delegate void DblOne(double value);
        public delegate void VisOne(System.Windows.Visibility value);

        public Processor Proc;

        public string Title { get; set; }
        public string Author { get; set; }
        public string SiteName { get; set; }
        public int ImgCount { get; set; }
        public string Number { get; set; }
        public string[] Tags { get; set; }
        public int Vote { get; set; }
        public int View { get; set; }
        public bool IsRead { get; set; }
        public string Route { get; set; }

        public DateTime Date;

        public Thread ThrLoad;

        //public bool Selected = false;
        public bool Show = true;
        public bool IsDownloading;
        public bool IsError = false;

        public NoParam RefreshInfo;

        public NoParam ShownChang;

        public VisOne ProgPanelVis;
        //public VisOne TagPanelVis;

        public IntOne ProgBarMax;
        public NoParam ProgBarVal;

        //public DblOne SizeChange;
    }
}
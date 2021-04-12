using System;

namespace imgLoader_WPF
{
    internal class IndexItem
    {
        internal delegate void NoParam();
        internal delegate void IntOne(int value);
        //internal delegate void DblOne(double value);
        internal delegate void VisOne(System.Windows.Visibility value);

        internal Processor Proc;

        internal int ImgCount, Vote, View;
        internal string Title, Author, SiteName, Number, Route;
        internal string[] Tags;
        internal DateTime Date, LastViewDate;

        //internal bool Selected = false;
        internal bool Show = true;
        internal bool IsRead, IsDownloading, IsError, IsSeparator;

        internal NoParam RefreshInfo, ShownChang;

        internal VisOne ProgPanelVis;

        internal IntOne ProgBarMax, ProgBarVal;

        //todo: 델리게이트들을 Core로 옮길 수 있는가?
    }
}
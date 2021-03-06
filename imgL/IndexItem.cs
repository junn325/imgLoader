﻿using System;

namespace imgL
{
    internal class IndexItem
    {
        internal delegate void NoParam();
        internal delegate void IntOne(int value);
        internal delegate void VisOne(System.Windows.Visibility value);

        internal Processor Proc;

        internal int ImgCount, Vote, View;

        internal string Title, Author, SiteName, Number, Route;
        internal string[] Tags;

        internal DateTime Date, LastViewDate;
        internal bool Show = true;
        internal bool IsRead, IsDownloading, IsError, IsSeparator, IsCntValid;

        internal NoParam RefreshInfo, ShownChang;
        internal VisOne ProgPanelVis;
        internal IntOne ProgBarMax, ProgBarVal;
    }
}
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

namespace imgLoader_WPF.Tag
{
    public partial class TagItem
    {
        public string TagName
        {
            get => Tag.Content.ToString();
            set => Tag.Content = value;
        }

        public SColor Sex
        {
            get => this.Background == (Brush)new BrushConverter().ConvertFrom("#E86441") ? SColor.Female : SColor.Male;
            set => this.Background = (value == SColor.Female ? (Brush)new BrushConverter().ConvertFrom("#E86441") : (Brush)new BrushConverter().ConvertFrom("#00A2FF"));
        }

        public enum SColor
        {
            Female,
            Male
        }
        public TagItem()
        {
            InitializeComponent();
        }
    }
}

using System.Windows.Media;

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
            get =>
                Background == (Brush)new BrushConverter().ConvertFrom("#E86441")
                    ? SColor.Female
                    : Background == (Brush)new BrushConverter().ConvertFrom("#00A2FF")
                        ? SColor.Male
                        : SColor.None;

            set =>
                Background =
                    value switch
                    {
                        SColor.Female => (Brush)new BrushConverter().ConvertFrom("#E86441"),
                        SColor.Male => (Brush)new BrushConverter().ConvertFrom("#00A2FF"),
                        _ => (Brush)new BrushConverter().ConvertFrom("#838587")

                    };
        }

        public enum SColor
        {
            Female,
            Male,
            None,
        }

        public TagItem()
        {
            InitializeComponent();
        }
    }
}

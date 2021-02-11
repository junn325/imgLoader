using System.Collections.ObjectModel;
using System.Windows;

namespace imgLoader_WPF.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public string[] Dd { get; set; }
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dd = new string[]{ "d", "dd", "ddd", "dddd", "ddddd"};

            List.DataContext = new BaseViewModel();
        }

        public class BaseViewModel
        {
            public ObservableCollection<BaseViewModelItem> ModelItems { get; set; }
            public string Key { get; set; }
            public BaseViewModel()
            {
                Key = "BingMapKeyFromCode";
                ModelItems = new ObservableCollection<BaseViewModelItem>();
                ModelItems.Add(new BaseViewModelItem() { Name = "Name 1" });
                ModelItems.Add(new BaseViewModelItem() { Name = "Name 2" });
                ModelItems.Add(new BaseViewModelItem() { Name = "Name 3" });
            }
        }
        public class BaseViewModelItem
        {
            public string Name { get; set; }
        }
    }
}

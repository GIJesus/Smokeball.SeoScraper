using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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

namespace Smokeball___Development_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel m_mainViewModel;
        public MainWindow()
        {
            m_mainViewModel = new MainViewModel();
            this.DataContext = m_mainViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m_mainViewModel.ExecuteGoogleQuery();
            this.WebBrowser.Navigate(new Uri(m_mainViewModel.QueryUrl));
        }
    }
}
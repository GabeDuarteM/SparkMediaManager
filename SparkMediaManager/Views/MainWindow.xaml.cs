// Developed by: Gabriel Duarte
// 
// Created at: 08/05/2016 19:21

using SparkMediaManager.ViewModels;

namespace SparkMediaManager.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ((MainViewModel) DataContext).ObjWindow = this;
        }
    }
}

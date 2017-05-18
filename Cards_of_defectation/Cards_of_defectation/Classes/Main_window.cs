using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using WPF.MDI;
using System.Windows.Controls;

namespace Cards_of_defectation.Classes
{
    class Main_window
    {
        static MdiContainer main_window;
        static Main_window exz;
        Main_window(MdiContainer main)
        {
            main_window = main;
        }
        public static Main_window Init()
        {
            return exz;
        }
        public static Main_window Init(MdiContainer main)
        {
            exz = new Main_window(main);
            return exz;
        }
        public void AddWindow(string title,UIElement window)
        {
            foreach (MdiChild child in main_window.Children)
            {
                if (child.WindowState == WindowState.Maximized)
                {
                    child.WindowState = WindowState.Normal;
                    child.Width = main_window.ActualWidth;
                    child.Height = main_window.ActualHeight;
                }       
            }
            bool res = true;
            if ((window as UserControl).ToolTip?.ToString() == "1")
            {
                res = false;
                (window as UserControl).ToolTip = null;
            }
            main_window.Children.Add(new MdiChild()
            {
                Title = title,
                Content = window,
                Width = (window as UserControl).Width,
                Height = (window as UserControl).Height,
                Resizable = res
            });
            if (main_window.ActualHeight == 0&&res) main_window.Children.Last().WindowState = WindowState.Maximized;
            main_window.Children.Last().Closing += new RoutedEventHandler(Close_Click);
        }
        void Close_Click(object sender,RoutedEventArgs e)
        {
            if ((sender as MdiChild).Content is MainOUP)
                ((sender as MdiChild).Content as MainOUP).Window_Closing(e as WPF.MDI.Event.ClosingEventArgs);
            if ((sender as MdiChild).Content is Defect_map)
                ((sender as MdiChild).Content as Defect_map).Window_Closing(e as WPF.MDI.Event.ClosingEventArgs);
            if ((sender as MdiChild).Content is FirstLevel)
                ((sender as MdiChild).Content as FirstLevel).Window_Closing(e as WPF.MDI.Event.ClosingEventArgs);
            if ((sender as MdiChild).Content is ReductionReference)
                ((sender as MdiChild).Content as ReductionReference).Window_Closing(e as WPF.MDI.Event.ClosingEventArgs);
            if ((sender as MdiChild).Content is ShopAlert)
                ((sender as MdiChild).Content as ShopAlert).Window_Closing(e as WPF.MDI.Event.ClosingEventArgs);
        }
    }
}

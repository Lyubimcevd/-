﻿#pragma checksum "..\..\..\Windows\Defect_map.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8E0D40E01D83B99F2253FAB9A06625CA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Cards_of_defectation.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Cards_of_defectation.Windows {
    
    
    /// <summary>
    /// Defect_map
    /// </summary>
    public partial class Defect_map : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 28 "..\..\..\Windows\Defect_map.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menu;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\Defect_map.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid cap;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\Windows\Defect_map.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid main_table;
        
        #line default
        #line hidden
        
        
        #line 267 "..\..\..\Windows\Defect_map.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabControl;
        
        #line default
        #line hidden
        
        
        #line 269 "..\..\..\Windows\Defect_map.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox_sostav;
        
        #line default
        #line hidden
        
        
        #line 295 "..\..\..\Windows\Defect_map.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox arm_search;
        
        #line default
        #line hidden
        
        
        #line 300 "..\..\..\Windows\Defect_map.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox_arm;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Cards_of_defectation;component/windows/defect_map.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\Defect_map.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\Windows\Defect_map.xaml"
            ((Cards_of_defectation.Windows.Defect_map)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.menu = ((System.Windows.Controls.Menu)(target));
            return;
            case 3:
            this.cap = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.main_table = ((System.Windows.Controls.DataGrid)(target));
            
            #line 98 "..\..\..\Windows\Defect_map.xaml"
            this.main_table.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.main_table_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 11:
            this.tabControl = ((System.Windows.Controls.TabControl)(target));
            return;
            case 12:
            this.listBox_sostav = ((System.Windows.Controls.ListBox)(target));
            
            #line 270 "..\..\..\Windows\Defect_map.xaml"
            this.listBox_sostav.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.listBox_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 13:
            this.arm_search = ((System.Windows.Controls.ComboBox)(target));
            
            #line 298 "..\..\..\Windows\Defect_map.xaml"
            this.arm_search.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.ComboBox_TextChanged));
            
            #line default
            #line hidden
            
            #line 299 "..\..\..\Windows\Defect_map.xaml"
            this.arm_search.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.arm_search_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.listBox_arm = ((System.Windows.Controls.ListBox)(target));
            
            #line 303 "..\..\..\Windows\Defect_map.xaml"
            this.listBox_arm.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.listBox_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 141 "..\..\..\Windows\Defect_map.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 167 "..\..\..\Windows\Defect_map.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged_1);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 193 "..\..\..\Windows\Defect_map.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged_2);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 219 "..\..\..\Windows\Defect_map.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged_3);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 240 "..\..\..\Windows\Defect_map.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged_4);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 259 "..\..\..\Windows\Defect_map.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged_5);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}


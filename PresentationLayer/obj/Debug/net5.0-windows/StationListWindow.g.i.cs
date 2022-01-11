﻿#pragma checksum "..\..\..\StationListWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1A2C7D17EF60383CB944467F5C457A958CA8EF91"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace PL {
    
    
    /// <summary>
    /// StationListWindow
    /// </summary>
    public partial class StationListWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\StationListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\StationListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UpGrid;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\StationListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGroupBySlotsNumber;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\StationListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGroupByFreeSlots;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\StationListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRefresh;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\StationListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddStation;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\StationListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\StationListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstvStations;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/stationlistwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StationListWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\StationListWindow.xaml"
            ((PL.StationListWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.UpGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.btnGroupBySlotsNumber = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\StationListWindow.xaml"
            this.btnGroupBySlotsNumber.Click += new System.Windows.RoutedEventHandler(this.btnGroupBySlotsNumber_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnGroupByFreeSlots = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\StationListWindow.xaml"
            this.btnGroupByFreeSlots.Click += new System.Windows.RoutedEventHandler(this.btnGroupByFreeSlots_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnRefresh = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\StationListWindow.xaml"
            this.btnRefresh.Click += new System.Windows.RoutedEventHandler(this.btnRefresh_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnAddStation = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\StationListWindow.xaml"
            this.btnAddStation.Click += new System.Windows.RoutedEventHandler(this.btnAddStation_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\StationListWindow.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lstvStations = ((System.Windows.Controls.ListBox)(target));
            
            #line 35 "..\..\..\StationListWindow.xaml"
            this.lstvStations.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstvStations_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


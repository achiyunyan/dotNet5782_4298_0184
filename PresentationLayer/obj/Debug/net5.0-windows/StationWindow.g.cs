﻿#pragma checksum "..\..\..\StationWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59FF5B4A15CCC2A5092842FE21F0D75AC1D626D3"
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
    /// StationWindow
    /// </summary>
    public partial class StationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid StationActions;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DronesTag;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanelDrone;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StationName;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameExeption;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StationSlots;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SlotsExeption;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstvDrones;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StationUpdate;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AddStation;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Id;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox idExeption;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FreeSlots;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox latitude;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox longitude;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\StationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addStationBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/PL;component/stationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StationWindow.xaml"
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
            
            #line 11 "..\..\..\StationWindow.xaml"
            ((PL.StationWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.StationActions = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 32 "..\..\..\StationWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBackToList_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DronesTag = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.StackPanelDrone = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.StationName = ((System.Windows.Controls.TextBox)(target));
            
            #line 47 "..\..\..\StationWindow.xaml"
            this.StationName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.StationName_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NameExeption = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.StationSlots = ((System.Windows.Controls.TextBox)(target));
            
            #line 55 "..\..\..\StationWindow.xaml"
            this.StationSlots.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.StationSlots_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SlotsExeption = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.lstvDrones = ((System.Windows.Controls.ListBox)(target));
            
            #line 59 "..\..\..\StationWindow.xaml"
            this.lstvDrones.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstvDrones_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.StationUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\StationWindow.xaml"
            this.StationUpdate.Click += new System.Windows.RoutedEventHandler(this.StationUpdate_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.AddStation = ((System.Windows.Controls.Grid)(target));
            return;
            case 13:
            this.Id = ((System.Windows.Controls.TextBox)(target));
            
            #line 111 "..\..\..\StationWindow.xaml"
            this.Id.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Id_TextChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.idExeption = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            
            #line 113 "..\..\..\StationWindow.xaml"
            this.Name.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Name_TextChanged);
            
            #line default
            #line hidden
            return;
            case 16:
            this.FreeSlots = ((System.Windows.Controls.TextBox)(target));
            
            #line 114 "..\..\..\StationWindow.xaml"
            this.FreeSlots.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FreeSlots_TextChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            this.latitude = ((System.Windows.Controls.TextBox)(target));
            
            #line 115 "..\..\..\StationWindow.xaml"
            this.latitude.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.latitude_TextChanged);
            
            #line default
            #line hidden
            return;
            case 18:
            this.longitude = ((System.Windows.Controls.TextBox)(target));
            
            #line 116 "..\..\..\StationWindow.xaml"
            this.longitude.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.longitude_TextChanged);
            
            #line default
            #line hidden
            return;
            case 19:
            this.addStationBtn = ((System.Windows.Controls.Button)(target));
            
            #line 123 "..\..\..\StationWindow.xaml"
            this.addStationBtn.Click += new System.Windows.RoutedEventHandler(this.addStationBtn_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 129 "..\..\..\StationWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBackToList_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\DronesListWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F78836123F6B01D5CD7F5E95DB273C2166977158"
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
    /// DronesListWindow
    /// </summary>
    public partial class DronesListWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UpGrid;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTemp;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox btnAddDrone;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboStatus;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboMaxWeight;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddDrones;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstvDrones;
        
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
            System.Uri resourceLocater = new System.Uri("/PL;V1.0.0.0;component/droneslistwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DronesListWindow.xaml"
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
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.UpGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.txtTemp = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnAddDrone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.comboStatus = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\DronesListWindow.xaml"
            this.comboStatus.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboStatus_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.comboMaxWeight = ((System.Windows.Controls.ComboBox)(target));
            
            #line 35 "..\..\..\DronesListWindow.xaml"
            this.comboMaxWeight.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboMaxWeight_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\DronesListWindow.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnAddDrones = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\DronesListWindow.xaml"
            this.btnAddDrones.Click += new System.Windows.RoutedEventHandler(this.btnAddDrones_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lstvDrones = ((System.Windows.Controls.ListView)(target));
            
            #line 42 "..\..\..\DronesListWindow.xaml"
            this.lstvDrones.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.openActionsWindow);
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\DronesListWindow.xaml"
            this.lstvDrones.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lstvDrones_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


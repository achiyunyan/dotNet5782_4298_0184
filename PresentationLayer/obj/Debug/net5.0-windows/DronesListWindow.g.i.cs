﻿#pragma checksum "..\..\..\DronesListWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BD31BFB8960F839FCB8965B2FC5ADD6F4A5A96EC"
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
        
        
        #line 13 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UpGrid;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox statusTxt;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboStatus;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox maxWeightTxt;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboMaxWeight;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGroupByState;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddDrones;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\DronesListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstvDrones;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/droneslistwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DronesListWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\DronesListWindow.xaml"
            ((PL.DronesListWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
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
            this.statusTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.comboStatus = ((System.Windows.Controls.ComboBox)(target));
            
            #line 33 "..\..\..\DronesListWindow.xaml"
            this.comboStatus.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboStatus_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.maxWeightTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.comboMaxWeight = ((System.Windows.Controls.ComboBox)(target));
            
            #line 37 "..\..\..\DronesListWindow.xaml"
            this.comboMaxWeight.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboMaxWeight_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\DronesListWindow.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnGroupByState = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\DronesListWindow.xaml"
            this.btnGroupByState.Click += new System.Windows.RoutedEventHandler(this.btnGroupByState_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnAddDrones = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\DronesListWindow.xaml"
            this.btnAddDrones.Click += new System.Windows.RoutedEventHandler(this.btnAddDrones_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.lstvDrones = ((System.Windows.Controls.ListBox)(target));
            
            #line 44 "..\..\..\DronesListWindow.xaml"
            this.lstvDrones.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.openActionsWindow);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


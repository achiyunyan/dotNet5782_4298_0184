﻿#pragma checksum "..\..\..\AddDroneWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48D4D54A798387DCA7FFEE93C42B93565BE87265"
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
    /// AddDroneWindow
    /// </summary>
    public partial class AddDroneWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\AddDroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Id;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\AddDroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Model;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\AddDroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboMaxWeight;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\AddDroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboInitialStation;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\AddDroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveCanges;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\AddDroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBackToList;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\AddDroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox idExeption;
        
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
            System.Uri resourceLocater = new System.Uri("/PL;component/adddronewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddDroneWindow.xaml"
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
            this.Id = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\..\AddDroneWindow.xaml"
            this.Id.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Id_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Model = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\..\AddDroneWindow.xaml"
            this.Model.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Model_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.comboMaxWeight = ((System.Windows.Controls.ComboBox)(target));
            
            #line 17 "..\..\..\AddDroneWindow.xaml"
            this.comboMaxWeight.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboMaxWeight_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.comboInitialStation = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\..\AddDroneWindow.xaml"
            this.comboInitialStation.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboInitialStation_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnSaveCanges = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\AddDroneWindow.xaml"
            this.btnSaveCanges.Click += new System.Windows.RoutedEventHandler(this.btnSaveCanges_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnBackToList = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\AddDroneWindow.xaml"
            this.btnBackToList.Click += new System.Windows.RoutedEventHandler(this.btnBackToList_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.idExeption = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


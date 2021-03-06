﻿#pragma checksum "..\..\FlightsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "29828F6B287911C32928C2D2932D2D53CAD7EDC7B1884C11F5A137A3E0F0383C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AirportBooking;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace AirportBooking {
    
    
    /// <summary>
    /// FlightsPage
    /// </summary>
    public partial class FlightsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ArrivingBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DepartingBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateControl;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView FlightsList;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancel;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button update;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delete;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button create;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox hoursBox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\FlightsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox minutesBox;
        
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
            System.Uri resourceLocater = new System.Uri("/AirportBooking;component/flightspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FlightsPage.xaml"
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
            this.ArrivingBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 20 "..\..\FlightsPage.xaml"
            this.ArrivingBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ArrivingBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DepartingBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 23 "..\..\FlightsPage.xaml"
            this.DepartingBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dateControl = ((System.Windows.Controls.DatePicker)(target));
            
            #line 24 "..\..\FlightsPage.xaml"
            this.dateControl.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dateControl_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.FlightsList = ((System.Windows.Controls.ListView)(target));
            
            #line 30 "..\..\FlightsPage.xaml"
            this.FlightsList.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.FlightsList_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cancel = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\FlightsPage.xaml"
            this.cancel.Click += new System.Windows.RoutedEventHandler(this.cancel_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.update = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\FlightsPage.xaml"
            this.update.Click += new System.Windows.RoutedEventHandler(this.Update_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.delete = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\FlightsPage.xaml"
            this.delete.Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.create = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\FlightsPage.xaml"
            this.create.Click += new System.Windows.RoutedEventHandler(this.Create_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.hoursBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\FlightsPage.xaml"
            this.hoursBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.hoursBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.minutesBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\FlightsPage.xaml"
            this.minutesBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.minutesBox_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\Views\SessionView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E459F5AC08A574D7AC9D6CEF789CA8166FE532FB3C52AB24B0B19AD7EACB8024"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LiveCharts.Wpf;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using System.Windows.Interactivity;
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


namespace BrewUI.Views {
    
    
    /// <summary>
    /// SessionView
    /// </summary>
    public partial class SessionView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SessionName;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BrewMethod;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BeerStyle;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MainTimerText;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TimeLeftText;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartMash;
        
        #line default
        #line hidden
        
        
        #line 480 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartBoil;
        
        #line default
        #line hidden
        
        
        #line 571 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CDTargetTemp;
        
        #line default
        #line hidden
        
        
        #line 573 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartCooldown;
        
        #line default
        #line hidden
        
        
        #line 613 "..\..\..\Views\SessionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackButton;
        
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
            System.Uri resourceLocater = new System.Uri("/BrewUI;component/views/sessionview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\SessionView.xaml"
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
            this.SessionName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.BrewMethod = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.BeerStyle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.MainTimerText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.TimeLeftText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.StartMash = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.StartBoil = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.CDTargetTemp = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.StartCooldown = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.BackButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


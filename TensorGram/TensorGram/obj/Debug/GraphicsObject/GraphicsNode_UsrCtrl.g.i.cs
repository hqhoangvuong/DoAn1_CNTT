﻿#pragma checksum "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CB25F56F47AA78F7D18F2CD16A16C6CA35519BDA26D333874DD48A2CF6C8A16A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TensorGram.GraphicsObject;


namespace TensorGram.GraphicsObject {
    
    
    /// <summary>
    /// GraphicsNode_UsrCtrl
    /// </summary>
    public partial class GraphicsNode_UsrCtrl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TitleGrid;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border TitleZone;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtblockTitle;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AttributeGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border AttributesZone;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtblockPropety;
        
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
            System.Uri resourceLocater = new System.Uri("/TensorGram;component/graphicsobject/graphicsnode_usrctrl.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
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
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.TitleGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 13 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
            this.TitleGrid.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Grid_MouseEnter);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\GraphicsObject\GraphicsNode_UsrCtrl.xaml"
            this.TitleGrid.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Grid_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TitleZone = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.txtblockTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.AttributeGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.AttributesZone = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.txtblockPropety = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


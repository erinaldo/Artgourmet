﻿#pragma checksum "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D0FC8C0EF56CFC74DFA7678E8B17516"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Controls.Carousel;
using Telerik.Windows.Controls.Charting;
using Telerik.Windows.Controls.Data.PropertyGrid;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Primitives;
using Telerik.Windows.Controls.RibbonBar;
using Telerik.Windows.Controls.TransitionEffects;
using Telerik.Windows.Controls.TreeListView;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.Data;
using Telerik.Windows.DragDrop;
using Telerik.Windows.Shapes;


namespace Artebit.Restaurante.Caixa.Cadastro {
    
    
    /// <summary>
    /// FormCat2
    /// </summary>
    public partial class FormCat2 : Telerik.Windows.Controls.RadWindow, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 27 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbCategoria;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadGridView gridDados;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadButton btAdd;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadButton btExcluir;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadButton btnSalvar;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadButton btnCancelar;
        
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
            System.Uri resourceLocater = new System.Uri("/Artebit.Restaurante.Caixa;component/cadastro/formularios/formcat2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
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
            this.lbCategoria = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.gridDados = ((Telerik.Windows.Controls.RadGridView)(target));
            
            #line 28 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
            this.gridDados.RowEditEnded += new System.EventHandler<Telerik.Windows.Controls.GridViewRowEditEndedEventArgs>(this.gridDados_RowEditEnded);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
            this.gridDados.CellEditEnded += new System.EventHandler<Telerik.Windows.Controls.GridViewCellEditEndedEventArgs>(this.gridDados_CellEditEnded);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
            this.gridDados.RowActivated += new System.EventHandler<Telerik.Windows.Controls.GridView.RowEventArgs>(this.gridDados_RowActivated);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btAdd = ((Telerik.Windows.Controls.RadButton)(target));
            
            #line 48 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
            this.btAdd.Click += new System.Windows.RoutedEventHandler(this.btAdd_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btExcluir = ((Telerik.Windows.Controls.RadButton)(target));
            
            #line 53 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
            this.btExcluir.Click += new System.Windows.RoutedEventHandler(this.btExcluir_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnSalvar = ((Telerik.Windows.Controls.RadButton)(target));
            
            #line 61 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
            this.btnSalvar.Click += new System.Windows.RoutedEventHandler(this.btnSalvar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnCancelar = ((Telerik.Windows.Controls.RadButton)(target));
            
            #line 62 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.btnCancelar_Click);
            
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
            case 3:
            
            #line 36 "..\..\..\..\..\Cadastro\Formularios\FormCat2.xaml"
            ((Telerik.Windows.Controls.RadComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboPreco_SelectionChanged);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}


﻿#pragma checksum "C:\Users\jules\Documents\GitHub\GradebookCS\GradebookCS\View\UserControls\AssignmentUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3C5656AA4734AE9DFF645C87AA124D7C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GradebookCS.View.UserControls
{
    partial class AssignmentUserControl : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.Root = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 13 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)this.Root).DoubleTapped += this.AssignmentUserControl_DoubleTapped;
                    #line default
                }
                break;
            case 2:
                {
                    this.NameTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 22 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.NameTextBlock).DoubleTapped += this.AssignmentUserControl_DoubleTapped;
                    #line default
                }
                break;
            case 3:
                {
                    this.NameTextBox = (global::GradebookCS.View.CustomControls.AutoSelectOnFocusTextBox)(target);
                }
                break;
            case 4:
                {
                    this.ScoreTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 24 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.ScoreTextBlock).DoubleTapped += this.AssignmentUserControl_DoubleTapped;
                    #line default
                }
                break;
            case 5:
                {
                    this.ScoreTextBox = (global::GradebookCS.View.CustomControls.AutoSelectOnFocusTextBox)(target);
                }
                break;
            case 6:
                {
                    this.MaxScoreTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 26 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.MaxScoreTextBlock).DoubleTapped += this.AssignmentUserControl_DoubleTapped;
                    #line default
                }
                break;
            case 7:
                {
                    this.MaxScoreTextBox = (global::GradebookCS.View.CustomControls.AutoSelectOnFocusTextBox)(target);
                }
                break;
            case 8:
                {
                    this.PercentTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 28 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.PercentTextBlock).DoubleTapped += this.AssignmentUserControl_DoubleTapped;
                    #line default
                }
                break;
            case 9:
                {
                    this.EditButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 29 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.EditButton).Tapped += this.EditButton_Tapped;
                    #line 29 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.EditButton).DoubleTapped += this.AssignmentUserControl_DoubleTapped;
                    #line default
                }
                break;
            case 10:
                {
                    this.SaveButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 30 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SaveButton).Tapped += this.SaveButton_Tapped;
                    #line default
                }
                break;
            case 11:
                {
                    this.DeleteButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 31 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.DeleteButton).DoubleTapped += this.AssignmentUserControl_DoubleTapped;
                    #line 31 "..\..\..\..\View\UserControls\AssignmentUserControl.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.DeleteButton).Tapped += this.DeleteButton_Tapped;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

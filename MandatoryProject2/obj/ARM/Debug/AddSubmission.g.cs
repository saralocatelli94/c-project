﻿#pragma checksum "C:\Users\sara\Documents\GitHub\c-sharp-project\MandatoryProject2\AddSubmission.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "74CCC38BF2BD592148282B1D0FD39369"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MandatoryProject2
{
    partial class BlankPage1 : 
        global::Windows.UI.Xaml.Controls.Page, 
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
                    this.Name = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 11 "..\..\..\AddSubmission.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.Name).GotFocus += this.TextBox_GotFocus;
                    #line default
                }
                break;
            case 2:
                {
                    this.Surname = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 12 "..\..\..\AddSubmission.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.Surname).GotFocus += this.TextBox_GotFocus;
                    #line default
                }
                break;
            case 3:
                {
                    this.PhoneNr = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 13 "..\..\..\AddSubmission.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.PhoneNr).GotFocus += this.TextBox_GotFocus;
                    #line default
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 14 "..\..\..\AddSubmission.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.Button_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.Email = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 15 "..\..\..\AddSubmission.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.Email).GotFocus += this.TextBox_GotFocus;
                    #line default
                }
                break;
            case 6:
                {
                    this.SeriaNr = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 16 "..\..\..\AddSubmission.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.SeriaNr).GotFocus += this.TextBox_GotFocus;
                    #line default
                }
                break;
            case 7:
                {
                    this.Date = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                    #line 18 "..\..\..\AddSubmission.xaml"
                    ((global::Windows.UI.Xaml.Controls.DatePicker)this.Date).DateChanged += this.DateChanged;
                    #line default
                }
                break;
            case 8:
                {
                    this.Upload = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 19 "..\..\..\AddSubmission.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Upload).Click += this.Upload_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.Image = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 10:
                {
                    this.NameLabel = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11:
                {
                    this.NameLabel_Copy = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12:
                {
                    this.NameLabel_Copy1 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13:
                {
                    this.NameLabel_Copy2 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14:
                {
                    this.NameLabel_Copy3 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 15:
                {
                    this.NameLabel_Copy4 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
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


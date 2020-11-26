/*
               Copyright (c) 2015-2020 Developer Express Inc.
{*******************************************************************}
{                                                                   }
{       Developer Express Mobile UI for Xamarin.Forms               }
{                                                                   }
{                                                                   }
{       Copyright (c) 2015-2020 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING         }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
using System;
using DemoCenter.Forms.ViewModels;
using DevExpress.XamarinForms.Editors;
using DevExpress.XamarinForms.Navigation;
using Xamarin.Forms;

namespace DemoCenter.Forms.Views {
    public partial class MainPage : ErrorDialogPage {
        public MainPage() {
            InitializeComponent();
            TitleViewExtensions.SetIsShadowVisible(this, false);
        }
        private void OnInfoClicked(object sender, EventArgs args) {
            (Application.Current.MainPage as DrawerPage).IsDrawerOpened = true;
        }
        public void DemoItem_TappedControlShortcut(object sender, System.EventArgs e) {
            var groupItemView = (GroupItemView)sender;
            if (BindingContext is MainViewModel viewModel && groupItemView != null) {
                if (viewModel.NavigationDemoCommand != null) {
                    viewModel.NavigationDemoCommand.Execute(groupItemView.BindingContext);
                }
            }
        }
    }
}

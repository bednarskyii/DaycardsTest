﻿using System;
using ControlsTest.Controls;
using ControlsTest.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryWithoutBorder), typeof(EntryWithoutBorderRenderer))]
namespace ControlsTest.iOS.Renderers
{
    public class EntryWithoutBorderRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.LeftView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                this.Control.RightView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                this.Control.LeftViewMode = UITextFieldViewMode.Always;
                this.Control.RightViewMode = UITextFieldViewMode.Always;

                this.Control.BorderStyle = UITextBorderStyle.None;
                this.Element.HeightRequest = 30;
            }
        }
    }
}

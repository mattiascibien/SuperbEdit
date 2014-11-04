﻿using System;
using System.Windows;
using ICSharpCode.AvalonEdit;

namespace SuperbEdit.AvalonTextEditor.Controls
{
    public class ModernTextEditor: ICSharpCode.AvalonEdit.TextEditor
    {
        private bool _suppressTextChangedEvent;

        public new string Text
        {
            get
            {
                if (base.Document == null)
                {
                    return string.Empty;
                }
                return base.Document.Text;
            }
            set
            {
                if (base.Document != null && base.Document.Text != value)
                {
                    _suppressTextChangedEvent = true;
                    var caretOffset = base.CaretOffset;
                    base.Document.Text = value;
                    base.CaretOffset = caretOffset;
                    _suppressTextChangedEvent = false;
                }
            }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ModernTextEditor),
            new PropertyMetadata((obj, args) =>
            {
                ModernTextEditor target = (ModernTextEditor)obj;
                target.Text = (string)args.NewValue??string.Empty;
            })
        );


        protected override void OnTextChanged(EventArgs e)
        {
            if (_suppressTextChangedEvent)
            {
                return;
            }
            SetValue(TextProperty, Text);
            base.OnTextChanged(e);
        }
    }
}

using System.Windows;
using System.Windows.Data;
using System;

namespace Trascend.Bolet.ControlesByT
{
    public class ByTDatePicker : System.Windows.Controls.DatePicker
    {
        public static readonly DependencyProperty EditableProperty = DependencyProperty.Register("Editable", typeof(bool),
            typeof(ByTDatePicker), new PropertyMetadata(true));
        public bool Editable
        {
            get { return (bool)GetValue(EditableProperty); }
            set { SetValue(EditableProperty, value); }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var textBox = GetTemplateChild("PART_TextBox") as System.Windows.Controls.Primitives.DatePickerTextBox;
            var binding = new Binding { Source = this, Path = new PropertyPath(ByTDatePicker.EditableProperty) };
            textBox.SetBinding(System.Windows.Controls.Primitives.DatePickerTextBox.FocusableProperty, binding);
        }


        protected override void OnDateValidationError(System.Windows.Controls.DatePickerDateValidationErrorEventArgs e)
        {

            this.Text = null;

            base.OnDateValidationError(e);  
        }
    }
}

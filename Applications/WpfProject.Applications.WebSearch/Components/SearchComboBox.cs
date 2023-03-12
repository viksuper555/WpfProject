namespace WpfProject.Applications.WebSearch.Components
{
    using System.Windows.Controls;

    public class SearchComboBox : ComboBox
    {
        private int caretPosition;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var element = this.GetTemplateChild("PART_EditableTextBox");
            if (element != null)
            {
                var textBox = (TextBox)element;
                textBox.SelectionChanged += this.OnDropSelectionChanged;
            }
        }

        private void OnDropSelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (this.IsDropDownOpen && txt.SelectionLength > 0)
            {
                this.caretPosition = txt.SelectionLength; // caretPosition must be set to TextBox's SelectionLength
                txt.CaretIndex = this.caretPosition;
            }

            if (txt.SelectionLength == 0 && txt.CaretIndex != 0)
            {
                this.caretPosition = txt.CaretIndex;
            }
        }
    }
}

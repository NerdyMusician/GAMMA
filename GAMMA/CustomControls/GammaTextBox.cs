using System.Windows.Controls;
using System.Windows.Input;

namespace GAMMA.CustomControls
{
    public class GammaTextBox : TextBox
    {
        public GammaTextBox()
        {
            SelectAllOnFocus = true;
        }
        public bool SelectAllOnFocus { get; set; }
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (SelectAllOnFocus) { this.SelectAll(); }
            base.OnGotKeyboardFocus(e);
        }
    }
}

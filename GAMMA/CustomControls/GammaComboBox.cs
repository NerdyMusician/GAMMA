using System.Windows.Controls;
using System.Windows.Input;

namespace GAMMA.CustomControls
{
    public class GammaComboBox : ComboBox
    {
        public GammaComboBox() : base()
        {
            //OpenOnKeyboardFocus = true;
        }

        //public bool OpenOnKeyboardFocus { get; set; }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            //this.IsDropDownOpen = OpenOnKeyboardFocus;
            //base.OnGotKeyboardFocus(e);
        }

    }
}

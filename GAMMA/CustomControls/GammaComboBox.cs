using System.Windows.Controls;
using System.Windows.Input;

namespace GAMMA.CustomControls
{
    public class GammaComboBox : ComboBox
    {
        public GammaComboBox() : base()
        {

        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            e.Handled = !IsDropDownOpen;
            base.OnPreviewMouseWheel(e);
        }

    }
}

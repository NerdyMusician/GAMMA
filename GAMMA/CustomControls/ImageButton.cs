using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace GAMMA.CustomControls
{
    public class ImageButton : Button
    {
        public ImageButton(): base()
        {

        }

        public Style ImageResource { get; set; }
        public string Text { get; set; }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ImageButton), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("ImageResource", typeof(Style), typeof(ImageButton), new PropertyMetadata(default(Style)));

    }
    public class MiniButton : Button
    {
        public MiniButton() : base()
        {
            Style = FindResource("MiniButton") as Style;
        }

        public Style ImageResource { get; set; }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("ImageResource", typeof(Style), typeof(MiniButton), new PropertyMetadata(default(Style)));

    }
    public class IconButton : Button
    {
        public IconButton() : base()
        {
            Style = FindResource("IconButton") as Style;
        }

        public Style ImageResource { get; set; }

    }
    public class MiniToggleButton : ToggleButton
    {
        public MiniToggleButton() : base()
        {
            Style = FindResource("MiniToggleButton") as Style;
            CloseOnWindowFocusLoss = true;
        }

        public Style ImageResource { get; set; }
        public bool CloseOnWindowFocusLoss { get; set; }

    }
    public class IconToggleButton : ToggleButton
    {
        public IconToggleButton() : base()
        {
            Style = FindResource("IconToggleButton") as Style;
            CloseOnWindowFocusLoss = true;
        }

        public Style ImageResource { get; set; }
        public bool CloseOnWindowFocusLoss { get; set; }

    }
    public class ImageToggleButton : ToggleButton
    {
        public ImageToggleButton() : base()
        {
            Style = FindResource("ImageToggleButton") as Style;
            CloseOnWindowFocusLoss = true;
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ImageToggleButton), new PropertyMetadata(default(string)));

        public Style ImageResource { get; set; }
        public bool CloseOnWindowFocusLoss { get; set; }
    }

}

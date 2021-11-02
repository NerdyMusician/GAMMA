using System;

namespace GAMMA.Toolbox
{
    public class XmlSaveMode : Attribute
    {
        public XmlSaveMode(string mode)
        {
            Mode = mode;
        }

        public string Mode { get; set; }

    }
}

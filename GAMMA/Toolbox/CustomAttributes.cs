using System;

namespace GAMMA.Toolbox
{
    public class XmlSaveMode : Attribute
    {
        public XmlSaveMode(XSME mode)
        {
            Mode = mode;
        }

        public XSME Mode { get; set; }

    }

    public enum XSME
    {
        Single,
        Enumerable
    }

}

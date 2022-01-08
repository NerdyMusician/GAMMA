using GAMMA.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static List<ConvertibleValue> ToConvertibleValueList(this List<string> list)
        {
            List<ConvertibleValue> convertibleValues = new();
            foreach (string value in list)
            {
                convertibleValues.Add(new(value));
            }
            return convertibleValues;
        }
        public static List<LabeledNumber> ToLabeledNumberList(this List<string> list)
        {
            List<LabeledNumber> labeledNumbers = new();
            foreach (string value in list)
            {
                labeledNumbers.Add(new(value));
                
            }
            return labeledNumbers;
        }
    }
}

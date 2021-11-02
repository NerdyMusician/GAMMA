using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GAMMA.Models
{
    [Serializable]
    public class BaseModel : INotifyPropertyChanged
    {
        // Constructors
        public BaseModel()
        {

        }

        // Public Properties
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        // Public Methods
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}

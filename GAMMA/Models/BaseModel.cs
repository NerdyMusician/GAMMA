using System;
using System.Collections.Generic;
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
        protected bool SetAndNotify<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            // courtesy of GitHub user stevemonaco
            if (EqualityComparer<T>.Default.Equals(field, value)) { return false; }
            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

    }

}

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace macilaci
{
    public class Bindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string tulajdonsagNev = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(tulajdonsagNev));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RSS_reader.ViewModel
{
  
        class ViewModelBase : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChange([CallerMemberName] string Member = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Member));
            }

        }
    
}

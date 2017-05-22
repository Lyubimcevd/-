using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Cards_of_defectation.Classes
{
    class ChoiceViewModal: INotifyPropertyChanged
    {
        string podr;
        bool is_check;

        public event PropertyChangedEventHandler PropertyChanged;

        public ChoiceViewModal(string ppodr)
        {
            podr = ppodr;
            is_check = false;
        }
        public string Podrazd
        {
            get
            {
                return podr;
            }
        }
        public bool IsChecked
        {
            get
            {
                return is_check;
            }
            set
            {
                is_check = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Cards_of_defectation.ViewModal
{
    class TreeViewModal: INotifyPropertyChanged
    {
        ObservableCollection<TreeViewModal> children;
        public event PropertyChangedEventHandler PropertyChanged;
        string header;
        int id;

        public TreeViewModal(string pheader,int pid)
        {
            header = pheader;
            id = pid;
            children = new ObservableCollection<TreeViewModal>();
        }
        public ObservableCollection<TreeViewModal> Children
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
                OnPropertyChanged("Children");
            }
        }
        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public bool IsSelected
        {
            get
            {
                return true;
            }
        }
    }
}

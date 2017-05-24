using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Cards_of_defectation.Classes;

namespace Cards_of_defectation.ViewModal
{
    class TreeViewModal: INotifyPropertyChanged
    {
        ObservableCollection<TreeViewModal> children;
        public event PropertyChangedEventHandler PropertyChanged;
        RowDefectViewModal row;

        public TreeViewModal(RowDefectViewModal prow)
        {
            row = prow;
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
        public string Cherch
        {
            get
            {
                if (row.Cherch == null) return row.Naim_det;
                else return row.Cherch;
            }
        }
        public string Nom_kart
        {
            get
            {
                return row.Nom_kart;
            }
        }
        public string Spos_ustr
        {
            get
            {
                return row.Spos_ustr;
            }
        }
        public float Kolvo
        {
            get
            {
                return row.Kolvo;
            }
        }
        public string IsDone
        {
            get
            {
                if (row.Data_def != null) return "Выполнено";
                else return "В работе";
            }
        }
        public int Id
        {
            get
            {
                return row.Id;
            }
        }
        public string Nom_ceh
        {
            get
            {
                if (row.Ceh.Length == 0) return "999";
                else return row.Ceh;
            }
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public void Sort()
        {
            TreeViewModal tmp;
            for (int i = 0; i<children.Count-1;i++)
                for(int j = i+1; j < children.Count;j++)
                {
                    if (string.Compare(children[i].Nom_ceh,children[j].Nom_ceh)>0)
                    {
                        tmp = children[i];
                        children[i] = children[j];
                        children[j] = tmp;
                    }
                }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cards_of_defectation.ОТГО.Classes;

namespace Cards_of_defectation.ОТГО.Windows
{
    public partial class CreateWindowNumberSZ : Window
    {
        NomSZ_Cherch_Naim_ZavNom tmp;
        public CreateWindowNumberSZ(NomSZ_Cherch_Naim_ZavNom ptmp)
        {
            InitializeComponent();
            tmp = ptmp;
            textbox.Focus();
        }

        private void MaskedTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tmp.Nom_sz = textbox.Text;
                CreateWindowNomCherch Next_window = new CreateWindowNomCherch(tmp);
                Next_window.Show();
                this.Close();
            } 
        }
    }
}

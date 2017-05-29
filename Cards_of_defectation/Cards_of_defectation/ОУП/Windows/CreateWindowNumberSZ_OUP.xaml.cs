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
using Cards_of_defectation.ViewModal;

namespace Cards_of_defectation.ОУП.Windows
{
    public partial class CreateWindowNumberSZ_OUP : Window
    {
        string tmp;
        public CreateWindowNumberSZ_OUP()
        {
            InitializeComponent();
            textbox.Focus();
        }
        public string Return_nom_sz()
        {
            return tmp;
        }

        private void MaskedTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tmp = textbox.Text;
                this.Close();
            } 
        }
    }
}

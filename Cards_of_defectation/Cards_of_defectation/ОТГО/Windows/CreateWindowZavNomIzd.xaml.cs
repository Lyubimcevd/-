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
using Cards_of_defectation.Classes;
using System.Data;
using Cards_of_defectation.Windows;

namespace Cards_of_defectation.ОТГО.Windows
{
    public partial class CreateWindowZavNomIzd : Window
    {
        NomSZ_Cherch_Naim tmp;
        public CreateWindowZavNomIzd(NomSZ_Cherch_Naim ptmp)
        {
            InitializeComponent();
            tmp = ptmp;
            textbox.Focus();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DataTable DT;
                DataRow New_row;
                if (Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select * from rz_naim_cherch where naim = '" + tmp.Naim + "' and cherch = '"
                    + tmp.Cherch + "'").Count == 0)
                {
                    DT = Server.InitServer().DataBase("uit").Table("select * from rz_naim_cherch").LoadTableFromServer();
                    New_row = DT.NewRow();
                    New_row[0] = tmp.Naim;
                    New_row[1] = tmp.Cherch;
                    DT.Rows.Add(New_row);
                    Server.InitServer().DataBase("uit").Table("select * from rz_naim_cherch").UpdateServerData(DT);
                }
                if (Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select * from rz_ser_nom_naim where ser_nom = '" + textbox.Text
                    + "' and naim = '" + tmp.Naim + "'").Count == 0)
                {
                    DT = Server.InitServer().DataBase("uit").Table("select * from rz_ser_nom_naim").LoadTableFromServer();
                    New_row = DT.NewRow();
                    New_row[0] = textbox.Text;
                    New_row[1] = tmp.Naim;
                    DT.Rows.Add(New_row);
                    Server.InitServer().DataBase("uit").Table("select * from rz_ser_nom_naim").UpdateServerData(DT);
                }

                SlugebZapiska FL = new SlugebZapiska(tmp.Nom_sz,textbox.Text);
                FL.Show();
                this.Close();
            }
        }
    }
}

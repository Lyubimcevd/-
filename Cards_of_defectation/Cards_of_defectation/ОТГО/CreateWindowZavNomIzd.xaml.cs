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

namespace Cards_of_defectation.ОТГО
{
    public partial class CreateWindowZavNomIzd : Window
    {
        NomSZ_Cherch_Naim_ZavNom tmp;
        public CreateWindowZavNomIzd(NomSZ_Cherch_Naim_ZavNom ptmp)
        {
            InitializeComponent();
            tmp = ptmp;
            textbox.Focus();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DataTable DT = Server.InitServer().DataBase("test1").Table("select * from type_cherch").LoadTableFromServer();
                DataRow New_row =  DT.NewRow();
                New_row[0] = tmp.Naim;
                New_row[1] = tmp.Cherch;
                DT.Rows.Add(New_row);
                Server.InitServer().DataBase("test1").Table("select * from type_cherch").UpdateServerData(DT);

                DT = Server.InitServer().DataBase("test1").Table("select * from nom_type").LoadTableFromServer();
                New_row = DT.NewRow();
                New_row[0] = textbox.Text;
                New_row[1] = tmp.Naim;
                DT.Rows.Add(New_row);
                Server.InitServer().DataBase("test1").Table("select * from nom_type").UpdateServerData(DT);

                SlugebZapiska FL = new SlugebZapiska(tmp.Nom_sz,textbox.Text);
                FL.Show();
                this.Close();
            }
        }
    }
}

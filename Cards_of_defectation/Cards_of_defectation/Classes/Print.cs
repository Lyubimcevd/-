using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using Cards_of_defectation.Classes;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Cards_of_defectation.ViewModal;

namespace Cards_of_defectation.Classes
{
    class Print
    {
        static Print print;
        Word.Application application;
        Word.Document document;
        Object missingObj, trueObj, falseObj;
        Word.Range range;
        Word.Table table;
        string fileName;

        Print()
        {
            missingObj = System.Reflection.Missing.Value;
            trueObj = true;
            falseObj = false;
        }

        public static Print Init()
        {
            if (print == null) print = new Print();
            return print;
        }

        public void PrintTree(TreeViewModal Modal)
        {
            application = new Word.Application();
            fileName = Path.GetTempFileName();
            document = application.Documents.Open(fileName);
            range = document.Range();
            document.Paragraphs.Space1();
            ExpandTree(Modal, 0);
            application.Visible = true;
        }
        void ExpandTree(TreeViewModal Modal, int koef)
        {
            range.Text += new string('\t', koef) + Modal.Cherch+"  "
                +Modal.Nom_kart+"  "+Modal.Spos_ustr+"  "+Modal.Kolvo+"  "+Modal.IsDone;
            foreach (TreeViewModal child in Modal.Children) ExpandTree(child, koef + 1);
        }
        public void PrintDocument(RowDefectViewModal header,ObservableCollection<RowDefectViewModal> Rows)
        {
            application = new Word.Application();
            fileName = Path.GetTempFileName();
            File.WriteAllBytes(fileName, Properties.Resources.бланк);
            document = application.Documents.Open(fileName);
            range = SearchRange("@@nom_zak");
            if (range != null) range.Text = "№ " + header.Nom_zak.ToString();
            range = SearchRange("@@prior");
            if (range != null) range.Text = header.Prior.ToString();
            range = SearchRange("@@kolvo");
            if (range != null) range.Text = header.Kolvo.ToString();
            range = SearchRange("@@ser_nom_izd");
            if (range != null) range.Text = "№ " + header.Ser_nom.ToString();
            range = SearchRange("@@Cherch");
            if (range != null) range.Text = header.Cherch;
            range = SearchRange("@@naim_det_1");
            if (range != null)
                range.Text = Server.InitServer().DataBase("uit")
                    .ExecuteCommand("select naim from rz_ser_nom_naim where ser_nom = '"
                                    + header.Ser_nom+"'")[0].ToString();
            range = SearchRange("@@naim_det_2");
            if (range != null) range.Text = header.Naim_det;          
            range = SearchRange("@@nom_kart");
            if (range != null)
                if (header.Par == 0) range.Text = header.Nom_sz.ToString();
                else range.Text = header.Nom_kart;
            table = document.Tables[1];
            for (int i = 0; i < Rows.Count; i++)
            {
                table.Cell(table.Rows.Count, 1).Range.Text = i.ToString()+1;
                table.Cell(table.Rows.Count, 2).Range.Text = Rows[i].Cherch + " = " + Rows[i].Kolvo.ToString() + " шт" +
                    "\n" + Rows[i].Opis_def + "\n" + Rows[i].Opis_def_komment;
                table.Cell(table.Rows.Count, 3).Range.Text = Rows[i].Prichina + "\n"+ Rows[i].Prichina_komment;
                table.Cell(table.Rows.Count, 4).Range.Text = Rows[i].Met_opr + "\n"+ Rows[i].Met_opr_komment;
                table.Cell(table.Rows.Count, 5).Range.Text = Rows[i].Teh_treb + "\n" + Rows[i].Teh_treb_komment;
                table.Cell(table.Rows.Count, 6).Range.Text = Rows[i].Spos_ustr+ "\n"+ Rows[i].Spos_ustr_komment;
                if (i < Rows.Count - 1) table.Rows.Add(missingObj);
            }
            foreach (Word.Paragraph paragraph in document.Paragraphs)
                if (paragraph.Range.Text.Trim() == string.Empty)
                {
                    paragraph.Range.Select();
                    application.Selection.Delete();
                }
            application.Visible = true;
        }
        public void PrintSlZap(SlugebZapiskaViewModal flvm,List<ChoiceViewModal> Rows)
        {
            application = new Word.Application();
            fileName = Path.GetTempFileName();
            File.WriteAllBytes(fileName, Properties.Resources.sl_zap);
            document = application.Documents.Open(fileName);
            range = SearchRange("@@poluch");
            if (range != null)
                range.Text = "";
                foreach (ChoiceViewModal cvm in Rows)
                    if (cvm.IsChecked) range.Text += cvm.Podrazd + "\n";
            range = SearchRange("@@date");
            if (range != null) range.Text = DateTime.Now.ToShortDateString();
            range = SearchRange("@@kontract");
            if (range != null) range.Text = flvm.SelectedKontract;
            range = SearchRange("@@izdelie");
            if (range != null) range.Text = flvm.Izdelie;
            range = SearchRange("@@nom_stanc");
            if (range != null) range.Text = flvm.SelectedSer_nom;
            range = SearchRange("@@voin_chast");
            if (range != null) range.Text = flvm.Voin_chast;
            range = SearchRange("@@nom_zak");
            if (range != null) range.Text = flvm.Nom_zak.ToString();
            range = SearchRange("@@srok_otprav");
            if (range != null) range.Text = flvm.Srok_otprav;
            range = SearchRange("@@prim");
            if (range != null) range.Text = flvm.Proizv_chast;
            table = document.Tables[2];
            for (int i = 0; i < flvm.Izgot.Count; i++)
            {
                table.Cell(table.Rows.Count, 1).Range.Text = (i+1).ToString();
                table.Cell(table.Rows.Count, 2).Range.Text = flvm.Izgot[i].SelectedCherch;
                table.Cell(table.Rows.Count, 3).Range.Text = flvm.Izgot[i].SelectedNaim;
                table.Cell(table.Rows.Count, 4).Range.Text = flvm.Izgot[i].Kolvo.ToString();                     
                table.Cell(table.Rows.Count, 5).Range.Text = flvm.Izgot[i].Prim;
                if (i < flvm.Izgot.Count - 1) table.Rows.Add(missingObj);
            }
            table = document.Tables[3];
            for (int i = 0; i < flvm.Remont.Count; i++)
            {
                table.Cell(table.Rows.Count, 1).Range.Text = (i+1).ToString();
                table.Cell(table.Rows.Count, 2).Range.Text = flvm.Remont[i].SelectedCherch;
                table.Cell(table.Rows.Count, 3).Range.Text = flvm.Remont[i].SelectedNaim;
                table.Cell(table.Rows.Count, 4).Range.Text = flvm.Remont[i].Kolvo.ToString();                    
                table.Cell(table.Rows.Count, 5).Range.Text = flvm.Remont[i].Prim;
                if (i < flvm.Remont.Count - 1) table.Rows.Add(missingObj);
            }
            table = document.Tables[4];
            for (int i = 0; i < flvm.Priobr.Count; i++)
            {
                table.Cell(table.Rows.Count, 1).Range.Text = (i+1).ToString();
                table.Cell(table.Rows.Count, 2).Range.Text = flvm.Priobr[i].SelectedN_nomer;
                table.Cell(table.Rows.Count, 3).Range.Text = flvm.Priobr[i].SelectedCherch;
                table.Cell(table.Rows.Count, 4).Range.Text = flvm.Priobr[i].SelectedNaim;
                table.Cell(table.Rows.Count, 5).Range.Text = flvm.Priobr[i].Kolvo.ToString() 
                    + EdIzm(flvm.Priobr[i].SelectedN_nomer);
                table.Cell(table.Rows.Count, 6).Range.Text = flvm.Priobr[i].Prim;
                if (i < flvm.Priobr.Count - 1) table.Rows.Add(missingObj);
            }
            table = document.Tables[5];
            for (int i = 0; i < flvm.Stor_rem.Count; i++)
            {
                table.Cell(table.Rows.Count, 1).Range.Text = (i+1).ToString();
                table.Cell(table.Rows.Count, 2).Range.Text = flvm.Stor_rem[i].SelectedCherch;
                table.Cell(table.Rows.Count, 3).Range.Text = flvm.Stor_rem[i].SelectedNaim;
                table.Cell(table.Rows.Count, 4).Range.Text = flvm.Stor_rem[i].Kolvo.ToString();                  
                table.Cell(table.Rows.Count, 5).Range.Text = flvm.Stor_rem[i].SelectedIzgotov;
                table.Cell(table.Rows.Count, 6).Range.Text = flvm.Stor_rem[i].Prim;
                if (i < flvm.Stor_rem.Count - 1) table.Rows.Add(missingObj);
            }
            foreach (Word.Paragraph paragraph in document.Paragraphs)
                if (paragraph.Range.Text.Trim() == string.Empty)
                {
                    paragraph.Range.Select();
                    application.Selection.Delete();
                }
            application.Visible = true;
        }        
        Word.Range SearchRange(string stringToFind)
        {
            object stringToFindObj = stringToFind;
            Word.Range wordRange;
            bool rangeFound;
            for (int i = 1; i <= document.Sections.Count; i++)
            {
                wordRange = document.Sections[i].Range;
                Word.Find wordFindObj = wordRange.Find;
                object[] wordFindParameters = new object[15] { stringToFindObj, missingObj, missingObj, missingObj,
                    missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj, missingObj,
                    missingObj, missingObj, missingObj };
                rangeFound = (bool)wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null,
                    wordFindObj, wordFindParameters);
                if (rangeFound) return wordRange;
            }
            return null;
        }
        string EdIzm(string cherch)
        {
            List<object> tmp;
            tmp = Server.InitServer().DataBase("cvodka")
                .ExecuteCommand("select ei from shifr where prbei = 1 and Ltrim(nn) = '" + cherch+"'");
            if (tmp.Count == 0)
                tmp = Server.InitServer().DataBase("cvodka")
                    .ExecuteCommand("select ei from shifr where prbei = 0 and Ltrim(nn) = '" + cherch+"'");               
            if (tmp.Count != 0)
                return Server.InitServer().DataBase("cvodka")
                    .ExecuteCommand("select kratko from ei_opisanie where ei = " + tmp[0])[0].ToString();
            else return "";

        }
    }
}

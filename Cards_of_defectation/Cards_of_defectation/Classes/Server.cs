using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows.Threading;
using Cards_of_defectation.Windows;
using Cards_of_defectation.ОУП.Windows;

namespace Cards_of_defectation.Classes
{
    class Server
    {
        static Server server;
        Dictionary<string, Connection> connections;

        private Server()
        {
            connections = new Dictionary<string, Connection>();
        }
        public static Server InitServer()
        {
            if (server == null) server = new Server();
            return server;
        }
        public Connection DataBase(string DataBaseName)
        {
            if (!connections.ContainsKey(DataBaseName)) 
                connections.Add(DataBaseName, new Connection(DataBaseName));
            return connections[DataBaseName];
        }
    }

    class Connection
    {
        SqlConnection conn;
        Dispatcher dis;
        ShopAlert SA;
        Tree_defect TD;
        Work_shop WS;
        MainOUP MOUP;

        public Connection(string DataBaseName)
        {
            conn = new SqlConnection("user id=ldo;password=IfLyyz4sCJ;server=nitel-hp;database=" + DataBaseName + ";MultipleActiveResultSets=True");
            //SqlDependency.Start("user id=ldo;password=IfLyyz4sCJ;server=nitel-hp;database="+ DataBaseName + ";MultipleActiveResultSets=True");
            conn.Open();
        }
        public List<object> ExecuteCommand(string Command)
        {
            SqlCommand SC = new SqlCommand(Command, conn);
            SqlDataReader SDR = SC.ExecuteReader();
            if (SDR.HasRows)
            {
                List<object> ResultDate = new List<object>();
                while (SDR.Read())
                {
                    for (int i = 0; i < SDR.GetSchemaTable().Rows.Count; i++)
                        if (!SDR.IsDBNull(i)) ResultDate.Add(SDR.GetValue(i));
                        else ResultDate.Add(null);
                }
                return ResultDate;
            }
            else return new List<object>();
        }
        public DataTableBuffer Table(string query)
        {
            return new DataTableBuffer(query, conn);
        }
        public void InitStalker(Dispatcher pdis, ShopAlert pSA)
        {
            dis = pdis;
            SA = pSA;
            Stalker();
        }
        public void InitStalker(Dispatcher pdis, Tree_defect pTD)
        {
            dis = pdis;
            TD = pTD;
            Stalker();
        }
        public void InitStalker(Dispatcher pdis, Work_shop pWS)
        {
            dis = pdis;
            WS = pWS;
            Stalker();
        }
        public void InitStalker(Dispatcher pdis, MainOUP pMOUP)
        {
            dis = pdis;
            MOUP = pMOUP;
            Stalker();
        }
        void Stalker()
        {
            using (var command = new SqlCommand("select a.id,a.nom_ceh,a.kolvo,b.Nom_sz from dbo.kart_defect as a, dbo.plan_rabot as b", conn))
            {
                var sqlDependency = new SqlDependency(command);
                sqlDependency.OnChange += new OnChangeEventHandler(OnDatabaseChange);
                //command.ExecuteReader();
            }
        }
        void OnDatabaseChange(object sender, SqlNotificationEventArgs e)
        {
            if (SA != null) dis.Invoke(new Action(SA.UpdateRow));
            if (TD != null) dis.Invoke(new Action(TD.UpdateTree));
            if (WS != null) dis.Invoke(new Action(WS.UpdateTable));
            if (MOUP != null) dis.Invoke(new Action(MOUP.UpdateTable));
            Stalker();
        }
        public void DeleteStalker(Tree_defect pTD)
        {
            TD = null;
        }
    }

    class DataTableBuffer
    {
        DataTable DT;
        SqlDataAdapter DA;
        SqlCommandBuilder CB;

        public DataTableBuffer(string SelectCommand,SqlConnection conn)
        {
            DA = new SqlDataAdapter(SelectCommand, conn);
            CB = new SqlCommandBuilder(DA);
            int start = SelectCommand.IndexOf("from") + 5;
            int end = SelectCommand.IndexOf("where")-1;
            if (end == -2) end = SelectCommand.Length;
            DT = new DataTable(SelectCommand.Substring(start, end - start));
            DA.Fill(DT);
        }
        public void UpdateServerData(List<Row_in_plan_rabot> Rows)
        {
            Converter.ListToTable(Rows, DT);
            DA.Update(DT);
        }
        public void UpdateServerData(List<Row_in_kart_defect> Rows)
        {
            Converter.ListToTable(Rows, DT);
            DA.Update(DT);
        }
        public void UpdateServerData(DataTable pDT)
        {
            DA.Update(pDT);
        }
        public object LoadFromServer()
        {
            if (DT.TableName == "rz_plan_rabot")
            {
                return Converter.TableToList(DT) as List<Row_in_plan_rabot>;
            }
            else
            {              
                return Converter.TableToList(DT) as List<Row_in_kart_defect>;              
            }        
        }
        public List<Row_in_kart_defect> LoadFromServerReverse()
        {
            List<Row_in_kart_defect> result = Converter.TableToList(DT) as List<Row_in_kart_defect>;
            result.Reverse();
            return result;
        }
        public DataTable LoadTableFromServer()
        {
            return DT;
        }
    }
}

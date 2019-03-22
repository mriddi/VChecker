using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Configuration;

namespace VChecker
{
    class Checker
    {
        public static void pushNvdFromXmlToDb(string pathToXml, string pathToDb)
        {
            if (pathToXml != "" & pathToDb != "")
            {
                Model1Container1 db = new Model1Container1(Checker.getConnectionString("Model1Container1", Form1.openDialog("файл данных для загрузки (*.xml)", "xml")));
                Nvd nvdDeserialized = deserializeXML(pathToXml);

                DataTable dt = MakeTable("Kaf", new List<string> { "ProductId", "VulnerablesoftwarelistVulnerablesoftwarelistId", "ProductIdId" }, new List<Type> { System.Type.GetType("System.String"), System.Type.GetType("System.Int32"), System.Type.GetType("System.Int32") }, new List<bool> { false, false, true }, "ProductIdId");
                DataRow row = dt.NewRow();

                foreach (Entry entry in nvdDeserialized.Entry)
                {
                    foreach (VulnerableSoftwareList vSoftware in entry.VulnerableSoftwareList)
                    {
                        foreach (Product product in vSoftware.Product)
                        {
                            row = dt.NewRow();
                            row["ProductId"] = product.ProductId;
                            row["VulnerableSoftwareListId"] = product.VulnerableSoftwareListId;
                            dt.Rows.Add(row);
                        }
                    }
                }
                dt.AcceptChanges();

                //pushBulk(dt, pathToDb);





                if (db.EntrySet.Count<Entry>() != 0)
                {
                    List<Entry> entryToAdd = new List<Entry>();
                    foreach (Entry entry in nvdDeserialized.Entry)
                    {
                        bool notRepeat = true;

                        var query = from b in db.EntrySet
                                    select b;
                        foreach (var item in query)
                        {
                            if (item.EntryId == entry.EntryId)
                                notRepeat = false;
                        }
                        if (notRepeat)
                        {
                            entry.NvdId = 1;
                            entryToAdd.Add(entry);
                        }
                    }
                    if (entryToAdd.Count != 0)
                        db.EntrySet.AddRange(entryToAdd);
                }
                else
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.NvdSet.Add(nvdDeserialized);
                    db.Configuration.AutoDetectChangesEnabled = true;
                }
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    Console.WriteLine("!!!!!!!!!!! {0}", ex);
                }
            }
        }

        private static Nvd deserializeXML(string fileName)
        {
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(Nvd));
                FileStream myFileStream = new FileStream(fileName, FileMode.Open);
                return (Nvd)mySerializer.Deserialize(myFileStream);
            }
            catch { }
            return null;
        }

        public static HashSet<string> getInstalledSoftware()
        {
            // Данные на основе Win32

            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_Product");
            //foreach (ManagementObject mo in searcher.Get())
            //{
            //    try
            //    {
            //        listBox1.Items.Add(checkValue(mo.Properties["Caption"].Value.ToString()) + "***" + checkValue(mo.Properties["Version"].Value.ToString()));
            //    }
            //    catch { };
            //}

            // Данные на основе реестра

            HashSet<string> items = new HashSet<string>();
            //string SoftwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string SoftwareKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(SoftwareKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            if (sk.GetValue("DisplayName") != null)
                                items.Add(sk.GetValue("DisplayName").ToString()); //+ "***" + checkValue(sk.GetValue("DisplayVersion")));
                        }
                        catch {}
                    }
                }
            }
            return items;
        }

        public static HashSet<string> searchCVEforSoftware(HashSet<string> softeareHashSet)
        {
            HashSet<string> softwareVulnSoft = new HashSet<string>();
            Model1Container1 db = new Model1Container1(Checker.getConnectionString("Model1Container1", Form1.openDialog("файл данных для загрузки (*.xml)", "xml")));
            Nvd nvd = new Nvd();

            foreach (string software in softeareHashSet)
            {
                string[] softwareN = software.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<Product> complytList = new List<Product>();

                foreach (Product product in db.ProductSet)
                {
                    if (softwareN.All(product.ProductN.Contains))
                        complytList.Add(product);

                    // Дополнить: поиск в лоджикал листах
                }
                if (complytList.Count != 0)
                {
                    foreach (Product product in complytList)
                    {
                        softwareVulnSoft.Add(software + " - " + product.VulnerableSoftwareList.Entry.EntryId);
                    }
                }
            }
            return softwareVulnSoft;
        }

        public static string checkValue(object input)
        {
            if (input != null)
                return input.ToString();
            else
                return string.Empty;
        }

        private static DataTable MakeTable(string dataTableName, List<string> columnNames, List<Type> columnTypes, List<bool> autoIncrement, string columNamePK)
        {
            DataTable newDataTable = new DataTable(dataTableName);

            for (int i = 0; i < columnNames.Count; i++)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.DataType = columnTypes[i];
                dataColumn.ColumnName = columnNames[i];
                dataColumn.AutoIncrement = autoIncrement[i];
                newDataTable.Columns.Add(dataColumn);
                if (columnNames[i] == columNamePK)
                {
                    DataColumn[] keys = new DataColumn[1];
                    keys[0] = dataColumn;
                    newDataTable.PrimaryKey = keys;
                }
            }

            return newDataTable;
        }

        public static string getConnectionString(string connectionName, string pathToMdfFile)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();
            return String.Format(connectionString, pathToMdfFile);
        }

        private static void pushBulk(DataTable dataTable, string pathToDb)
        {
            string connectionString = getConnectionString("sql", pathToDb);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand commandRowCount = new SqlCommand("SELECT COUNT(*) FROM " + "dbo.ProductSet;", connection);
                long countStart = System.Convert.ToInt32(commandRowCount.ExecuteScalar());

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "dbo.ProductSet";

                    try
                    {
                        bulkCopy.WriteToServer(dataTable, DataRowState.Unchanged);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                long countEnd = System.Convert.ToInt32(commandRowCount.ExecuteScalar());
                Console.WriteLine("Ending row count = {0}, {1} rows were added.", countEnd, countEnd - countStart);
            }
        }
    }
}

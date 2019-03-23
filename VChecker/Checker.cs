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
                Model1Container1 db = new Model1Container1(pathToDb);
                Nvd nvdDeserialized = deserializeXML(pathToXml);


                //List<string> Nvd = new List<string> { "NvdId", "PubDate" };
                List<string> Entry = new List<string> { "EntryId", "Summary", "LastmodifiedDateTime", "PublishedDateTime", "NvdId" };
                List<string> References = new List<string> { "ReferencesId", "Lang", "ReferencesType", "EntryId", "Source" };
                List<string> Reference = new List<string> { "ReferenceId", "Href", "Lang", "Text", "ReferencesId" };
                List<string> VulnerableSoftwareList = new List<string> { "VulnerableSoftwareListId", "EntryId" };
                List<string> Product = new List<string> { "ProductId", "ProductN" , "VulnerableSoftwareListId" };
                List<string> VulnerableConfiguration = new List<string> { "VulnerableConfigurationId", "EntryId" };
                List<string> LogicalTest1 = new List<string> { "LogicalTest1Id", "Negate1", "Operator1", "VulnerableConfigurationId" };
                List<string> LogicalTest2 = new List<string> { "LogicalTest2Id", "Negate2", "Operator2", "LogicalTest1Id" };
                List<string> FactRef1 = new List<string> { "FactRef1Id", "Name", "LogicalTest1Id" };
                List<string> FactRef2 = new List<string> { "FactRef2Id", "Name", "LogicalTest2Id" };

                DataTable dtEntry = MakeTable(nameof(Entry), Entry, new List<Type> { }, new List<bool> {true,false,false,false,false }, Entry[0]);
                DataTable dtReferences = MakeTable(nameof(References), References, new List<Type> { }, new List<bool> {true,false,false,false,false }, References[0]);
                DataTable dtReference = MakeTable(nameof(Reference), Reference, new List<Type> { }, new List<bool> {true,false,false,false,false }, Reference[0]);
                DataTable dtVulnerableSoftwareList = MakeTable(nameof(VulnerableSoftwareList), VulnerableSoftwareList, new List<Type> { }, new List<bool> {true,false }, VulnerableSoftwareList[0]);
                DataTable dtProduct = MakeTable(nameof(Product), Product, new List<Type> { System.Type.GetType("System.Int32"), System.Type.GetType("System.String") , System.Type.GetType("System.Int32") }, new List<bool> { true, false , false}, Product[0]);
                DataTable dtVulnerableConfiguration = MakeTable(nameof(VulnerableConfiguration), VulnerableConfiguration, new List<Type> { }, new List<bool> {true,false }, VulnerableConfiguration[0]);
                DataTable dtLogicalTest1 = MakeTable(nameof(LogicalTest1), LogicalTest1, new List<Type> { }, new List<bool> { true,false,false,false}, LogicalTest1[0]);
                DataTable dtLogicalTest2 = MakeTable(nameof(LogicalTest2), LogicalTest2, new List<Type> { }, new List<bool> { true,false,false,false}, LogicalTest2[0]);
                DataTable dtFactRef1 = MakeTable(nameof(FactRef1), FactRef1, new List<Type> { }, new List<bool> { true,false,false}, FactRef1[0]);
                DataTable dtFactRef2 = MakeTable(nameof(FactRef2), FactRef2, new List<Type> { }, new List<bool> { true,false,false}, FactRef2[0]);

                DataRow rowEntry = dtEntry.NewRow();
                DataRow rowReferences = dtReferences.NewRow();
                DataRow rowReference = dtReference.NewRow();
                DataRow rowVulnerableSoftwareList = dtVulnerableSoftwareList.NewRow();
                DataRow rowProduct = dtProduct.NewRow();
                DataRow rowVulnerableConfiguration = dtVulnerableConfiguration.NewRow();
                DataRow rowLogicalTest1 = dtLogicalTest1.NewRow();
                DataRow rowLogicalTest2 = dtLogicalTest2.NewRow();
                DataRow rowFactRef1 = dtFactRef1.NewRow();
                DataRow rowFactRef2 = dtFactRef2.NewRow();

                foreach (Entry entry in nvdDeserialized.Entry)
                {
                    foreach (VulnerableSoftwareList vSoftware in entry.VulnerableSoftwareList)
                    {
                        foreach (Product product in vSoftware.Product)
                        {
                            rowProduct = dtProduct.NewRow();
                            rowProduct["ProductN"] = product.ProductN;
                            rowProduct["VulnerableSoftwareListId"] = 1;
                            dtProduct.Rows.Add(rowProduct);
                        }
                    }
                }
                dtProduct.AcceptChanges();

                pushBulk(dtProduct, pathToDb);





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

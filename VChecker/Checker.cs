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
                Model1Container1 db = new Model1Container1(/*pathToDb*/);
                Nvd nvdDeserialized = deserializeXML(pathToXml);

                List<string> Nvd = new List<string> { "PubDate" };
                List<string> Entry = new List<string> { "EntryId", "Summary", "LastmodifiedDateTime", "PublishedDateTime", "NvdPubDate" };
                List<string> References = new List<string> { "Id","Href", "EntryId" };
                List<string> VulnerableSoftwareList = new List<string> { "Id", "Product", "EntryId" };
                List<string> VulnerableConfiguration = new List<string> { "Id", "Name", "EntryId" };

                DataTable dtNvd= MakeTable(nameof(Nvd), Nvd, Nvd[0]);
                DataTable dtEntry = MakeTable(nameof(Entry), Entry, Entry[0]);
                DataTable dtReferences = MakeTable(nameof(References), References, References[0]);
                DataTable dtVulnerableSoftwareList = MakeTable(nameof(VulnerableSoftwareList), VulnerableSoftwareList, VulnerableSoftwareList[0]);
                DataTable dtVulnerableConfiguration = MakeTable(nameof(VulnerableConfiguration), VulnerableConfiguration, VulnerableConfiguration[0]);

                DataRow rowNvd = dtNvd.NewRow();
                DataRow rowEntry = dtEntry.NewRow();
                DataRow rowReferences = dtReferences.NewRow();
                DataRow rowVulnerableSoftwareList = dtVulnerableSoftwareList.NewRow();
                DataRow rowVulnerableConfiguration = dtVulnerableConfiguration.NewRow();

                rowNvd = dtNvd.NewRow();
                rowNvd["PubDate"] = nvdDeserialized.PubDate;
                dtNvd.Rows.Add(rowNvd);
                foreach (Entry entry in nvdDeserialized.Entry)
                {
                    rowEntry = dtEntry.NewRow();
                    rowEntry["EntryId"] = entry.EntryId;
                    rowEntry["Summary"] = entry.Summary;
                    rowEntry["LastmodifiedDateTime"] = entry.LastmodifiedDateTime;
                    rowEntry["PublishedDateTime"] = entry.PublishedDateTime;
                    rowEntry["NvdPubDate"] = entry.NvdPubDate;
                    dtEntry.Rows.Add(rowEntry);
                    foreach (References references in entry.References)
                    {
                        rowReferences = dtReferences.NewRow();
                        rowReferences["Href"] = references.Href;
                        rowReferences["EntryId"] = references.EntryId;
                        dtReferences.Rows.Add(rowReferences);
                    }
                    foreach (VulnerableSoftwareList vSoftware in entry.VulnerableSoftwareList)
                    {
                        rowVulnerableSoftwareList = dtVulnerableSoftwareList.NewRow();
                        rowVulnerableSoftwareList["Product"] = vSoftware.Product;
                        rowVulnerableSoftwareList["EntryId"] = vSoftware.EntryId;
                        dtVulnerableSoftwareList.Rows.Add(rowVulnerableSoftwareList);
                    }
                    foreach (VulnerableConfiguration vConfiguration in entry.VulnerableConfiguration)
                    {
                        rowVulnerableConfiguration = dtVulnerableConfiguration.NewRow();
                        rowVulnerableConfiguration["Name"] = vConfiguration.Name;
                        rowVulnerableConfiguration["EntryId"] = vConfiguration.EntryId;
                        dtVulnerableConfiguration.Rows.Add(rowVulnerableConfiguration);
                    }
                }
                dtNvd.AcceptChanges();
                rowEntry.AcceptChanges();
                rowReferences.AcceptChanges();
                rowVulnerableSoftwareList.AcceptChanges();
                rowVulnerableConfiguration.AcceptChanges();

                pushBulk(dtNvd, pathToDb);
                pushBulk(dtEntry, pathToDb);
                pushBulk(dtReferences, pathToDb);
                pushBulk(dtVulnerableSoftwareList, pathToDb);
                pushBulk(dtVulnerableConfiguration, pathToDb);

                //    if (db.EntrySet.Count<Entry>() != 0)
                //    {
                //        List<Entry> entryToAdd = new List<Entry>();
                //        foreach (Entry entry in nvdDeserialized.Entry)
                //        {
                //            bool notRepeat = true;

                //            var query = from b in db.EntrySet
                //                        select b;
                //            foreach (var item in query)
                //            {
                //                if (item.EntryId == entry.EntryId)
                //                    notRepeat = false;
                //            }
                //            if (notRepeat)
                //            {
                //                entry.NvdId = 1;
                //                entryToAdd.Add(entry);
                //            }
                //        }
                //        if (entryToAdd.Count != 0)
                //            db.EntrySet.AddRange(entryToAdd);
                //    }
                //    else
                //    {
                //        db.Configuration.AutoDetectChangesEnabled = false;
                //        db.NvdSet.Add(nvdDeserialized);
                //        db.Configuration.AutoDetectChangesEnabled = true;
                //    }
                //    try
                //    {
                //        db.SaveChanges();
                //    }
                //    catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                //    {
                //        Console.WriteLine("!!!!!!!!!!! {0}", ex);
                //    }
            }
        }

        private static Nvd deserializeXML(string fileName)
        {
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(XmlD.Nvd));
                FileStream myFileStream = new FileStream(fileName, FileMode.Open);
                XmlD.Nvd nvd = (XmlD.Nvd)mySerializer.Deserialize(myFileStream);
                Nvd nvdN = new Nvd();
                nvdN.PubDate = nvd.PubDate;

                foreach (XmlD.Entry entry in nvd.Entry)
                {
                    Entry entryN = new Entry();
                    entryN.EntryId = entry.EntryId;
                    entryN.Summary = entry.Summary;
                    entryN.LastmodifiedDateTime = entry.LastmodifiedDateTime;
                    entryN.PublishedDateTime = entry.PublishedDateTime;
                    entryN.NvdPubDate = nvdN.PubDate;
                    entryN.Nvd = nvdN;
                    foreach (XmlD.References references in entry.References)
                    {
                        foreach (XmlD.Reference reference in references.Reference)
                        {
                            References referencesN = new References();
                            referencesN.Href = reference.Href;
                            referencesN.EntryId = entryN.EntryId;
                            referencesN.Entry = entryN;
                            entryN.References.Add(referencesN);
                        }
                    }
                    //============//
                    foreach (XmlD.VulnerableSoftwareList vSoftwarList in entry.VulnerableSoftwareList)
                    {
                        foreach (XmlD.Product product in vSoftwarList.Product)
                        {
                            VulnerableSoftwareList vSoftwarListN = new VulnerableSoftwareList();
                            vSoftwarListN.Product = product.ProductN;
                            vSoftwarListN.EntryId = entryN.EntryId;
                            vSoftwarListN.Entry = entryN;
                            entryN.VulnerableSoftwareList.Add(vSoftwarListN);
                        }
                    }
                    //============//
                    foreach (XmlD.VulnerableConfiguration vConfiguration in entry.VulnerableConfiguration)
                    {
                        foreach (XmlD.LogicalTest1 lt1 in vConfiguration.LogicalTest1)
                        {
                            foreach (XmlD.FactRef1 fr1 in lt1.FactRef1)
                            {
                                VulnerableConfiguration vConfigurationN = new VulnerableConfiguration();
                                vConfigurationN.Name = fr1.Name;
                                vConfigurationN.EntryId = entryN.EntryId;
                                vConfigurationN.Entry = entryN;
                                entryN.VulnerableConfiguration.Add(vConfigurationN);
                            }

                            foreach (XmlD.LogicalTest2 lt2 in lt1.LogicalTest2)
                            {
                                foreach (XmlD.FactRef2 fr2 in lt2.FactRef2)
                                {
                                    VulnerableConfiguration vConfigurationN = new VulnerableConfiguration();
                                    vConfigurationN.Name = fr2.Name;
                                    vConfigurationN.EntryId = entryN.EntryId;
                                    vConfigurationN.Entry = entryN;
                                    entryN.VulnerableConfiguration.Add(vConfigurationN);
                                }
                            }
                        }
                    }
                    nvdN.Entry.Add(entryN);
                }
                return nvdN;
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

        //public static HashSet<string> searchCVEforSoftware(HashSet<string> softeareHashSet)
        //{
        //    HashSet<string> softwareVulnSoft = new HashSet<string>();
        //    Model1Container1 db = new Model1Container1(Checker.getConnectionString("Model1Container1", Form1.openDialog("файл данных для загрузки (*.xml)", "xml")));
        //    Nvd nvd = new Nvd();

        //    foreach (string software in softeareHashSet)
        //    {
        //        string[] softwareN = software.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //        List<Product> complytList = new List<Product>();

        //        foreach (Product product in db.ProductSet)
        //        {
        //            if (softwareN.All(product.ProductN.Contains))
        //                complytList.Add(product);

        //            // Дополнить: поиск в лоджикал листах
        //        }
        //        if (complytList.Count != 0)
        //        {
        //            foreach (Product product in complytList)
        //            {
        //                softwareVulnSoft.Add(software + " - " + product.VulnerableSoftwareList.Entry.EntryId);
        //            }
        //        }
        //    }
        //    return softwareVulnSoft;
        //}

        public static string checkValue(object input)
        {
            if (input != null)
                return input.ToString();
            else
                return string.Empty;
        }

        private static DataTable MakeTable(string dataTableName, List<string> columnNames,  string columNamePK)
        {
            DataTable newDataTable = new DataTable(dataTableName);

            for (int i = 0; i < columnNames.Count; i++)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.DataType = System.Type.GetType("System.String");
                dataColumn.ColumnName = columnNames[i];
                if (columnNames[i] == "Id")
                    dataColumn.AutoIncrement = true;
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

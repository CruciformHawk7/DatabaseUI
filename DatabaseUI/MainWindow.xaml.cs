using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        List<UIRecordObject> records = new List<UIRecordObject>();
        List<DBRecordObject> jsonRecords = new List<DBRecordObject>();
        List<AssetObject> assets = new List<AssetObject>();
        //temporarily hardcode the info
        string mainDB = @"D:\JS\testdb.json";
        string assetDB = @"D:\JS\testdb_locations.json";

        private void mainMake()
        {
            string op = JsonConvert.SerializeObject(records);
            System.IO.File.WriteAllText(mainDB, op);
        }

        private void Converter(List<UIRecordObject> inp)
        {
            jsonRecords.Clear();
            foreach (var item in inp)
            {
                int n = -1, o = -1;
                foreach (var p in assets)
                {
                    if (p.Description == item.id)
                    {
                        n = p.assetID;
                    }
                    if (p.Description == item.target)
                    {
                        o = p.assetID;
                    }
                }
                jsonRecords.Add(new DBRecordObject() {
                    id = n,
                    assetLocation =item.assetLocation,
                    direction =item.direction,
                    target = o
                });
            }
        }

        private void mainRead()
        {
            try
            {
                string text = System.IO.File.ReadAllText(mainDB);
                List<UIRecordObject> deserialisedProduct = JsonConvert.DeserializeObject<List<UIRecordObject>>(text);
                foreach (var item in deserialisedProduct)
                {
                    records.Add(item);
                }
                lstContents.Items.Clear();
                lstContents.Items.Add(deserialisedProduct);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("Directory Not Found!" + ex.Source);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("File not found!" + ex.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error. This should'nt have happened!\n" + ex.Message);
            }
            //handle more errors
        }

        private void assetMake()
        {
            string op = JsonConvert.SerializeObject(assets);
            System.IO.File.WriteAllText(assetDB, op);
        }

        private void assetRead()
        {
            try
            {
                string text = System.IO.File.ReadAllText(mainDB);
                List<AssetObject> deserialisedProduct = JsonConvert.DeserializeObject<List<AssetObject>>(text);
                foreach (var item in deserialisedProduct)
                {
                    assets.Add(item);
                }
                lstAssets.Items.Clear();
                lstAssets.Items.Add(deserialisedProduct);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("Directory Not Found!" + ex.Source);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("File not found!" + ex.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error. This should'nt have happened!\n" + ex.Message);
            }
            //handle more errors
        }

        public MainWindow()
        {
            InitializeComponent();

            assetRead();
            mainRead();

            //if (lstContents.SelectedIndex == -1) btnDelete.IsEnabled = false;

            foreach (var item in records)
            {
                cboID.Items.Add(item.id);
                cboTarget.Items.Add(item.target);
                cboID.SelectedIndex = 0;
                cboTarget.SelectedIndex = 0;
            }
           
            lstAssets.Items.Add(assets);

            if (cboID.SelectedValue == null) {
                cboID.IsEnabled = false;
            }
            if (cboTarget.SelectedValue == null) {
                cboTarget.IsEnabled = false;
            }
            
        }

        private void allcomments()
        {
            //lstContents.Items.Add(new UIRecordObject() { id = "Road", target = "UG Building", assetLocation = "UG.jpg", direction = Direction.North });
            //lstContents.Items.Add(new UIRecordObject() { id = "Road", target = "PG Building", assetLocation = "PG.jpg", direction = Direction.South });

            //records.Add(new UIRecordObject() { id = "Road", target = "UG Building", assetLocation = "UG.jpg", direction = Direction.North }); 


            //lstAssets.Items.Add(new AssetObject() { assetID = 0, Description = "Road" });
            //lstAssets.Items.Add(new AssetObject() { assetID = 1, Description = "UG Building" });
            //lstAssets.Items.Add(new AssetObject() { assetID = 2, Description = "PG Building" });

        }

        public void Converter(DBRecordObject dbRecordObject)
        {

        }

        public void Converter(UIRecordObject uiRecordObject)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            mainRead();

        }

        private void lstContents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstContents.SelectedIndex == -1) btnDelete.IsEnabled = false;
            else btnDelete.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            mainMake();
        }
    }

    public class UIRecordObject
    {
        //convert this crap back to ints, send a recordobject to addtolist
        public string id { get; set; }
        public string target { get; set; }
        public string assetLocation { get; set; }
        public Direction direction { get; set; }
    }

    public class DBRecordObject
    {
        public int id { get; set; }
        public int target { get; set; }
        public string assetLocation { get; set; }
        public Direction direction { get; set; }
    }

    class AssetObject
    {
        public int assetID { get; set; }
        public string Description { get; set; }
    }

    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }
}

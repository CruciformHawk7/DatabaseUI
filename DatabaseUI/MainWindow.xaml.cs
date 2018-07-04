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
        List<AssetObject> assets = new List<AssetObject>();
        string outputFile = @"C:\Users\Nikhil\Desktop\JS\testdb.json";

        private void makeString()
        {
            string op = JsonConvert.SerializeObject(records);
            System.IO.File.WriteAllText(outputFile, op);

        }

        private void readString()
        {
            string text = System.IO.File.ReadAllText(outputFile);
            List<UIRecordObject> deserialisedProduct = JsonConvert.DeserializeObject<List<UIRecordObject>> (text); //deserialisedProduct = records

            lstContents.Items.Clear();
            lstContents.Items.Add(deserialisedProduct);

            
        }

        public MainWindow()
        {
            InitializeComponent();

            //lstContents.Items.Add(new UIRecordObject() { id = "Road", target = "UG Building", assetLocation = "UG.jpg", direction = Direction.North });
            //lstContents.Items.Add(new UIRecordObject() { id = "Road", target = "PG Building", assetLocation = "PG.jpg", direction = Direction.South });

            records.Add(new UIRecordObject() { id = "Road", target = "UG Building", assetLocation = "UG.jpg", direction = Direction.North }); 
            lstContents.Items.Add(records);

            lstContents.Items.Clear();

            if (lstContents.SelectedIndex == -1) btnDelete.IsEnabled = false;

            foreach (var item in records)
            {
                cboID.Items.Add(item.id);
                cboTarget.Items.Add(item.target);
                cboID.SelectedIndex = 0;
                cboTarget.SelectedIndex = 0;
            }

            //lstAssets.Items.Add(new AssetObject() { assetID = 0, Description = "Road" });
            //lstAssets.Items.Add(new AssetObject() { assetID = 1, Description = "UG Building" });
            //lstAssets.Items.Add(new AssetObject() { assetID = 2, Description = "PG Building" });

            lstAssets.Items.Add(assets);

            if (cboID.SelectedValue == null) {
                cboID.IsEnabled = false;
            }
            if (cboTarget.SelectedValue == null) {
                cboTarget.IsEnabled = false;
            }
            
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
            readString();

        }

        private void lstContents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstContents.SelectedIndex == -1) btnDelete.IsEnabled = false;
            else btnDelete.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            makeString();
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

using DevExpress.Xpf.Map;
using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Globalization;

namespace BindMap {

    public class ShipInfo {
        public ShipInfo(double latitude, double longitude, string name, string description, int year) {
            this.Location = new GeoPoint(latitude, longitude);
            this.Name = name;
            this.Year = year;
            this.Description = description;
        }

        public GeoPoint Location { get; private set; }
        public string Name { get; private set; }
        public int Year { get; private set; }
        public string Description { get; private set; }
        public string Header { get { return Name + " (" + Year + ")"; } }
    }

    public static class DataLoader {
        public static XDocument LoadXmlFromResources(string fileName) {
            try {
                return XDocument.Load("/BindMap;component" + fileName);
            }
            catch {
                return null;
            }
        }
    }

    public partial class MainPage : UserControl {
        public ObservableCollection<ShipInfo> Ships { get; set; }
        public MainPage() {
            InitializeComponent();
            infoGrid.DataContext = this;
            Ships = new ObservableCollection<ShipInfo>();
            LoadDataFromXML();

        }

        void LoadDataFromXML() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Ships.xml");
            if (document != null) {
                foreach (XElement element in document.Element("Ships").Elements()) {
                    ShipInfo shipInfo = new ShipInfo(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture),
                        Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture),
                        element.Element("Name").Value,
                        element.Element("Description").Value,
                        Convert.ToInt16(element.Element("Year").Value));
                    Ships.Add(shipInfo);
                }
            }
        }
    }
}

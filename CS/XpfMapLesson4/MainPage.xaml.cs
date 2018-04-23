using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Xml.Linq;
using DevExpress.Xpf.Map;

namespace XpfMapLesson4 {
    #region #MainPageClass
    public partial class MainPage : UserControl {
        const String filepath = "Ships.xml";
        public ObservableCollection<ShipInfo> Ships { get; set; }

        public MainPage() {
            InitializeComponent();
            LayoutRoot.DataContext = this;
            Ships = LoadDataFromXml(filepath);
        }

        ObservableCollection<ShipInfo> LoadDataFromXml(String filename) {
            ObservableCollection<ShipInfo> ships = new ObservableCollection<ShipInfo>();

            XDocument document = XDocument.Load("XpfMapLesson4;component/" + filename);
            if (document != null) {
                foreach (XElement element in document.Element("Ships").Elements()) {
                    ShipInfo shipInfo = new ShipInfo(
                        Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture),
                        Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture),
                        element.Element("Name").Value,
                        element.Element("Description").Value,
                        Convert.ToInt16(element.Element("Year").Value));
                    ships.Add(shipInfo);
                }
            }

            return ships;
        }
    }
    #endregion #MainPageClass

    #region #ShipInfoClass
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
    }
    #endregion #ShipInfoClass
    
}

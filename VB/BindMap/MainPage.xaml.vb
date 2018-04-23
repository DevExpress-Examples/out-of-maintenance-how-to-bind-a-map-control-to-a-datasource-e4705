Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Map
Imports System
Imports System.Windows.Controls
Imports System.Collections.ObjectModel
Imports System.Xml.Linq
Imports System.Globalization

Namespace BindMap

	Public Class ShipInfo
		Public Sub New(ByVal latitude As Double, ByVal longitude As Double, ByVal name As String, ByVal description As String, ByVal year As Integer)
			Me.Location = New GeoPoint(latitude, longitude)
			Me.Name = name
			Me.Year = year
			Me.Description = description
		End Sub

		Private privateLocation As GeoPoint
		Public Property Location() As GeoPoint
			Get
				Return privateLocation
			End Get
			Private Set(ByVal value As GeoPoint)
				privateLocation = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Private Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateYear As Integer
		Public Property Year() As Integer
			Get
				Return privateYear
			End Get
			Private Set(ByVal value As Integer)
				privateYear = value
			End Set
		End Property
		Private privateDescription As String
		Public Property Description() As String
			Get
				Return privateDescription
			End Get
			Private Set(ByVal value As String)
				privateDescription = value
			End Set
		End Property
		Public ReadOnly Property Header() As String
			Get
				Return Name & " (" & Year & ")"
			End Get
		End Property
	End Class

	Public NotInheritable Class DataLoader
		Private Sub New()
		End Sub
		Public Shared Function LoadXmlFromResources(ByVal fileName As String) As XDocument
			Try
				Return XDocument.Load("/BindMap;component" & fileName)
			Catch
				Return Nothing
			End Try
		End Function
	End Class

	Partial Public Class MainPage
		Inherits UserControl
		Private privateShips As ObservableCollection(Of ShipInfo)
		Public Property Ships() As ObservableCollection(Of ShipInfo)
			Get
				Return privateShips
			End Get
			Set(ByVal value As ObservableCollection(Of ShipInfo))
				privateShips = value
			End Set
		End Property
		Public Sub New()
			InitializeComponent()
			infoGrid.DataContext = Me
			Ships = New ObservableCollection(Of ShipInfo)()
			LoadDataFromXML()

		End Sub

		Private Sub LoadDataFromXML()
			Dim document As XDocument = DataLoader.LoadXmlFromResources("/Data/Ships.xml")
			If document IsNot Nothing Then
				For Each element As XElement In document.Element("Ships").Elements()
					Dim shipInfo As New ShipInfo(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture), element.Element("Name").Value, element.Element("Description").Value, Convert.ToInt16(element.Element("Year").Value))
					Ships.Add(shipInfo)
				Next element
			End If
		End Sub
	End Class
End Namespace

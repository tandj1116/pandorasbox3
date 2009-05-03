using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using TheBox.Forms;
using TheBox.Common;
using TheBox.Controls;
using TheBox.Data;
using TheBox.BoxServer;
using TheBox.MapViewer.DrawObjects;

namespace TheBox
{
	/// <summary>
	/// Lists the small tabs available on the box form
	/// </summary>
	public enum SmallTabs
	{
		/// <summary>
		/// The art preview
		/// </summary>
		Art,
		/// <summary>
		/// The map preview
		/// </summary>
		Map,
		/// <summary>
		/// The props editor
		/// </summary>
		Props,
		/// <summary>
		/// The custom buttons tab
		/// </summary>
		Custom
	}

	/// <summary>
	/// Summary description for Box.
	/// </summary>
	public class Box : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl SmallTab;
		private System.Windows.Forms.NotifyIcon Tray;
		private System.Windows.Forms.ContextMenu TrayMenu;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem TrayBox;
		private System.Windows.Forms.MenuItem TrayOptions;
		private System.Windows.Forms.MenuItem TrayExit;
		private System.Windows.Forms.Button bSetHue;
		private System.Windows.Forms.PictureBox imgHue;
		private System.Windows.Forms.NumericUpDown numHue;
		private TheBox.Pages.Travel m_TravelTab;
		private System.Windows.Forms.TabPage tabArt;
		private System.Windows.Forms.TabPage tabMap;
		private System.Windows.Forms.TabPage tabProps;
		private System.Windows.Forms.TabControl BigTab;
		private System.Windows.Forms.TabPage TabTravel;
		private TheBox.Buttons.BoxButton boxButton1;
		private TheBox.Buttons.BoxButton boxButton2;
		private TheBox.Buttons.BoxButton boxButton3;
		private TheBox.Buttons.BoxButton boxButton4;
		private TheBox.Buttons.BoxButton boxButton5;
		private TheBox.Buttons.BoxButton boxButton6;
		private TheBox.Buttons.BoxButton boxButton7;
		private TheBox.Buttons.BoxButton boxButton8;
		private System.Windows.Forms.TabPage TabNPCs;
		private TheBox.Controls.PropManager ucPropManager;
		private TheBox.Pages.Mobiles m_PageMobiles;
		private System.Windows.Forms.TabPage TabProperties;
		private TheBox.Pages.Props m_PageProperties;
		private System.Windows.Forms.TabPage TabDeco;
		private System.Windows.Forms.TabPage TabItems;
		private System.Windows.Forms.TabPage TabNotes;
		private TheBox.Pages.Notes m_NotesTab;
		private TheBox.Pages.Items m_ItemsTab;
		private System.Windows.Forms.TabPage TabAdmin;
		private TheBox.Pages.Admin m_TabAdmin;
		private System.Windows.Forms.TabPage TabGeneral;
		private TheBox.Pages.General general1;
		private System.Windows.Forms.TabPage TabLights;
		private System.Windows.Forms.TabPage TabDoors;
		private TheBox.Pages.Doors DoorsTab;
		private TheBox.Pages.Lights LightsTab;
		private System.Windows.Forms.TabPage TabTools;
		private System.Windows.Forms.PictureBox pctCap;
		private TheBox.Pages.Tools m_Tools;
		private System.Windows.Forms.TabPage tabCustom;
		private TheBox.Buttons.BoxButton boxButton9;
		private TheBox.Buttons.BoxButton boxButton10;
		private TheBox.Buttons.BoxButton boxButton11;
		private TheBox.Buttons.BoxButton boxButton12;
		private TheBox.Buttons.BoxButton boxButton13;
		private TheBox.Buttons.BoxButton boxButton14;
		private TheBox.Buttons.BoxButton boxButton15;
		private TheBox.Buttons.BoxButton boxButton16;
		private TheBox.Buttons.BoxButton boxButton17;
		private TheBox.Buttons.BoxButton boxButton18;
		private TheBox.Buttons.BoxButton boxButton19;
		private TheBox.Buttons.BoxButton boxButton20;
		private TheBox.Buttons.BoxButton boxButton21;
		private TheBox.Buttons.BoxButton boxButton22;
		private TheBox.Buttons.BoxButton boxButton23;
		private System.Windows.Forms.MenuItem miProfile;
		private System.Windows.Forms.Label bMenu;
		private System.Windows.Forms.ImageList boxImgLst;
		private System.Windows.Forms.MenuItem miAbout;
		private System.Windows.Forms.MenuItem menuItem1;
		private TheBox.Pages.Deco m_TabDeco;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem miViewDataFolder;
		private System.Windows.Forms.MenuItem miViewLog;
		private TheBox.ArtViewer.ArtViewer Art;
		private TheBox.MapViewer.MapViewer Map;
		private System.ComponentModel.IContainer components;

		#region Pages

		/// <summary>
		/// Gets the travel user control
		/// </summary>
		public TheBox.Pages.Travel Travel
		{
			get { return m_TravelTab; }
		}

		/// <summary>
		/// Gets the Mobiles user control
		/// </summary>
		public TheBox.Pages.Mobiles Mobiles
		{
			get { return m_PageMobiles; }
		}

		/// <summary>
		/// Gets the page displaying all the properties
		/// </summary>
		public TheBox.Pages.Props Properties
		{
			get { return m_PageProperties; }
		}

		#endregion

		public Box()
		{
			Splash.SetStatusText( "Loading appearance" );
			InitializeComponent();

			Splash.SetStatusText( "Initializing maps and artwork" );
			Map.MulManager = Pandora.Profile.MulManager;
			Art.MulFileManager = Pandora.Profile.MulManager;
			
			Pandora.Map = Map;
			Pandora.Art = Art;
			Pandora.Prop = ucPropManager;

			if ( Pandora.Hues != null )
			{
				Splash.SetStatusText( "Reading hues" );
				m_HuePicker = new HuePicker();
			}

			Splash.SetStatusText( "Restoring options" );
			ApplyOptions();

			Splash.SetStatusText( "Building travel destinations" );
			InitPages();
		}

		/// <summary>
		/// Applies the options in the profile
		/// </summary>
		private void ApplyOptions()
		{
			TheBox.Options.Profile p = Pandora.Profile;

			TopMost = p.General.TopMost;
			Opacity = p.General.Opacity / 100.0;

			// Hue
			if ( Pandora.Hues != null )
			{
				numHue.Value = p.Hues.SelectedIndex;

				if ( p.Hues.SelectedIndex != 0 )
					imgHue.Image = Pandora.Hues[ p.Hues.SelectedIndex ].GetSpectrum( imgHue.Size );

				m_HuesMenu = new RecentHuesMenu( p.Hues.RecentHues );
				m_HuesMenu.HueClicked += new EventHandler(m_HuesMenu_HueClicked);
			}
			else
			{
				imgHue.Enabled = false;
				numHue.Enabled = false;
				bSetHue.Enabled = false;
			}

			// Map
			Map.DrawStatics = p.Travel.DrawStatics;
			Map.XRayView = p.Travel.XRayView;
		}

		/// <summary>
		/// Initializes the pages that require a special initialization
		/// </summary>
		private void InitPages()
		{
			m_TravelTab.Init();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Box));
			this.BigTab = new System.Windows.Forms.TabControl();
			this.TabGeneral = new System.Windows.Forms.TabPage();
			this.general1 = new TheBox.Pages.General();
			this.TabDeco = new System.Windows.Forms.TabPage();
			this.m_TabDeco = new TheBox.Pages.Deco();
			this.TabTravel = new System.Windows.Forms.TabPage();
			this.m_TravelTab = new TheBox.Pages.Travel();
			this.TabProperties = new System.Windows.Forms.TabPage();
			this.m_PageProperties = new TheBox.Pages.Props();
			this.TabItems = new System.Windows.Forms.TabPage();
			this.m_ItemsTab = new TheBox.Pages.Items();
			this.TabNPCs = new System.Windows.Forms.TabPage();
			this.m_PageMobiles = new TheBox.Pages.Mobiles();
			this.TabAdmin = new System.Windows.Forms.TabPage();
			this.m_TabAdmin = new TheBox.Pages.Admin();
			this.TabTools = new System.Windows.Forms.TabPage();
			this.m_Tools = new TheBox.Pages.Tools();
			this.TabDoors = new System.Windows.Forms.TabPage();
			this.DoorsTab = new TheBox.Pages.Doors();
			this.TabLights = new System.Windows.Forms.TabPage();
			this.LightsTab = new TheBox.Pages.Lights();
			this.TabNotes = new System.Windows.Forms.TabPage();
			this.m_NotesTab = new TheBox.Pages.Notes();
			this.SmallTab = new System.Windows.Forms.TabControl();
			this.tabArt = new System.Windows.Forms.TabPage();
			this.Art = new TheBox.ArtViewer.ArtViewer();
			this.tabMap = new System.Windows.Forms.TabPage();
			this.Map = new TheBox.MapViewer.MapViewer();
			this.tabProps = new System.Windows.Forms.TabPage();
			this.ucPropManager = new TheBox.Controls.PropManager();
			this.tabCustom = new System.Windows.Forms.TabPage();
			this.boxButton23 = new TheBox.Buttons.BoxButton();
			this.boxButton22 = new TheBox.Buttons.BoxButton();
			this.boxButton21 = new TheBox.Buttons.BoxButton();
			this.boxButton20 = new TheBox.Buttons.BoxButton();
			this.boxButton19 = new TheBox.Buttons.BoxButton();
			this.boxButton18 = new TheBox.Buttons.BoxButton();
			this.boxButton17 = new TheBox.Buttons.BoxButton();
			this.boxButton16 = new TheBox.Buttons.BoxButton();
			this.boxButton15 = new TheBox.Buttons.BoxButton();
			this.boxButton14 = new TheBox.Buttons.BoxButton();
			this.boxButton13 = new TheBox.Buttons.BoxButton();
			this.boxButton12 = new TheBox.Buttons.BoxButton();
			this.boxButton11 = new TheBox.Buttons.BoxButton();
			this.boxButton10 = new TheBox.Buttons.BoxButton();
			this.boxButton9 = new TheBox.Buttons.BoxButton();
			this.bSetHue = new System.Windows.Forms.Button();
			this.Tray = new System.Windows.Forms.NotifyIcon(this.components);
			this.TrayMenu = new System.Windows.Forms.ContextMenu();
			this.TrayBox = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.TrayOptions = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.miViewDataFolder = new System.Windows.Forms.MenuItem();
			this.miViewLog = new System.Windows.Forms.MenuItem();
			this.miProfile = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miAbout = new System.Windows.Forms.MenuItem();
			this.TrayExit = new System.Windows.Forms.MenuItem();
			this.imgHue = new System.Windows.Forms.PictureBox();
			this.numHue = new System.Windows.Forms.NumericUpDown();
			this.boxButton1 = new TheBox.Buttons.BoxButton();
			this.boxButton2 = new TheBox.Buttons.BoxButton();
			this.boxButton3 = new TheBox.Buttons.BoxButton();
			this.boxButton4 = new TheBox.Buttons.BoxButton();
			this.boxButton5 = new TheBox.Buttons.BoxButton();
			this.boxButton6 = new TheBox.Buttons.BoxButton();
			this.boxButton7 = new TheBox.Buttons.BoxButton();
			this.boxButton8 = new TheBox.Buttons.BoxButton();
			this.pctCap = new System.Windows.Forms.PictureBox();
			this.bMenu = new System.Windows.Forms.Label();
			this.boxImgLst = new System.Windows.Forms.ImageList(this.components);
			this.BigTab.SuspendLayout();
			this.TabGeneral.SuspendLayout();
			this.TabDeco.SuspendLayout();
			this.TabTravel.SuspendLayout();
			this.TabProperties.SuspendLayout();
			this.TabItems.SuspendLayout();
			this.TabNPCs.SuspendLayout();
			this.TabAdmin.SuspendLayout();
			this.TabTools.SuspendLayout();
			this.TabDoors.SuspendLayout();
			this.TabLights.SuspendLayout();
			this.TabNotes.SuspendLayout();
			this.SmallTab.SuspendLayout();
			this.tabArt.SuspendLayout();
			this.tabMap.SuspendLayout();
			this.tabProps.SuspendLayout();
			this.tabCustom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numHue)).BeginInit();
			this.SuspendLayout();
			// 
			// BigTab
			// 
			this.BigTab.Controls.Add(this.TabGeneral);
			this.BigTab.Controls.Add(this.TabDeco);
			this.BigTab.Controls.Add(this.TabTravel);
			this.BigTab.Controls.Add(this.TabProperties);
			this.BigTab.Controls.Add(this.TabItems);
			this.BigTab.Controls.Add(this.TabNPCs);
			this.BigTab.Controls.Add(this.TabAdmin);
			this.BigTab.Controls.Add(this.TabTools);
			this.BigTab.Controls.Add(this.TabDoors);
			this.BigTab.Controls.Add(this.TabLights);
			this.BigTab.Controls.Add(this.TabNotes);
			this.BigTab.ItemSize = new System.Drawing.Size(42, 16);
			this.BigTab.Location = new System.Drawing.Point(0, 20);
			this.BigTab.Name = "BigTab";
			this.BigTab.SelectedIndex = 0;
			this.BigTab.Size = new System.Drawing.Size(504, 166);
			this.BigTab.TabIndex = 0;
			this.BigTab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Box_KeyDown);
			this.BigTab.SelectedIndexChanged += new System.EventHandler(this.BigTab_SelectedIndexChanged);
			// 
			// TabGeneral
			// 
			this.TabGeneral.Controls.Add(this.general1);
			this.TabGeneral.Location = new System.Drawing.Point(4, 20);
			this.TabGeneral.Name = "TabGeneral";
			this.TabGeneral.Size = new System.Drawing.Size(496, 142);
			this.TabGeneral.TabIndex = 7;
			this.TabGeneral.Text = "Tabs.General";
			// 
			// general1
			// 
			this.general1.Location = new System.Drawing.Point(0, 0);
			this.general1.Name = "general1";
			this.general1.Size = new System.Drawing.Size(496, 142);
			this.general1.TabIndex = 0;
			// 
			// TabDeco
			// 
			this.TabDeco.Controls.Add(this.m_TabDeco);
			this.TabDeco.Location = new System.Drawing.Point(4, 20);
			this.TabDeco.Name = "TabDeco";
			this.TabDeco.Size = new System.Drawing.Size(496, 142);
			this.TabDeco.TabIndex = 3;
			this.TabDeco.Text = "Tabs.Deco";
			// 
			// m_TabDeco
			// 
			this.m_TabDeco.Location = new System.Drawing.Point(0, 0);
			this.m_TabDeco.Name = "m_TabDeco";
			this.m_TabDeco.Size = new System.Drawing.Size(496, 142);
			this.m_TabDeco.TabIndex = 0;
			// 
			// TabTravel
			// 
			this.TabTravel.Controls.Add(this.m_TravelTab);
			this.TabTravel.Location = new System.Drawing.Point(4, 20);
			this.TabTravel.Name = "TabTravel";
			this.TabTravel.Size = new System.Drawing.Size(496, 142);
			this.TabTravel.TabIndex = 0;
			this.TabTravel.Text = "Tabs.Travel";
			// 
			// m_TravelTab
			// 
			this.m_TravelTab.Location = new System.Drawing.Point(0, 0);
			this.m_TravelTab.Name = "m_TravelTab";
			this.m_TravelTab.Size = new System.Drawing.Size(496, 142);
			this.m_TravelTab.TabIndex = 0;
			// 
			// TabProperties
			// 
			this.TabProperties.Controls.Add(this.m_PageProperties);
			this.TabProperties.Location = new System.Drawing.Point(4, 20);
			this.TabProperties.Name = "TabProperties";
			this.TabProperties.Size = new System.Drawing.Size(496, 142);
			this.TabProperties.TabIndex = 2;
			this.TabProperties.Text = "Tabs.Props";
			// 
			// m_PageProperties
			// 
			this.m_PageProperties.Location = new System.Drawing.Point(0, 0);
			this.m_PageProperties.Name = "m_PageProperties";
			this.m_PageProperties.SelectedProperty = null;
			this.m_PageProperties.Size = new System.Drawing.Size(496, 142);
			this.m_PageProperties.TabIndex = 0;
			// 
			// TabItems
			// 
			this.TabItems.Controls.Add(this.m_ItemsTab);
			this.TabItems.Location = new System.Drawing.Point(4, 20);
			this.TabItems.Name = "TabItems";
			this.TabItems.Size = new System.Drawing.Size(496, 142);
			this.TabItems.TabIndex = 4;
			this.TabItems.Text = "Tabs.Items";
			// 
			// m_ItemsTab
			// 
			this.m_ItemsTab.Location = new System.Drawing.Point(0, 0);
			this.m_ItemsTab.Name = "m_ItemsTab";
			this.m_ItemsTab.Size = new System.Drawing.Size(496, 142);
			this.m_ItemsTab.TabIndex = 0;
			// 
			// TabNPCs
			// 
			this.TabNPCs.Controls.Add(this.m_PageMobiles);
			this.TabNPCs.Location = new System.Drawing.Point(4, 20);
			this.TabNPCs.Name = "TabNPCs";
			this.TabNPCs.Size = new System.Drawing.Size(496, 142);
			this.TabNPCs.TabIndex = 1;
			this.TabNPCs.Text = "Tabs.NPCs";
			// 
			// m_PageMobiles
			// 
			this.m_PageMobiles.Location = new System.Drawing.Point(0, 0);
			this.m_PageMobiles.Name = "m_PageMobiles";
			this.m_PageMobiles.Size = new System.Drawing.Size(496, 142);
			this.m_PageMobiles.TabIndex = 0;
			// 
			// TabAdmin
			// 
			this.TabAdmin.Controls.Add(this.m_TabAdmin);
			this.TabAdmin.Location = new System.Drawing.Point(4, 20);
			this.TabAdmin.Name = "TabAdmin";
			this.TabAdmin.Size = new System.Drawing.Size(496, 142);
			this.TabAdmin.TabIndex = 6;
			this.TabAdmin.Text = "Tabs.Admin";
			// 
			// m_TabAdmin
			// 
			this.m_TabAdmin.Location = new System.Drawing.Point(0, 0);
			this.m_TabAdmin.Name = "m_TabAdmin";
			this.m_TabAdmin.Size = new System.Drawing.Size(496, 142);
			this.m_TabAdmin.TabIndex = 0;
			// 
			// TabTools
			// 
			this.TabTools.Controls.Add(this.m_Tools);
			this.TabTools.Location = new System.Drawing.Point(4, 20);
			this.TabTools.Name = "TabTools";
			this.TabTools.Size = new System.Drawing.Size(496, 142);
			this.TabTools.TabIndex = 10;
			this.TabTools.Text = "Tabs.Tools";
			// 
			// m_Tools
			// 
			this.m_Tools.Location = new System.Drawing.Point(0, 0);
			this.m_Tools.Name = "m_Tools";
			this.m_Tools.Size = new System.Drawing.Size(496, 142);
			this.m_Tools.TabIndex = 0;
			// 
			// TabDoors
			// 
			this.TabDoors.Controls.Add(this.DoorsTab);
			this.TabDoors.Location = new System.Drawing.Point(4, 20);
			this.TabDoors.Name = "TabDoors";
			this.TabDoors.Size = new System.Drawing.Size(496, 142);
			this.TabDoors.TabIndex = 9;
			this.TabDoors.Text = "Tabs.Doors";
			// 
			// DoorsTab
			// 
			this.DoorsTab.Location = new System.Drawing.Point(0, 0);
			this.DoorsTab.Name = "DoorsTab";
			this.DoorsTab.Size = new System.Drawing.Size(496, 142);
			this.DoorsTab.TabIndex = 0;
			// 
			// TabLights
			// 
			this.TabLights.Controls.Add(this.LightsTab);
			this.TabLights.Location = new System.Drawing.Point(4, 20);
			this.TabLights.Name = "TabLights";
			this.TabLights.Size = new System.Drawing.Size(496, 142);
			this.TabLights.TabIndex = 8;
			this.TabLights.Text = "Tabs.Lights";
			// 
			// LightsTab
			// 
			this.LightsTab.Location = new System.Drawing.Point(0, 0);
			this.LightsTab.Name = "LightsTab";
			this.LightsTab.Size = new System.Drawing.Size(496, 142);
			this.LightsTab.TabIndex = 0;
			// 
			// TabNotes
			// 
			this.TabNotes.Controls.Add(this.m_NotesTab);
			this.TabNotes.Location = new System.Drawing.Point(4, 20);
			this.TabNotes.Name = "TabNotes";
			this.TabNotes.Size = new System.Drawing.Size(496, 142);
			this.TabNotes.TabIndex = 5;
			this.TabNotes.Text = "Tabs.Notes";
			// 
			// m_NotesTab
			// 
			this.m_NotesTab.Location = new System.Drawing.Point(0, 0);
			this.m_NotesTab.Name = "m_NotesTab";
			this.m_NotesTab.Size = new System.Drawing.Size(496, 142);
			this.m_NotesTab.TabIndex = 0;
			// 
			// SmallTab
			// 
			this.SmallTab.Controls.Add(this.tabArt);
			this.SmallTab.Controls.Add(this.tabMap);
			this.SmallTab.Controls.Add(this.tabProps);
			this.SmallTab.Controls.Add(this.tabCustom);
			this.SmallTab.ItemSize = new System.Drawing.Size(42, 16);
			this.SmallTab.Location = new System.Drawing.Point(504, 20);
			this.SmallTab.Name = "SmallTab";
			this.SmallTab.SelectedIndex = 0;
			this.SmallTab.Size = new System.Drawing.Size(184, 166);
			this.SmallTab.TabIndex = 1;
			this.SmallTab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Box_KeyDown);
			// 
			// tabArt
			// 
			this.tabArt.Controls.Add(this.Art);
			this.tabArt.Location = new System.Drawing.Point(4, 20);
			this.tabArt.Name = "tabArt";
			this.tabArt.Size = new System.Drawing.Size(176, 142);
			this.tabArt.TabIndex = 0;
			this.tabArt.Text = "Tabs.Art";
			// 
			// Art
			// 
			this.Art.Animate = false;
			this.Art.Art = TheBox.ArtViewer.Art.Items;
			this.Art.ArtIndex = 0;
			this.Art.BackColor = System.Drawing.Color.White;
			this.Art.Hue = 0;
			this.Art.Location = new System.Drawing.Point(1, 1);
			this.Art.Name = "Art";
			this.Art.ResizeTallItems = false;
			this.Art.RoomView = true;
			this.Art.ShowID = true;
			this.Art.Size = new System.Drawing.Size(174, 140);
			this.Art.TabIndex = 0;
			this.Art.Text = "artViewer1";
			// 
			// tabMap
			// 
			this.tabMap.Controls.Add(this.Map);
			this.tabMap.Location = new System.Drawing.Point(4, 20);
			this.tabMap.Name = "tabMap";
			this.tabMap.Size = new System.Drawing.Size(176, 142);
			this.tabMap.TabIndex = 1;
			this.tabMap.Text = "Tabs.Map";
			// 
			// Map
			// 
			this.Map.Center = new System.Drawing.Point(0, 0);
			this.Map.DisplayErrors = true;
			this.Map.DrawStatics = false;
			this.Map.Location = new System.Drawing.Point(1, 1);
			this.Map.Map = TheBox.MapViewer.Maps.Felucca;
			this.Map.Name = "Map";
			this.Map.Navigation = TheBox.MapViewer.MapNavigation.LeftMouseButton;
			this.Map.RotateView = false;
			this.Map.ShowCross = true;
			this.Map.Size = new System.Drawing.Size(174, 140);
			this.Map.TabIndex = 0;
			this.Map.Text = "mapViewer1";
			this.Map.WheelZoom = true;
			this.Map.XRayView = false;
			this.Map.ZoomLevel = 0;
			this.Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_MouseDown);
			// 
			// tabProps
			// 
			this.tabProps.Controls.Add(this.ucPropManager);
			this.tabProps.Location = new System.Drawing.Point(4, 20);
			this.tabProps.Name = "tabProps";
			this.tabProps.Size = new System.Drawing.Size(176, 142);
			this.tabProps.TabIndex = 2;
			this.tabProps.Text = "Tabs.Props";
			// 
			// ucPropManager
			// 
			this.ucPropManager.Location = new System.Drawing.Point(0, 0);
			this.ucPropManager.Name = "ucPropManager";
			this.ucPropManager.Size = new System.Drawing.Size(176, 144);
			this.ucPropManager.TabIndex = 0;
			// 
			// tabCustom
			// 
			this.tabCustom.Controls.Add(this.boxButton23);
			this.tabCustom.Controls.Add(this.boxButton22);
			this.tabCustom.Controls.Add(this.boxButton21);
			this.tabCustom.Controls.Add(this.boxButton20);
			this.tabCustom.Controls.Add(this.boxButton19);
			this.tabCustom.Controls.Add(this.boxButton18);
			this.tabCustom.Controls.Add(this.boxButton17);
			this.tabCustom.Controls.Add(this.boxButton16);
			this.tabCustom.Controls.Add(this.boxButton15);
			this.tabCustom.Controls.Add(this.boxButton14);
			this.tabCustom.Controls.Add(this.boxButton13);
			this.tabCustom.Controls.Add(this.boxButton12);
			this.tabCustom.Controls.Add(this.boxButton11);
			this.tabCustom.Controls.Add(this.boxButton10);
			this.tabCustom.Controls.Add(this.boxButton9);
			this.tabCustom.Location = new System.Drawing.Point(4, 20);
			this.tabCustom.Name = "tabCustom";
			this.tabCustom.Size = new System.Drawing.Size(176, 142);
			this.tabCustom.TabIndex = 3;
			this.tabCustom.Text = "Tabs.Custom";
			// 
			// boxButton23
			// 
			this.boxButton23.AllowEdit = true;
			this.boxButton23.ButtonID = 118;
			this.boxButton23.Def = null;
			this.boxButton23.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton23.IsActive = true;
			this.boxButton23.Location = new System.Drawing.Point(120, 116);
			this.boxButton23.Name = "boxButton23";
			this.boxButton23.Size = new System.Drawing.Size(56, 23);
			this.boxButton23.TabIndex = 14;
			this.boxButton23.Text = "boxButton23";
			// 
			// boxButton22
			// 
			this.boxButton22.AllowEdit = true;
			this.boxButton22.ButtonID = 117;
			this.boxButton22.Def = null;
			this.boxButton22.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton22.IsActive = true;
			this.boxButton22.Location = new System.Drawing.Point(120, 88);
			this.boxButton22.Name = "boxButton22";
			this.boxButton22.Size = new System.Drawing.Size(56, 23);
			this.boxButton22.TabIndex = 13;
			this.boxButton22.Text = "boxButton22";
			// 
			// boxButton21
			// 
			this.boxButton21.AllowEdit = true;
			this.boxButton21.ButtonID = 116;
			this.boxButton21.Def = null;
			this.boxButton21.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton21.IsActive = true;
			this.boxButton21.Location = new System.Drawing.Point(120, 60);
			this.boxButton21.Name = "boxButton21";
			this.boxButton21.Size = new System.Drawing.Size(56, 23);
			this.boxButton21.TabIndex = 12;
			this.boxButton21.Text = "boxButton21";
			// 
			// boxButton20
			// 
			this.boxButton20.AllowEdit = true;
			this.boxButton20.ButtonID = 115;
			this.boxButton20.Def = null;
			this.boxButton20.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton20.IsActive = true;
			this.boxButton20.Location = new System.Drawing.Point(120, 32);
			this.boxButton20.Name = "boxButton20";
			this.boxButton20.Size = new System.Drawing.Size(56, 23);
			this.boxButton20.TabIndex = 11;
			this.boxButton20.Text = "boxButton20";
			// 
			// boxButton19
			// 
			this.boxButton19.AllowEdit = true;
			this.boxButton19.ButtonID = 114;
			this.boxButton19.Def = null;
			this.boxButton19.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton19.IsActive = true;
			this.boxButton19.Location = new System.Drawing.Point(60, 60);
			this.boxButton19.Name = "boxButton19";
			this.boxButton19.Size = new System.Drawing.Size(56, 23);
			this.boxButton19.TabIndex = 10;
			this.boxButton19.Text = "boxButton19";
			// 
			// boxButton18
			// 
			this.boxButton18.AllowEdit = true;
			this.boxButton18.ButtonID = 113;
			this.boxButton18.Def = null;
			this.boxButton18.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton18.IsActive = true;
			this.boxButton18.Location = new System.Drawing.Point(60, 116);
			this.boxButton18.Name = "boxButton18";
			this.boxButton18.Size = new System.Drawing.Size(56, 23);
			this.boxButton18.TabIndex = 9;
			this.boxButton18.Text = "boxButton18";
			// 
			// boxButton17
			// 
			this.boxButton17.AllowEdit = true;
			this.boxButton17.ButtonID = 112;
			this.boxButton17.Def = null;
			this.boxButton17.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton17.IsActive = true;
			this.boxButton17.Location = new System.Drawing.Point(60, 88);
			this.boxButton17.Name = "boxButton17";
			this.boxButton17.Size = new System.Drawing.Size(56, 23);
			this.boxButton17.TabIndex = 8;
			this.boxButton17.Text = "boxButton17";
			// 
			// boxButton16
			// 
			this.boxButton16.AllowEdit = true;
			this.boxButton16.ButtonID = 111;
			this.boxButton16.Def = null;
			this.boxButton16.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton16.IsActive = true;
			this.boxButton16.Location = new System.Drawing.Point(0, 116);
			this.boxButton16.Name = "boxButton16";
			this.boxButton16.Size = new System.Drawing.Size(56, 23);
			this.boxButton16.TabIndex = 7;
			this.boxButton16.Text = "boxButton16";
			// 
			// boxButton15
			// 
			this.boxButton15.AllowEdit = true;
			this.boxButton15.ButtonID = 110;
			this.boxButton15.Def = null;
			this.boxButton15.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton15.IsActive = true;
			this.boxButton15.Location = new System.Drawing.Point(60, 32);
			this.boxButton15.Name = "boxButton15";
			this.boxButton15.Size = new System.Drawing.Size(56, 23);
			this.boxButton15.TabIndex = 6;
			this.boxButton15.Text = "boxButton15";
			// 
			// boxButton14
			// 
			this.boxButton14.AllowEdit = true;
			this.boxButton14.ButtonID = 109;
			this.boxButton14.Def = null;
			this.boxButton14.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton14.IsActive = true;
			this.boxButton14.Location = new System.Drawing.Point(0, 88);
			this.boxButton14.Name = "boxButton14";
			this.boxButton14.Size = new System.Drawing.Size(56, 23);
			this.boxButton14.TabIndex = 5;
			this.boxButton14.Text = "boxButton14";
			// 
			// boxButton13
			// 
			this.boxButton13.AllowEdit = true;
			this.boxButton13.ButtonID = 108;
			this.boxButton13.Def = null;
			this.boxButton13.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton13.IsActive = true;
			this.boxButton13.Location = new System.Drawing.Point(0, 60);
			this.boxButton13.Name = "boxButton13";
			this.boxButton13.Size = new System.Drawing.Size(56, 23);
			this.boxButton13.TabIndex = 4;
			this.boxButton13.Text = "boxButton13";
			// 
			// boxButton12
			// 
			this.boxButton12.AllowEdit = true;
			this.boxButton12.ButtonID = 107;
			this.boxButton12.Def = null;
			this.boxButton12.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton12.IsActive = true;
			this.boxButton12.Location = new System.Drawing.Point(0, 32);
			this.boxButton12.Name = "boxButton12";
			this.boxButton12.Size = new System.Drawing.Size(56, 23);
			this.boxButton12.TabIndex = 3;
			this.boxButton12.Text = "boxButton12";
			// 
			// boxButton11
			// 
			this.boxButton11.AllowEdit = true;
			this.boxButton11.ButtonID = 106;
			this.boxButton11.Def = null;
			this.boxButton11.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton11.IsActive = true;
			this.boxButton11.Location = new System.Drawing.Point(120, 4);
			this.boxButton11.Name = "boxButton11";
			this.boxButton11.Size = new System.Drawing.Size(56, 23);
			this.boxButton11.TabIndex = 2;
			this.boxButton11.Text = "boxButton11";
			// 
			// boxButton10
			// 
			this.boxButton10.AllowEdit = true;
			this.boxButton10.ButtonID = 105;
			this.boxButton10.Def = null;
			this.boxButton10.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton10.IsActive = true;
			this.boxButton10.Location = new System.Drawing.Point(60, 4);
			this.boxButton10.Name = "boxButton10";
			this.boxButton10.Size = new System.Drawing.Size(56, 23);
			this.boxButton10.TabIndex = 1;
			this.boxButton10.Text = "boxButton10";
			// 
			// boxButton9
			// 
			this.boxButton9.AllowEdit = true;
			this.boxButton9.ButtonID = 104;
			this.boxButton9.Def = null;
			this.boxButton9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton9.IsActive = true;
			this.boxButton9.Location = new System.Drawing.Point(0, 4);
			this.boxButton9.Name = "boxButton9";
			this.boxButton9.Size = new System.Drawing.Size(56, 23);
			this.boxButton9.TabIndex = 0;
			this.boxButton9.Text = "boxButton9";
			// 
			// bSetHue
			// 
			this.bSetHue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bSetHue.Location = new System.Drawing.Point(112, 0);
			this.bSetHue.Name = "bSetHue";
			this.bSetHue.Size = new System.Drawing.Size(32, 20);
			this.bSetHue.TabIndex = 3;
			this.bSetHue.Text = "Common.Set";
			this.bSetHue.Click += new System.EventHandler(this.bSetHue_Click);
			// 
			// Tray
			// 
			this.Tray.ContextMenu = this.TrayMenu;
			this.Tray.Icon = ((System.Drawing.Icon)(resources.GetObject("Tray.Icon")));
			this.Tray.Text = "Pandora\'s Box";
			this.Tray.Visible = true;
			this.Tray.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tray_MouseDown);
			// 
			// TrayMenu
			// 
			this.TrayMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											this.TrayBox,
																											this.menuItem2,
																											this.TrayOptions,
																											this.menuItem3,
																											this.miProfile,
																											this.menuItem1,
																											this.miAbout,
																											this.TrayExit});
			// 
			// TrayBox
			// 
			this.TrayBox.Index = 0;
			this.TrayBox.Text = "Misc.www";
			this.TrayBox.Click += new System.EventHandler(this.TrayBox_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// TrayOptions
			// 
			this.TrayOptions.Index = 2;
			this.TrayOptions.Text = "Common.Options";
			this.TrayOptions.Click += new System.EventHandler(this.TrayOptions_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											 this.miViewDataFolder,
																											 this.miViewLog});
			this.menuItem3.Text = "Common.View";
			// 
			// miViewDataFolder
			// 
			this.miViewDataFolder.Index = 0;
			this.miViewDataFolder.Text = "Misc.DataFolder";
			this.miViewDataFolder.Click += new System.EventHandler(this.miViewDataFolder_Click);
			// 
			// miViewLog
			// 
			this.miViewLog.Index = 1;
			this.miViewLog.Text = "Misc.Log";
			this.miViewLog.Click += new System.EventHandler(this.miViewLog_Click);
			// 
			// miProfile
			// 
			this.miProfile.Index = 4;
			this.miProfile.Text = "Common.Profile";
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 5;
			this.menuItem1.Text = "-";
			// 
			// miAbout
			// 
			this.miAbout.Index = 6;
			this.miAbout.Text = "Common.About";
			this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
			// 
			// TrayExit
			// 
			this.TrayExit.Index = 7;
			this.TrayExit.Text = "Common.Exit";
			this.TrayExit.Click += new System.EventHandler(this.TrayExit_Click);
			// 
			// imgHue
			// 
			this.imgHue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.imgHue.Location = new System.Drawing.Point(72, 2);
			this.imgHue.Name = "imgHue";
			this.imgHue.Size = new System.Drawing.Size(38, 16);
			this.imgHue.TabIndex = 5;
			this.imgHue.TabStop = false;
			this.imgHue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgHue_MouseDown);
			// 
			// numHue
			// 
			this.numHue.Location = new System.Drawing.Point(24, 0);
			this.numHue.Maximum = new System.Decimal(new int[] {
																					3000,
																					0,
																					0,
																					0});
			this.numHue.Name = "numHue";
			this.numHue.Size = new System.Drawing.Size(48, 20);
			this.numHue.TabIndex = 6;
			this.numHue.ValueChanged += new System.EventHandler(this.numHue_ValueChanged);
			// 
			// boxButton1
			// 
			this.boxButton1.AllowEdit = true;
			this.boxButton1.ButtonID = 9;
			this.boxButton1.Def = null;
			this.boxButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton1.IsActive = true;
			this.boxButton1.Location = new System.Drawing.Point(144, 0);
			this.boxButton1.Name = "boxButton1";
			this.boxButton1.Size = new System.Drawing.Size(64, 20);
			this.boxButton1.TabIndex = 7;
			this.boxButton1.Text = "boxButton1";
			// 
			// boxButton2
			// 
			this.boxButton2.AllowEdit = true;
			this.boxButton2.ButtonID = 28;
			this.boxButton2.Def = null;
			this.boxButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton2.IsActive = true;
			this.boxButton2.Location = new System.Drawing.Point(208, 0);
			this.boxButton2.Name = "boxButton2";
			this.boxButton2.Size = new System.Drawing.Size(64, 20);
			this.boxButton2.TabIndex = 8;
			this.boxButton2.Text = "boxButton2";
			// 
			// boxButton3
			// 
			this.boxButton3.AllowEdit = true;
			this.boxButton3.ButtonID = 29;
			this.boxButton3.Def = null;
			this.boxButton3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton3.IsActive = true;
			this.boxButton3.Location = new System.Drawing.Point(272, 0);
			this.boxButton3.Name = "boxButton3";
			this.boxButton3.Size = new System.Drawing.Size(64, 20);
			this.boxButton3.TabIndex = 9;
			this.boxButton3.Text = "boxButton3";
			// 
			// boxButton4
			// 
			this.boxButton4.AllowEdit = true;
			this.boxButton4.ButtonID = 30;
			this.boxButton4.Def = null;
			this.boxButton4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton4.IsActive = true;
			this.boxButton4.Location = new System.Drawing.Point(336, 0);
			this.boxButton4.Name = "boxButton4";
			this.boxButton4.Size = new System.Drawing.Size(64, 20);
			this.boxButton4.TabIndex = 10;
			this.boxButton4.Text = "boxButton4";
			// 
			// boxButton5
			// 
			this.boxButton5.AllowEdit = true;
			this.boxButton5.ButtonID = 31;
			this.boxButton5.Def = null;
			this.boxButton5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton5.IsActive = true;
			this.boxButton5.Location = new System.Drawing.Point(400, 0);
			this.boxButton5.Name = "boxButton5";
			this.boxButton5.Size = new System.Drawing.Size(64, 20);
			this.boxButton5.TabIndex = 11;
			this.boxButton5.Text = "boxButton5";
			// 
			// boxButton6
			// 
			this.boxButton6.AllowEdit = true;
			this.boxButton6.ButtonID = 32;
			this.boxButton6.Def = null;
			this.boxButton6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton6.IsActive = true;
			this.boxButton6.Location = new System.Drawing.Point(464, 0);
			this.boxButton6.Name = "boxButton6";
			this.boxButton6.Size = new System.Drawing.Size(64, 20);
			this.boxButton6.TabIndex = 12;
			this.boxButton6.Text = "boxButton6";
			// 
			// boxButton7
			// 
			this.boxButton7.AllowEdit = true;
			this.boxButton7.ButtonID = 33;
			this.boxButton7.Def = null;
			this.boxButton7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton7.IsActive = true;
			this.boxButton7.Location = new System.Drawing.Point(528, 0);
			this.boxButton7.Name = "boxButton7";
			this.boxButton7.Size = new System.Drawing.Size(64, 20);
			this.boxButton7.TabIndex = 13;
			this.boxButton7.Text = "boxButton7";
			// 
			// boxButton8
			// 
			this.boxButton8.AllowEdit = true;
			this.boxButton8.ButtonID = 34;
			this.boxButton8.Def = null;
			this.boxButton8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.boxButton8.IsActive = true;
			this.boxButton8.Location = new System.Drawing.Point(592, 0);
			this.boxButton8.Name = "boxButton8";
			this.boxButton8.Size = new System.Drawing.Size(64, 20);
			this.boxButton8.TabIndex = 14;
			this.boxButton8.Text = "boxButton8";
			// 
			// pctCap
			// 
			this.pctCap.Image = ((System.Drawing.Image)(resources.GetObject("pctCap.Image")));
			this.pctCap.Location = new System.Drawing.Point(656, 0);
			this.pctCap.Name = "pctCap";
			this.pctCap.Size = new System.Drawing.Size(32, 20);
			this.pctCap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pctCap.TabIndex = 15;
			this.pctCap.TabStop = false;
			this.pctCap.Paint += new System.Windows.Forms.PaintEventHandler(this.pctCap_Paint);
			this.pctCap.MouseEnter += new System.EventHandler(this.pctCap_MouseEnter);
			this.pctCap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pctCap_MouseUp);
			this.pctCap.MouseLeave += new System.EventHandler(this.pctCap_MouseLeave);
			this.pctCap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pctCap_MouseDown);
			// 
			// bMenu
			// 
			this.bMenu.ImageIndex = 0;
			this.bMenu.ImageList = this.boxImgLst;
			this.bMenu.Location = new System.Drawing.Point(3, 2);
			this.bMenu.Name = "bMenu";
			this.bMenu.Size = new System.Drawing.Size(16, 16);
			this.bMenu.TabIndex = 16;
			this.bMenu.MouseEnter += new System.EventHandler(this.bMenu_MouseEnter);
			this.bMenu.MouseLeave += new System.EventHandler(this.bMenu_MouseLeave);
			this.bMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bMenu_MouseDown_1);
			// 
			// boxImgLst
			// 
			this.boxImgLst.ImageSize = new System.Drawing.Size(16, 16);
			this.boxImgLst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("boxImgLst.ImageStream")));
			this.boxImgLst.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Box
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 185);
			this.Controls.Add(this.bMenu);
			this.Controls.Add(this.pctCap);
			this.Controls.Add(this.boxButton8);
			this.Controls.Add(this.boxButton7);
			this.Controls.Add(this.boxButton6);
			this.Controls.Add(this.boxButton5);
			this.Controls.Add(this.boxButton4);
			this.Controls.Add(this.boxButton3);
			this.Controls.Add(this.boxButton2);
			this.Controls.Add(this.boxButton1);
			this.Controls.Add(this.numHue);
			this.Controls.Add(this.imgHue);
			this.Controls.Add(this.bSetHue);
			this.Controls.Add(this.SmallTab);
			this.Controls.Add(this.BigTab);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Box";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Box";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Box_KeyDown);
			this.Resize += new System.EventHandler(this.Box_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Box_Closing);
			this.Load += new System.EventHandler(this.Box_Load);
			this.LocationChanged += new System.EventHandler(this.Box_LocationChanged);
			this.BigTab.ResumeLayout(false);
			this.TabGeneral.ResumeLayout(false);
			this.TabDeco.ResumeLayout(false);
			this.TabTravel.ResumeLayout(false);
			this.TabProperties.ResumeLayout(false);
			this.TabItems.ResumeLayout(false);
			this.TabNPCs.ResumeLayout(false);
			this.TabAdmin.ResumeLayout(false);
			this.TabTools.ResumeLayout(false);
			this.TabDoors.ResumeLayout(false);
			this.TabLights.ResumeLayout(false);
			this.TabNotes.ResumeLayout(false);
			this.SmallTab.ResumeLayout(false);
			this.tabArt.ResumeLayout(false);
			this.tabMap.ResumeLayout(false);
			this.tabProps.ResumeLayout(false);
			this.tabCustom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numHue)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Variables

		/// <summary>
		/// When using the XMinimize option, this bypasses the closing check
		/// </summary>
		private bool m_Exit = false;

		/// <summary>
		/// The hue picker
		/// </summary>
		private HuePicker m_HuePicker = null;

		/// <summary>
		/// The recent hues menu
		/// </summary>
		private RecentHuesMenu m_HuesMenu = null;

		/// <summary>
		/// The next profile that should be loaded after this form is closed
		/// </summary>
		private string m_NextProfile = null;

		/// <summary>
		/// Gets the name of the profile that should be loaded after this instance is closed
		/// </summary>
		public string NextProfile
		{
			get { return m_NextProfile; }
		}

		#endregion

		/// <summary>
		/// Display tool tips on the map
		/// </summary>
		private void Map_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Right )
			{
				// Do spawn tool tip
				Point loc = Pandora.Map.PointToClient( System.Windows.Forms.Control.MousePosition );
				IMapDrawable obj = Pandora.Map.FindDrawObject( loc, 6 );

				if ( obj != null && obj is SpawnDrawObject )
				{
					SpawnDrawObject spawn = obj as SpawnDrawObject;

					TheBox.Forms.PopUpForm.PopUp( Pandora.BoxForm, "Spawn Details", spawn.Spawn.ToolTipDetailed, true, null );
				}
				else
				{
					PopUpForm.PopUp( Pandora.BoxForm, "No Spawn", "Nothing spawns here", true, null );
				}
			}
		}

		#region Minimizing, closing and tray behaviour

		/// <summary>
		/// OnClosing handler
		/// </summary>
		private void Box_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( Pandora.Profile.General.XMinimize )
			{
				if ( ! m_Exit )
				{
					e.Cancel = true;
					WindowState = FormWindowState.Minimized;
					return;
				}
			}

			// Save position
			if ( WindowState == FormWindowState.Normal )
			{
				Pandora.Profile.General.WindowLocation = this.Location;
			}

			// Save options
			Pandora.Profile.Save();
		}

		/// <summary>
		/// Resizing: minimizing behaviour
		/// </summary>
		private void Box_Resize(object sender, System.EventArgs e)
		{
			if ( WindowState == FormWindowState.Minimized && Pandora.Profile.General.MinimizeToTray )
			{
				ShowInTaskbar = true;
				ShowInTaskbar = false;
			}

			TopMost = Pandora.Profile.General.TopMost;
		}

		/// <summary>
		/// Box site from the tray menu
		/// </summary>
		private void TrayBox_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start( "http://arya.runuo.com/" );
		}

		/// <summary>
		/// Options form from the tray menu
		/// </summary>
		private void TrayOptions_Click(object sender, System.EventArgs e)
		{
			OptionsForm of = new OptionsForm();
			of.ShowDialog( this );
		}

		/// <summary>
		/// Exiting from the tray
		/// </summary>
		private void TrayExit_Click(object sender, System.EventArgs e)
		{
			m_Exit = true;
			Close();
		}

		/// <summary>
		/// Occurs when the user changes the current profile through the menu
		/// </summary>
		private void ChangeProfile(object sender, EventArgs e)
		{
			MenuItem mi = sender as MenuItem;

			if ( mi == null )
				return;

			m_NextProfile = mi.Text;
			m_Exit = true;
			Close();
		}

		/// <summary>
		/// User creates a new profile through the menu
		/// </summary>
		private void miNewProfile_Click(object sender, EventArgs e)
		{
			Pandora.Profile.Save();
			Pandora.CreateNewProfile();
		}

		/// <summary>
		/// Export profile
		/// </summary>
		private void miExportProfile_Click(object sender, EventArgs e)
		{
            ProfileManager.Instance.ExportProfile(Pandora.Profile);
		}

		/// <summary>
		/// Import profile
		/// </summary>
		private void miImportProfile_Click(object sender, EventArgs e)
		{
            TheBox.Options.Profile p = ProfileManager.Instance.ImportProfile();

			if ( p != null )
			{
				if ( MessageBox.Show(
					this,
					Pandora.TextProvider[ "Misc.ProfileImport" ],
					"",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question ) == DialogResult.Yes )
				{
					ChangeProfile( p.Name );
				}
			}
		}

		/// <summary>
		/// Show about form
		/// </summary>
		private void miAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm form = new AboutForm();
			form.ShowDialog();
		}

		#region Box Menu

		private void bMenu_MouseEnter(object sender, System.EventArgs e)
		{
			bMenu.ImageIndex = 1;
		}

		private void bMenu_MouseLeave(object sender, System.EventArgs e)
		{
			bMenu.ImageIndex = 0;
		}

		private void bMenu_MouseDown_1(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			TrayMenu.Show( bMenu, new Point( e.X, e.Y ) );
		}

		#endregion

		#endregion

		#region Hues

		/// <summary>
		/// Sets the correct value for the hue picker
		/// </summary>
		public int SelectedHue
		{
			set
			{
				if ( value >= 0 && value <= 3000 )
				{
					Pandora.Profile.Hues.SelectedIndex = value;
					numHue.Value = value;
				}
			}
		}

		/// <summary>
		/// Hue numeric up and down: value changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numHue_ValueChanged(object sender, System.EventArgs e)
		{
			if ( !Created )
				return;

			if ( numHue.Value != 0 )
			{
				m_HuePicker.SelectedHue = (int) numHue.Value;
				imgHue.Image = Pandora.Hues[ (int) numHue.Value ].GetSpectrum( imgHue.Size );
			}
			else
			{
				imgHue.Image.Dispose();
				imgHue.Image = null;
			}

			Pandora.Profile.Hues.SelectedIndex = (int) numHue.Value;
		}

		/// <summary>
		/// Send Set Hue command
		/// </summary>
		private void bSetHue_Click(object sender, System.EventArgs e)
		{
			SetHue( null );
		}

		private void SetHue( string modifier )
		{
			Pandora.Profile.Hues.SelectedIndex = (int) numHue.Value;

			Pandora.Profile.Commands.DoSet( "Hue", Pandora.Profile.Hues.SelectedIndex.ToString(), modifier );
			Pandora.Prop.SetProperty( "Hue", Pandora.Profile.Hues.SelectedIndex.ToString(), null );
			Pandora.Profile.Hues.RecentHues.Add( Pandora.Profile.Hues.SelectedIndex );
		}

		/// <summary>
		/// Mouse down on the Hue Preview (open picker/menu)
		/// </summary>
		private void imgHue_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( !imgHue.Enabled )
				return;

			if ( e.Button == MouseButtons.Left )
			{
				m_HuePicker.Visible = ! m_HuePicker.Visible;
				// m_HuePicker.ShowDialog();
				// numHue.Value = m_HuePicker.SelectedHue;
			}
			else if ( e.Button == MouseButtons.Right )
			{
				m_HuesMenu.Show( imgHue, new Point( e.X, e.Y ) );
			}
		}

		/// <summary>
		/// A hue has been selected by the recent hues menu
		/// </summary>
		private void m_HuesMenu_HueClicked(object sender, EventArgs e)
		{
			numHue.Value = m_HuesMenu.SelectedHue;
		}

		#endregion

		#region Tabs managment

		/// <summary>
		/// Gets the names of the tabs displayed
		/// </summary>
		/// <returns>An array of strings containing the names of the tabs in the Box form</returns>
		public string[] GetTabNames()
		{
			string[] names = new string[ BigTab.TabCount ];

			for ( int i = 0; i < names.Length; i++ )
			{
				names[ i ] = BigTab.TabPages[ i ].Text;
			}

			return names;
		}

		/// <summary>
		/// Selects the tab displayed by the smaller tab
		/// </summary>
		/// <param name="tab">The SmallTabs value representing the small tab that should be selected</param>
		public void SelectSmallTab( SmallTabs tab )
		{
			TabPage page = null;

			switch ( tab )
			{
				case SmallTabs.Art:
					page = tabArt;
					break;
				case SmallTabs.Map:
					page = tabMap;
					break;
				case SmallTabs.Props:
					page = tabProps;
					break;
			}

			if ( SmallTab.SelectedTab != page )
				SmallTab.SelectedTab = page;
		}

		/// <summary>
		/// When something uses the map, bring it to front
		/// </summary>
		private void MapUsed(object sender, System.EventArgs e)
		{
			SmallTab.SelectedTab = tabMap;
		}

		/// <summary>
		/// Updates the zoom level in the options
		/// </summary>
		private void Map_ZoomLevelChanged(object sender, System.EventArgs e)
		{
			Pandora.Profile.Travel.Zoom = Map.ZoomLevel;
		}

		#endregion

		#region Misc Methods

		/// <summary>
		/// KEYS
		/// </summary>
		private void Box_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( BigTab.SelectedTab == TabTravel )
			{
				m_TravelTab.DoKeys( this, e );
			}
			else if ( BigTab.SelectedTab == TabNPCs )
			{
				m_PageMobiles.DoKeys( this, e );
			}
			else if ( BigTab.SelectedTab == TabDeco )
			{
				m_TabDeco.DoKeys( this, e );
			}
			else if ( BigTab.SelectedTab == TabItems )
			{
				m_ItemsTab.DoKeys( this, e );
			}
		}

		private void Box_Load(object sender, System.EventArgs e)
		{
			Splash.SetStatusText( "Choosing language" );
			Pandora.LocalizeControl( this );
			Pandora.LocalizeMenu( TrayMenu );

			Splash.SetStatusText( "Repositioning" );
			// Set position
			Location = Pandora.Profile.General.WindowLocation;
			VerifyVisibility();
			ShowInTaskbar = Pandora.Profile.General.ShowInTaskBar;

			// Display spawns
			if ( Pandora.Profile.Travel.ShowSpawns && SpawnData.SpawnProvider != null )
			{
				Splash.SetStatusText( "Spawning..." );
				SpawnData.SpawnProvider.RefreshSpawns();
			}

			Splash.SetStatusText( "Artwork setup" );
			// Set options on the art viewer
			Pandora.Art.RoomView = Pandora.Profile.General.RoomView;
			Pandora.Art.ResizeTallItems = Pandora.Profile.General.Scale;
			Pandora.Art.Animate = Pandora.Profile.General.Animate;
			Pandora.Art.BackColor = Pandora.Profile.General.ArtBackground.Color;

			// Go online
			if ( Pandora.Profile.Server.Enabled && Pandora.Profile.Server.ConnectOnStartup )
			{
				Splash.SetStatusText( "Connecting to BoxServer" );
				BoxServerForm form = new BoxServerForm( true );
				form.ShowDialog();
			}

			// Startup programs
			Splash.SetStatusText( "Launching startup programs" );
			Pandora.Profile.Launcher.PerformStartup();

			Text = string.Format( Pandora.TextProvider[ "Misc.BoxTitle" ], Pandora.Profile.Name, Pandora.Connected ? Pandora.TextProvider[ "Misc.Online" ] : Pandora.TextProvider[ "Misc.Offline" ] );

			// Set startup tab
			if ( Pandora.Profile.General.StartupTab != null )
			{
				foreach ( TabPage page in BigTab.TabPages )
				{
					if ( page.Text == Pandora.Profile.General.StartupTab )
					{
						BigTab.SelectedTab = page;
						break;
					}
				}
			}

			Splash.SetStatusText( "Building tips and menus" );

			// Tooltips
			Pandora.ToolTip.SetToolTip( pctCap, Pandora.TextProvider[ "Tips.Screenshot" ] );

			// Build profiles menu items
			MenuItem miNewProfile = new MenuItem( Pandora.TextProvider[ "Common.New" ] );
			miNewProfile.Click += new EventHandler(miNewProfile_Click);
			miProfile.MenuItems.Add( miNewProfile );

			MenuItem miExportProfile = new MenuItem( Pandora.TextProvider[ "Common.Export" ] );
			miExportProfile.Click += new EventHandler(miExportProfile_Click);
			miProfile.MenuItems.Add( miExportProfile );

			MenuItem miImportProfile = new MenuItem( Pandora.TextProvider[ "Common.Import" ] );
			miImportProfile.Click += new EventHandler(miImportProfile_Click);
			miProfile.MenuItems.Add( miImportProfile );

			miProfile.MenuItems.Add( new MenuItem( "-" ) );

			foreach( string s in TheBox.Options.Profile.ExistingProfiles )
			{
				MenuItem mi = new MenuItem( s );
				mi.Checked = s == Pandora.Profile.Name;
				if ( !mi.Checked )
					mi.Click += new EventHandler(ChangeProfile);
				else
					mi.Enabled = false;

				miProfile.MenuItems.Add( mi );
			}

			bSetHue.Tag = new CommandCallback( SetHue );
			bSetHue.ContextMenu = Pandora.cmModifiers;

			Splash.Close();
			Utility.BringWindowToFront( this.Handle );
		}

		/// <summary>
		/// Manage the small tab accordingly
		/// </summary>
		private void BigTab_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TabPage page = BigTab.SelectedTab;

			if ( page == TabTravel )
			{
				SmallTab.SelectedTab = tabMap;
			}
			else if ( page == TabNPCs )
			{
				SmallTab.SelectedTab = tabArt;

				Pandora.Art.Art = ArtViewer.Art.NPCs;
				Pandora.Art.ArtIndex = Pandora.Profile.Mobiles.ArtIndex;
				m_PageMobiles.RefreshArt();
			}
			else if ( page == TabProperties )
			{
				SmallTab.SelectedTab = tabProps;
			}
			else if ( page == TabDeco )
			{
				SmallTab.SelectedTab = tabArt;

				Pandora.Art.Art = ArtViewer.Art.Items;
				Pandora.Art.ArtIndex = Pandora.Profile.Deco.ArtIndex;

				if ( Pandora.Profile.Deco.Hued )
				{
					Pandora.Art.Hue = Pandora.Profile.Hues.SelectedIndex;
				}
				else
				{
					Pandora.Art.Hue = 0;
				}
			}
			else if ( page == TabItems )
			{
				SmallTab.SelectedTab = tabArt;

				Pandora.Art.Art = ArtViewer.Art.Items;

				Pandora.Art.ArtIndex = Pandora.Profile.Items.ArtIndex;
				Pandora.Art.Hue = Pandora.Profile.Items.ArtHue;
			}
		}

		/// <summary>
		/// Moves the window if it doesn't fit correctly
		/// </summary>
		private void VerifyVisibility()
		{
			if ( Left < 0 )
				Left = 0;

			if ( Top < 0 )
				Top = 0;

			double xdelta = Right - SystemInformation.WorkingArea.Width;
			double ydelta = Bottom - SystemInformation.WorkingArea.Height;

			if ( xdelta > 0 )
			{
				double xPercentage = xdelta / Width;

				if ( xPercentage > .30 )
				{
					Left = SystemInformation.WorkingArea.Width - Width;
				}
			}

			if ( ydelta > 0 )
			{
				double yPercentage = ydelta / Height;

				if ( yPercentage > .30 )
				{
					Top = SystemInformation.WorkingArea.Height - Height;
				}
			}
		}

		/// <summary>
		/// Updates all the views using BoxData as data source
		/// </summary>
		public void UpdateBoxData()
		{
			m_PageMobiles.RefreshData();
			m_ItemsTab.UpdateBoxData();
		}

		/// <summary>
		/// Click the tray menu brings up the window
		/// </summary>
		private void Tray_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Left )
			{
				if ( WindowState == FormWindowState.Minimized )
				{
					WindowState = FormWindowState.Normal;
					ShowInTaskbar = Pandora.Profile.General.ShowInTaskBar;
				}

				Activate();
			}
		}

		/// <summary>
		/// Box Form moved: save the location in the options
		/// </summary>
		private void Box_LocationChanged(object sender, System.EventArgs e)
		{
			if ( WindowState == FormWindowState.Normal )
			{
				Pandora.Profile.General.WindowLocation = Location;
			}
		}

		/// <summary>
		/// Closes the current form and switches to a different profile
		/// </summary>
		/// <param name="nextProfile">The name of the next profile</param>
		public void ChangeProfile( string nextProfile )
		{
			m_NextProfile = nextProfile;
			m_Exit = true;
			Close();
		}

		#endregion

		#region Screen Capture

		/// <summary>
		/// Draw border
		/// </summary>
		private void pctCap_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Utility.DrawBorder( pctCap, e.Graphics );
		}

		private void pctCap_MouseEnter(object sender, System.EventArgs e)
		{
			pctCap.BackColor = SystemColors.ControlLightLight;
		}

		private void pctCap_MouseLeave(object sender, System.EventArgs e)
		{
			pctCap.BackColor = SystemColors.Control;
		}

		private void pctCap_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			pctCap.BackColor = SystemColors.Window;

			if ( e.Button == MouseButtons.Left )
			{
				// Take screenie
				Pandora.Profile.Screenshots.Capture();
			}
			else
			{
				// Show screenshot control
				TheBox.Forms.CapForm form = new CapForm();
				form.ShowDialog();
			}
		}

		private void pctCap_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.X >= 0 && e.X < pctCap.Right && e.Y >= 0 && e.Y <= pctCap.Bottom )
			{
				pctCap.BackColor = SystemColors.ControlLightLight;
			}
			else
			{
				pctCap.BackColor = SystemColors.Control;
			}
		}

		#endregion

		/// <summary>
		/// Show the application data folder
		/// </summary>
		private void miViewDataFolder_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start( Pandora.ApplicationDataFolder );
			}
			catch{}
		}

		/// <summary>
		/// Show the log file
		/// </summary>
		private void miViewLog_Click(object sender, System.EventArgs e)
		{
			string log = Path.Combine( Pandora.ApplicationDataFolder, "Log.txt" );

			if ( File.Exists( log ) )
			{
				try
				{
					System.Diagnostics.Process.Start( log );
				}
				catch {}
			}
		}

		/// <summary>
		/// Updates the button style according to the options
		/// </summary>
		public void UpdateButtonStyle()
		{
			FlatStyle flat = FlatStyle.System;

			if ( Pandora.Profile.General.FlatButtons )
			{
				flat = FlatStyle.Flat;
			}

			SetButtonStyle( this, flat );
		}

		/// <summary>
		/// Sets a style on the buttons on a control
		/// </summary>
		/// <param name="c">The control that should have the style set</param>
		/// <param name="style">The style being applied</param>
		private void SetButtonStyle( Control c, FlatStyle style )
		{
			ButtonBase b = c as ButtonBase;

			if ( b != null )
				b.FlatStyle = style;

			foreach( Control child in c.Controls )
				SetButtonStyle( child, style );
		}
	}
}
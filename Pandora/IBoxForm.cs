using System;
using TheBox.Pages;
using TheBox.Forms;
namespace TheBox
{
    public interface IBoxForm : IForm
    {       
        Mobiles Mobiles { get; }
        string NextProfile { get; }
        Props Properties { get; }
        int SelectedHue { set; }
        Travel Travel { get; }

        string[] GetTabNames();
        void ChangeProfile(string nextProfile);
        void SelectSmallTab(SmallTabs tab);
        void UpdateBoxData();
        void UpdateButtonStyle();

        
    }
}

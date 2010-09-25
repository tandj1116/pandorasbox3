using System;
namespace TheBox.Common
{
    public interface ISplash
    {
        void Close();
        void SetStatusText(string text);
        void Show();
    }
}

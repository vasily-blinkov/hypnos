using System.Windows.Forms;

namespace Hypnos.Desktop.Utils
{
    public static class CheckedListBoxUtility
    {
        public static void UncheckAll(this CheckedListBox box)
        {
            for (var index = 0; index < box.Items.Count; index++)
            {
                box.SetItemChecked(index, false);
            }
        }
    }
}

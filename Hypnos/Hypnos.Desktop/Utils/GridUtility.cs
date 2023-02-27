using Hypnos.Desktop.Models.Administration;
using System.Windows.Forms;

namespace Hypnos.Desktop.Utils
{
    public static class GridUtility
    {
        public static void Setup(this DataGridView grid)
        {
            var columns = grid.Columns;
            var id = nameof(Entity.ID);

            if (columns.Contains(id))
            {
                columns[id].Visible = false;
            }

            DataGridViewColumn col;

            for (int index = 0; index < columns.Count; index++)
            {
                col = columns[index];

                if (col.Name == id)
                {
                    continue;
                }

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}

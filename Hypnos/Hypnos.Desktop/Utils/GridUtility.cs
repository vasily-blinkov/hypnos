using System.Collections;
using System.Windows.Forms;
using Hypnos.Desktop.Models.Administration;

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
        
        /// <returns>
        /// Returns row which column called <paramref name="name"/> value is <paramref name="value"/>.
        /// Returns <c>null</c> if the grid hasn't got the column with <paramref name="name"/>
        /// or there is no row with the <paramref name="value"/> in such column.
        /// </returns>
        public static DataGridViewRow Find(this DataGridViewRowCollection rows, string name, object value, IEqualityComparer comparer)
        {
            DataGridViewRow row;

            for (int index = 0; index < rows.Count; index++)
            {
                row = rows[index];
                
                if (comparer.Equals(value, row.Cells[name].Value))
                {
                    return row;
                }
            }

            return null;
        }
    }
}

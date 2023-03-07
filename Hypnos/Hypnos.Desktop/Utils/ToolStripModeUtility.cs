using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Hypnos.Desktop.Utils
{
    public class ToolStripModeUtility<T>
        where T : struct, Enum
    {
        private readonly ToolStrip toolStrip;
        private readonly Dictionary<ToolStripItem, List<T>> itemModeMap = new Dictionary<ToolStripItem, List<T>>();

        private T mode;

        public T Mode
        {
            get => mode;
            set => Switch(mode = value);
        }

        public ToolStripModeUtility(ToolStrip toolStrip)
        {
            this.toolStrip = toolStrip;
        }

        public ToolStripModeUtility<T> Map(T mode, params ToolStripItem[] itemCollection)
        {
            foreach (var item in itemCollection)
            {
                if (!itemModeMap.ContainsKey(item))
                {
                    itemModeMap.Add(item, new List<T>());
                }

                if (!itemModeMap[item].Contains(mode))
                {
                    itemModeMap[item].Add(mode);
                }
            }

            return this;
        }

        public void Switch(T mode)
        {
            foreach (ToolStripItem item in toolStrip.Items)
            {
                if (!itemModeMap.ContainsKey(item))
                {
                    continue;
                }

                item.Visible = itemModeMap[item].Contains(mode);
            }

            this.mode = mode;
        }
    }
}

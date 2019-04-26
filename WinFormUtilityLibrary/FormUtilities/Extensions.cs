using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormUtilityLibrary.FormUtilities
{
    public static class ControlExtensions
    {

        public static int RemoveAllAt(this ListView lvi, int[] ats)
        {
            int count = 0;
            foreach (var i in ats)
            {
                lvi.Items.RemoveAt(i);
                count++;
            }
            return count;
        }

    }
}

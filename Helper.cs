using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SepanHotel
{
    internal class Helper
    {
        public bool StringValidation(List<string> strgs)
        {
            foreach (string str in strgs)
            {
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }
            }
            return true;
        }

        public void OpenUc(UserControl uc, Panel p)
        {
            p.Controls.Add(uc);
            p.Tag = uc;
            uc.BringToFront();
            uc.Show();
        }
    }
}

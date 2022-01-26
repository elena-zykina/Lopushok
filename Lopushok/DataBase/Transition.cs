using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lopushok.DataBase
{
    class Transition
    {
        public static Frame MainFrame { get; set; }

        private static LopushokEntities context;
        public static LopushokEntities Context
        {
            get
            {
                if (context == null)
                    context = new LopushokEntities();

                return context;
            }
        }
    }
}

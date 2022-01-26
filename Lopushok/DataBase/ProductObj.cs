using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lopushok.Pages;

namespace Lopushok.DataBase
{
    class ProductObj
    {
        public static int ID { get; set; }
        public static string Title { get; set; }
        public static string ArticleNumber { get; set; }
        public static string Description { get; set; }
        public static string Image { get; set; }
        public static decimal MinCostForAgent { get; set; }
    }
}

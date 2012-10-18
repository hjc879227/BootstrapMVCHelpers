using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ovixon.WebSite.codes
{
    public class AjaxDataTableGetParams
    {
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public string sSearch { get; set; }
        public bool bEscapeRegex { get; set; }
        public int iColumns { get; set; }
        public int iSortingCols { get; set; }
        public int iSortCol_0 { get; set; }
        public string iSortDir_0 { get; set; }
        public int sEcho { get; set; }
        public bool bSortable_0 { get; set; }
        public bool bSearchable_0 { get; set; }
    }
}
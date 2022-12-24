using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSoftware.Models
{
    public partial class CKEditor
    {
        [AllowHtml]
        public SanPham NoiDung { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkhuKB.Models
{
    public class PageVO
    {
        public string searchText { get; set; }
        public int page { get; set; }
        public int itemPerPage { get; set; }
        public string selecter { get; set; }

        public PageVO(string searchText, string selecter, int page, int itemPerPage)
        {
            this.selecter = selecter;
            this.searchText = searchText;
            this.page = page;
            this.itemPerPage = itemPerPage;
        }
    }
}

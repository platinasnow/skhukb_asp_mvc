using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkhuKB.Models
{
    public class Paging
    {
        public int itemPerPage = 10;
        public int nextPage = 10;
        public int page = 1;
        public int totalItemCount;

        public Paging(int totalItemCount, int itemPerPage, int nextPage)
        {
            this.totalItemCount = totalItemCount;
            this.nextPage = nextPage;
            this.itemPerPage = itemPerPage;
        }

        public int getCurrentPage()
        {
            int page = this.page;
            if (page < 1)
            {
                page = 1;
            }
            int pageCount = getPageCount();
            if (page > pageCount)
            {
                page = pageCount;
            }
            return page;
        }

        public int getPageCount()
        {
            return (totalItemCount - 1) / itemPerPage + 1;
        }

        public int getPageBegin()
        {
            return ((getCurrentPage() - 1) / itemPerPage) * itemPerPage + 1;
        }

        public int getPageEnd()
        {
            int pageCount = getPageCount();
            int num = getPageBegin() + nextPage - 1;
            return Math.Min(pageCount, num);
        }

        public int getItemSeqBegin()
        {
            int page = this.page;
            int itemPerPage = this.itemPerPage;
            return (page - 1) * itemPerPage + 1;
        }

        public int getItemSeqEnd()
        {
            int page = this.page;
            int itemPerPage = this.itemPerPage;
            int totalItemCount = this.totalItemCount;
            if (totalItemCount == 0)
            {
                return page * itemPerPage;
            }
            else
            {
                return Math.Min(totalItemCount, page * itemPerPage);
            }

        }

        public int getTotalItemCount()
        {
            return totalItemCount;
        }

        public void setTotalItemCount(int totalItemCount)
        {
            this.totalItemCount = totalItemCount;
        }

        public int getItemPerPage()
        {
            return itemPerPage;
        }

        public void setItemPerPage(int itemPerPage)
        {
            this.itemPerPage = itemPerPage;
        }

        public int getNextPage()
        {
            return nextPage;
        }

        public void setNextPage(int nextPage)
        {
            this.nextPage = nextPage;
        }

        public int getPage()
        {
            return page;
        }

        public void setPage(int page)
        {
            this.page = page;
        }

        public int getGoNextPage()
        {
            return Math.Min(getPageCount(), getPage() + getNextPage());
        }

        public int getGoPrevPage()
        {
            return Math.Max(1, getPage() - getNextPage());
        }
    }
}

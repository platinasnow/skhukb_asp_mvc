using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkhuKB.Models;
using SkhuKB.DAO;
using System.Collections;

namespace SkhuKB.Controllers
{
    [Authorize]
    public class MobileController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchDialog()
        {
            return View();
        }

        public ActionResult ArticleList(String searchText, String selecter, int page = 1)
        {
            PageVO pageVo = new PageVO(searchText, selecter, page, 10);
            int totalCount = ArticleDAO.getTotalCountArticleList(searchText, selecter);
            Paging paging = new Paging(totalCount, 10, 10);
            paging.setPage(page);
            ViewBag.articleList = ArticleDAO.getArticleList(searchText, selecter, page);
            ViewBag.paging = paging;
            ViewBag.selecter = selecter;
            ViewBag.searchText = searchText;
            return View();
        }

        public ActionResult SubArticleList(string aIdx)
        {
            ArrayList subArticleList = SubArticleDAO.getSubArticleList(aIdx, 'N');
            ViewBag.subArticleList = subArticleList;

            return View();
        }


    }
}

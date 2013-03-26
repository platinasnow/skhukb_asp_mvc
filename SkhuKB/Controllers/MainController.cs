using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkhuKB.Models;
using System.Collections;
using SkhuKB.Util;
using System.IO;
using SkhuKB.DAO;

namespace SkhuKB.Controllers
{
    [Authorize]
    public class MainController : BaseController
    {
        public ActionResult Index()
        {
            return View(new UserVO { Username = this.User.Identity.Name });
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

        public ActionResult DetailView(string aIdx, int saIdx = 0)
        {
            SubArticleVO subVO = new SubArticleVO(saIdx, aIdx);
            ViewBag.aIdx = aIdx;
            ViewBag.articleItem = ArticleDAO.getArticleItem(aIdx);
            ArrayList subArticleList = SubArticleDAO.getSubArticleList(aIdx, 'N');
            ViewBag.subArticleList = subArticleList;
            //항목을 검색하여 들어갔을 때에는 saIdx가 없어서, 항목의 첫번째 값을 표시하게 해줌. 
            if (saIdx == 0)
            {
                if (subArticleList.Count > 0)
                {
                    subVO.saIdx = ((SubArticleVO)subArticleList[0]).saIdx;
                }
            }
            //파일 첨부가 있으면 리스트 출력
            SubArticleVO detailItem = SubArticleDAO.getSubArticleItem(subVO.saIdx, 'N');
            ViewBag.detailItem = detailItem;

            // AIDX, SAIDX 
            ViewBag.aIdx = subVO.aIdx;
            ViewBag.saIdx = subVO.saIdx;


            ViewBag.fileList = FileDAO.getSubArticleFiles(subVO.saIdx);
            return View();
        }

        public ActionResult WriteSubArticleContents(string aIdx, String writeType, int saIdx = 0)
        {
            SubArticleVO subVO = new SubArticleVO(saIdx, aIdx);
            if ("add".Equals(writeType))
            {
                subVO.aIdx = null;
                subVO.saIdx = 0;
            }
            //왼쪽의 리스트를 위한 데이터
            ArrayList subArticleList = SubArticleDAO.getSubArticleList(subVO.aIdx, 'N');
            ViewBag.subArticleList = subArticleList;

            SubArticleVO detailItem = SubArticleDAO.getSubArticleItem(subVO.saIdx, 'N');
            ViewBag.detailItem = detailItem;
            if (detailItem != null)
            {
                ViewBag.fileList = FileDAO.getSubArticleFiles(subVO.saIdx);
            }

            ViewBag.writeType = writeType;
            ViewBag.aIdx = aIdx;
            ViewBag.saIdx = saIdx;

            return View();
        }

        [ValidateInput(false)]
        public RedirectToRouteResult WriteSubArticleSubmit(string aIdx, int saIdx, String title,
             String txtContent, String writeType, IEnumerable<HttpPostedFileBase> files)
        {
            //항목이 없을 경우 항목을 추가함
            ArticleVO articleVO = ArticleDAO.getArticleItem(aIdx);
            if (articleVO == null)
            {
                ArticleDAO.insertArticleList(aIdx);
            }

            if (!(title.Length > 50))
            {
                String writer = HttpContext.User.Identity.Name;
                //subArticle을 삽입, 수정시에는 삽입과 동시에 이전글을 삭제
                int fileSaIdx = SubArticleDAO.InsertSubArticleList(aIdx, title, txtContent, writer);
                if ("modify".Equals(writeType))
                {
                    SubArticleDAO.deleteSubArticleList(saIdx);
                }

                //파일 업로드
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = StringUtil.setFileNameByDateTime(Path.GetFileName(file.FileName));
                            var path = Path.Combine(Server.MapPath("~/UploadFiles"), fileName);
                            file.SaveAs(path);
                            FileDAO.insertSubArticleFile(fileSaIdx, fileName);
                        }
                    }
                }

            }
            return RedirectToAction("DetailView", new { aIdx = aIdx });
        }



        public ActionResult DeleteSubArticle(string aIdx, int saIdx)
        {
            /*
             * 파일을 실제로 삭제한다.
            string directroy = Server.MapPath("~/UploadFiles");
            try
            {
                int fileSeq = ((SubArticleVO)Mapper.Instance().QueryForObject("getSubArticleListByAidxAndSaIdx", subVO)).FileSeq;
                ArrayList deleteFileName = (ArrayList)Mapper.Instance().QueryForList("fileListByFileSeq", fileSeq);

                ArrayList files = new ArrayList();
                foreach (FilesVO file in deleteFileName)
                {
                    if (files != null)
                    {
                        System.IO.File.Delete(directroy + "\\" + file.fileName);
                    }
                }
            }
            catch (Exception e) { }
             */
            SubArticleDAO.deleteSubArticleList(saIdx);
            return RedirectToAction("DetailView", new { aIdx = aIdx });
        }

        public ActionResult FileDownload(string fileName)
        {
            return new DownloadResult { VirtualPath = "~/UploadFiles/" + fileName, FileDownloadName = fileName };
        }

        public ActionResult HistoryListView(string aIdx)
        {
            //왼쪽의 리스트를 위한 데이터
            ArrayList subArticleList = SubArticleDAO.getSubArticleList(aIdx, 'N');
            ViewBag.subArticleList = subArticleList;

            //과거이력 리스트
            ViewBag.historyList = SubArticleDAO.getSubArticleList(aIdx, 'Y');

            ViewBag.aIdx = aIdx;
            return View();
        }

        public ActionResult HistoryDetailView(string aIdx, int saIdx)
        {
            //왼쪽의 리스트를 위한 데이터
            ArrayList subArticleList = SubArticleDAO.getSubArticleList(aIdx, 'N');
            ViewBag.subArticleList = subArticleList;

            ViewBag.detailItem = SubArticleDAO.getSubArticleItem(saIdx, 'Y');
            //파일 첨부가 있으면 리스트 출력
            ViewBag.fileList = FileDAO.getSubArticleFiles(saIdx);
            // AIDX, SAIDX 
            ViewBag.aIdx = aIdx;
            ViewBag.saIdx = saIdx;

            return View();
        }
    }
}

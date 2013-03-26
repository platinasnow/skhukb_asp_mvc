using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SkhuKB.Util;
using System.Web.Mvc;

namespace naver01.se.popup.quick_photo
{
	public partial class FileUploader : System.Web.UI.Page
	{
		private string UploadDir = "/UploadFiles/"; 
		// 이미지 파일을 서버에 저장하고 저장한 정보를 클라이언트에 보내줌.
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				HttpFileCollection uploadedFiles = Request.Files;

				string callback_func = Request.Form["callback_func"];

				// 다수의 파일을 다운로드 하여 파일을 저장함
				for (int j = 0; j < uploadedFiles.Count; j++)
				{
					HttpPostedFile userPostedFile = uploadedFiles[j];

					// 파일 내용이 있을경우
					if (userPostedFile.ContentLength > 0)
					{
                        string fileName = Path.GetFileName(userPostedFile.FileName);
                        string fileUrl = UploadDir + fileName ;	// 업로드 디렉토리 + 파일명.
                        fileUrl = HttpUtility.UrlDecode(fileUrl, System.Text.Encoding.GetEncoding(51949));

                        fileUrl = StringUtil.setFileNameByDateTime(fileUrl); //파일 이름의 중복을 막기 위해 파일 뒤에 시간을 붙임
						// 파일 저장  (같은 파일명일경우 에러 처리 필요)
						userPostedFile.SaveAs(Server.MapPath(fileUrl));

						// 클라이언트에 저장한 파일 정보를 보내 줌
						string returnUrl = string.Format("callback.html?callback_func={0}&bNewLine=true&sFileName={1}&sFileURL={2}",
							callback_func, fileName, fileUrl);

						Response.Redirect(returnUrl);
					}
					else
					{
						ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('첨부파일 등록중 에러발생。');", true);

					}
				}
			}
		}
	}
}
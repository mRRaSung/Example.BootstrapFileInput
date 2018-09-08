using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BootsFileUpload.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Delete(string key)
        {
            //Delete

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] files, string pid)
        {
            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null)
                    {
                        var filePath = Path.Combine(Server.MapPath("~/Files/product/1b3a"), file.FileName);
                        file.SaveAs(filePath);
                    }
                }
            }

            FileItem response = new FileItem();
            response.initialPreview = new string[]
            {
                "<img class='kv-preview-data file-preview-image' src='/Content/2.png'  style='width: auto; height: auto; max-width: 100%; max-height: 100%;'>"
            };
            response.initialPreviewConfig = new PreviewConfig[]
            {
                new PreviewConfig
                {
                    caption = "aaa.jpg",
                    size = 1000,
                    url = "/Home/Delete",
                    key = pid,
                    width = "60px"
                }
            };
            //response.error = "Something wrong :(";

            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(response));
        }


        public class FileItem
        {
            public string error { get; set; }
            //public string[] errorkeys { get; set; } = new string[] { };
            public string[] initialPreview { get; set; }
            public PreviewConfig[] initialPreviewConfig { get; set; }
            //public string[] initialPreviewThumbTags { get; set; }
            //public bool append { get; set; }
        }

        //caption: "3.jpg", size: 567728, width: "120px", url: "/site/file-delete", key: 3
        public class PreviewConfig
        {
            public string caption { get; set; }
            public int size { get; set; }
            public string url { get; set; }
            public string key { get; set; }
            public string width { get; set; }
        }

    }
}
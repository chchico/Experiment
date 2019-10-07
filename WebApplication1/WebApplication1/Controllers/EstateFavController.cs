using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class EstateFavController : Controller
    {
        /// <summary>
        /// お気に入り物件配列
        /// </summary>
        private string[] FavoriteIDs
        {
            get
            {
                return this.Session["FavoriteIDs"] as string[] ?? new string[] { };
            }
        }


        // GET: ReactTest01
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReactTest01
        public ActionResult GetEstateJson()
        {
            // お気に入りJsonを読み込み
            var estateData = new ViewModels.EstateData();

            //response
            object obj = new { status = "OK", data = estateData.Estates };

            //return
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: ReactTest01
        public ActionResult GetFavJson()
        {
            // お気に入りJsonを読み込み
            var estateData = new ViewModels.EstateData();

            var model = new List<ViewModels.EstateData.Estate>();
            foreach (var favoriteID in this.FavoriteIDs)
            {
                model.Add(estateData.Estates.SingleOrDefault(m => m.EstateID == favoriteID));
            }

            //response
            object obj = new { status = "OK", data = model };

            //return
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: ReactTest01
        public ActionResult ToggleFavJson(string id)
        {
            // お気に入りJsonに追加
            var favoriteIDs = this.FavoriteIDs.ToList();

            if (favoriteIDs.Contains(id))
            {
                favoriteIDs.Remove(id);
            }
            else
            {
                favoriteIDs.Add(id);
            }


            this.Session["FavoriteIDs"] = favoriteIDs.ToArray();

            //return
            return RedirectToAction("GetFavJson");
        }
    }
}
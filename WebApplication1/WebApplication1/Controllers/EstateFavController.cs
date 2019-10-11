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
                return this.Session[this.FavoriteTitle] as string[] ?? new string[] { };
            }
        }

        /// <summary>
        /// お気に入り物件配列
        /// </summary>
        private string FavoriteTitle
        {
            get
            {
                return this.Session["FavoriteTitle"] as string ?? "Default";
            }
        }

        /// <summary>
        /// お気に入り物件配列
        /// </summary>
        private string[] FavoriteTitles
        {
            get
            {
                return this.Session["FavoriteTitles"] as string[] ?? new string[] { "Default" };
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
        public ActionResult GetFavJson(string favoriteTitle)
        {
            if (!string.IsNullOrEmpty(favoriteTitle))
            {
                this.Session["FavoriteTitle"] = favoriteTitle;

                if (!this.FavoriteTitles.Contains(favoriteTitle))
                {
                    var favoriteTitles = this.FavoriteTitles.ToList();
                    favoriteTitles.Add((string)favoriteTitle);
                    this.Session["FavoriteTitles"] = favoriteTitles.ToArray();
                }
            }

            // お気に入りJsonを読み込み
            var estateData = new ViewModels.EstateData();

            var estates = new List<ViewModels.EstateData.Estate>();
            foreach (var favoriteID in this.FavoriteIDs)
            {
                estates.Add(estateData.Estates.SingleOrDefault(m => m.EstateID == favoriteID));
            }

            var model = new ViewModels.FavoriteList
            {
                Estates = estates,
                Title = this.FavoriteTitle,
                Titles = this.FavoriteTitles.ToList(),
            };

            //response
            object obj = new { status = "OK", data = model };

            //return
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // GET: ReactTest01
        public ActionResult ToggleFavoriteEstate(string id)
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

            this.Session[this.FavoriteTitle] = favoriteIDs.ToArray();

            //return
            return RedirectToAction("GetFavJson");
        }

        // GET: ReactTest01
        public ActionResult DeleteFavoriteTitle(string favoriteTitle)
        {
            // お気に入りJsonに追加
            if (this.FavoriteTitles.Contains(favoriteTitle))
            {
                var favoriteTitles = this.FavoriteTitles.ToList();
                favoriteTitles.Remove((string)favoriteTitle);
                this.Session["FavoriteTitles"] = favoriteTitles.ToArray();

                this.Session["FavoriteTitle"] = null;
                this.Session[favoriteTitle] = null;
            }

            // Defaultをセット
            if (!this.FavoriteTitles.Contains("Default"))
            {
                var favoriteTitles = this.FavoriteTitles.ToList();
                favoriteTitles.Add("Default");
                this.Session["FavoriteTitles"] = favoriteTitles.ToArray();
            }

            //return
            return RedirectToAction("GetFavJson");
        }
    }
}
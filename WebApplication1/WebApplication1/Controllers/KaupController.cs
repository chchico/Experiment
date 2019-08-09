using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class KaupController : Controller
    {
        public ActionResult Index()
        {
            var form = (KaupCalculation.CalculationForm)this.TempData["KaupCalculation.Form"] ?? new KaupCalculation.CalculationForm();

                var model = new KaupCalculation(form);
            return View(model);
        }

        public ActionResult Calculation(KaupCalculation.CalculationForm form)
        {
            this.TempData["KaupCalculation.Form"] = form;

            return this.Redirect("Index");
        }
    }
}
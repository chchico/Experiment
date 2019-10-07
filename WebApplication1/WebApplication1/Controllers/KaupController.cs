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
        [HttpGet]
        public ActionResult Index()
        {
            var form = (KaupCalculation.CalculationForm)this.TempData["KaupCalculation.Form"] ?? new KaupCalculation.CalculationForm();

            var model = new KaupCalculation(form);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(KaupCalculation.CalculationForm form)
        {
            this.TempData["KaupCalculation.Form"] = form;

            return this.RedirectToAction("Index", "Kaup");
        }

        [HttpGet]
        public ActionResult Graph()
        {
            var form = (KaupGraph)this.TempData["KaupGraph"] ?? new KaupGraph();

            var model = new KaupGraph();
            return View(model);
        }

        [HttpPost]
        public ActionResult Graph(KaupGraph form)
        {
            //var form = (KaupGraph)this.TempData["KaupGraph"] ?? new KaupGraph();

            //var model = new KaupCalculation(form);
            return this.Redirect("Graph");
        }
    }
}
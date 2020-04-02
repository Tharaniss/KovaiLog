using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using KovaiLog.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KovaiLog.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new KovaiLogDBContext()); 
        //repository = new GenericRepository<Content>(new KovaiLogDBContext());
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var contentData = this._unitOfWork.ContentRepository.GetContentWithType().OrderByDescending(x => x.UpdatedOn).ToList();
            foreach(var content in contentData)
            {
                content.CreatedDate = TimeAgo(content.UpdatedOn ?? DateTime.Now);
            }
            ViewData["Content"] = contentData;
            colorSetting();
            return View();
        }

        private void colorSetting()
        {
            ViewBag.PanelColor = new Dictionary<string, string>();
            ViewBag.PanelColor.Add("Blue", "panel-primary");
            ViewBag.PanelColor.Add("Green", "panel-success");
            ViewBag.PanelColor.Add("Red", "panel-danger");
            ViewBag.LabelColor = new Dictionary<string, string>();
            ViewBag.LabelColor.Add("Blue", "label-primary");
            ViewBag.LabelColor.Add("Green", "label-success");
            ViewBag.LabelColor.Add("Red", "label-danger");
        }

        public static string TimeAgo(DateTime date)
        {
            TimeSpan timeSince = DateTime.Now.Subtract(date);
            if (timeSince.TotalMilliseconds < 1) return "not yet";
            if (timeSince.TotalMinutes < 1) return "just now";
            if (timeSince.TotalMinutes < 2) return "1 minute ago";
            if (timeSince.TotalMinutes < 60) return string.Format("{0} minutes ago", timeSince.Minutes);
            if (timeSince.TotalMinutes < 120) return "1 hour ago";
            if (timeSince.TotalHours < 24) return string.Format("{0} hours ago", timeSince.Hours);
            if (timeSince.TotalDays < 2) return "yesterday";
            if (timeSince.TotalDays < 7) return string.Format("{0} days ago", timeSince.Days);
            if (timeSince.TotalDays < 14) return "last week";
            if (timeSince.TotalDays < 21) return "2 weeks ago";
            if (timeSince.TotalDays < 28) return "3 weeks ago";
            if (timeSince.TotalDays < 60) return "last month";
            if (timeSince.TotalDays < 365) return string.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30));
            if (timeSince.TotalDays < 730) return "last year"; //last but not least...
            return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));
        }
    }
}

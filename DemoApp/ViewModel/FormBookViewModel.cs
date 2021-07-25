using DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.ViewModel
{
    public class FormBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SelectListItem> Authors { get; set; }
    }
}
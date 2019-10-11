using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class FavoriteList
    {
        public FavoriteList()
        {
            this.Titles = new List<string>();
            this.Estates = new List<EstateData.Estate>();
        }

        public string Title { get; set; }

        public List<string> Titles { get; set; }

        public List<EstateData.Estate> Estates { get; set; }
    }
}
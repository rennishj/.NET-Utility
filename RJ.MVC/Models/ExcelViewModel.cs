using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RJ.MVC.Models
{
    public class ExcelViewModel
    {
        /// <summary>
        /// The way to use this key is to add the spreadsheets to the cache
        /// with NewGuid and return  that to the UI for the user to later download using this key
        /// </summary>
        public Guid FileId { get; set; }
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public ExcelViewModel()
        {
            this.FileId = Guid.NewGuid();
        }
    }
}
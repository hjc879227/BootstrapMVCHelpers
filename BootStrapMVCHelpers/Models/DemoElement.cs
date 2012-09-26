using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using Ovixon.Bootstrap;

namespace Ovixon.WebSite.Models
{
    public class DemoElement
    {
        [DisplayName("Always correct property:")]
        public string Prop1 { get; set; }

        [DisplayName("Always incorrect property:")]
        [Required(ErrorMessage = "Prop2 is not valid")]
        [StringLength(9999, MinimumLength = 9999, ErrorMessage = "Prop2 is not valid")]
        public string Prop2 { get; set; }

        [DisplayName("Required property:")]
        [Required(ErrorMessage = "Prop3 is required")]
        public string Prop3 { get; set; }

        [DisplayName("Simple date:")]
        [DataType(DataType.Date, ErrorMessage = "Prop4 is not valid date")]
        public string Prop4 { get; set; }

        [DisplayName("Simple other date:")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Prop5 is not valid date")]
        public string Prop5 { get; set; }

        [DisplayName("Value for select:")]
        [Required]
        public string Prop6 { get; set; }

        public List<SelectListItem> ListOfData
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem() { Text = "elem 1", Value = "1" });
                list.Add(new SelectListItem() { Text = "elem 2", Value = "2" });
                list.Add(new SelectListItem() { Text = "elem 3", Value = "3" });
                return list;
            }
        }

        public List<string> ListOfStrings
        {
            get
            {
                return this.ListOfData.Select(s => s.Text).ToList();
            }
        }

        public Dictionary<string, string> ListOfDescriptions
        {
            get
            {
                Dictionary<string, string> elems = new Dictionary<string, string>();
                foreach (SelectListItem item in ListOfData)
                    elems.Add("ID-" + item.Value, item.Text);
                return elems;
            }
        }

        public List<string> ListOfHeadersForTable
        {
            get
            {
                List<string> headers = new List<string>();
                headers.Add("#");
                headers.Add("Date");
                headers.Add("Message");
                return headers;
            }
        }

        public List<List<string>> ListOfElementsForTable
        {
            get
            {
                List<List<string>> elements = new List<List<string>>();
                elements.Add((new[] { "1", "01.01.2011", "New business day" }).ToList());
                elements.Add((new[] { "2", "01.01.2011", "Error on the Demo page" }).ToList());
                elements.Add((new[] { "3", "02.01.2011", "Access denied for Demo user to Demo page" }).ToList());
                elements.Add((new[] { "4", "03.01.2011", "Demo user logon" }).ToList());
                elements.Add((new[] { "5", "04.01.2011", "New Demo2 user!" }).ToList());
                elements.Add((new[] { "6", "05.01.2011", "New business day 2" }).ToList());
                return elements;
            }
        }

        public List<BootstrapRowStyleType> ListOfStylesForRows
        {
            get
            {
                List<BootstrapRowStyleType> styles = new List<BootstrapRowStyleType>();
                styles.Add(BootstrapRowStyleType.none);
                styles.Add(BootstrapRowStyleType.error);
                styles.Add(BootstrapRowStyleType.warning);
                styles.Add(BootstrapRowStyleType.info);
                styles.Add(BootstrapRowStyleType.success);
                styles.Add(BootstrapRowStyleType.none);
                return styles;
            }
        }
    }
}
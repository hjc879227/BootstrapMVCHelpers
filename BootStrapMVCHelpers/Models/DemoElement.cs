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

        [DisplayName("Property with min length = 6:")]
        [StringLength(9999, MinimumLength = 6, ErrorMessage = "Minimum length is 6 chars")]
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

        [DisplayName("Bool property:")]
        public bool Prop7 { get; set; }

        public bool Prop8 { get; set; }

        private string[] _Prop9 = null;
        [DisplayName("CheckBoxList:")]
        public string[] Prop9
        {
            get
            {
                if (_Prop9 == null)
                    return new[] {"2", "4"};

                return _Prop9;
            }
            set
            {
                _Prop9 = value;
            }
        }

        public List<SelectListItem> ListOfData
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem() { Text = "elem 1", Value = "1" });
                list.Add(new SelectListItem() { Text = "elem 2", Value = "2" });
                list.Add(new SelectListItem() { Text = "elem 3", Value = "3" });
                list.Add(new SelectListItem() { Text = "elem 4", Value = "4" });
                list.Add(new SelectListItem() { Text = "elem 5", Value = "5" });
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


        public MultiSelectList ListForCheckBoxList
        {
            get
            {
                return new MultiSelectList(this.ListOfData, "Value", "Text");
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

        public Tree<string> DemoTree
        {
            get
            {
                Tree<string> tree = new Tree<string>();

                Tree<string> tree_cld = new Tree<string>();
                tree_cld.Value = "elem 1";
                tree.Add(tree_cld);

                Tree<string> tree_cld_cld = new Tree<string>();
                tree_cld_cld.Value = "<span class='muted'>elem 1.1</span>";
                tree_cld.Add(tree_cld_cld);

                tree_cld_cld = new Tree<string>();
                tree_cld_cld.Value = "<span class='text-warning'>elem 1.2</span>";
                tree_cld.Add(tree_cld_cld);

                Tree<string> tree_cld_cld_cld = new Tree<string>();
                tree_cld_cld_cld.Value = "elem 1.2.1";
                tree_cld_cld.Add(tree_cld_cld_cld);

                tree_cld_cld_cld = new Tree<string>();
                tree_cld_cld_cld.Value = "elem 1.2.2";
                tree_cld_cld.Add(tree_cld_cld_cld);

                tree_cld_cld = new Tree<string>();
                tree_cld_cld.Value = "<span class='text-error'>elem 1.3</span>";
                tree_cld.Add(tree_cld_cld);

                tree_cld_cld = new Tree<string>();
                tree_cld_cld.Value = "<span class='text-info'>elem 1.4</span>";
                tree_cld.Add(tree_cld_cld);

                tree_cld_cld = new Tree<string>();
                tree_cld_cld.Value = "<span class='text-success'>elem 1.5</span>";
                tree_cld.Add(tree_cld_cld);

                tree_cld = new Tree<string>();
                tree_cld.Value = "elem 2";
                tree.Add(tree_cld);

                tree_cld = new Tree<string>();
                tree_cld.Value = "elem 3";
                tree.Add(tree_cld);

                tree_cld = new Tree<string>();
                tree_cld.Value = "elem 4";
                tree.Add(tree_cld);

                return tree;
            }
        }
    }
}
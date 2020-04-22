using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationCS.Models {
    public class Car {
        [Key]
        public int ID { set; get; }
        public string TradeMark { set; get; }
        public string Model { set; get; }
        public string RtfContent { set; get; }
    }
}
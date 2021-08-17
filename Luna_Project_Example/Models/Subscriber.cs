using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Luna_Project_Example.Models
{
    public class Subscriber
    {
        [Key]
        public int subID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int? meter_meterID { get; set; }

        public Subscriber()
        {
        }
    }
    
}
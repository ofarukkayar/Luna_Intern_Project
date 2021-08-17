using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Luna_Project_Example.Models
{
    public class Meter
    {
        [Key]
        public int meterID { get; set; }
        public int valveWidth { get; set; }
        public int numberOfSubs { get; set; }
        public int maxNumberOfSubs { get; set; }
        public DateTime productionDate { get; set; }
        public int batteryState { get; set; }
        public Meter()
        {
            valveWidth = 0;
            productionDate = new DateTime(2000, 01, 01);
            batteryState = 10;

        }
    }


}
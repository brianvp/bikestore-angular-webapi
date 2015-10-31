﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeStore.Models
{
    public class ModelDTO
    {
        public int ModelId { get; set; }

        public string ManufacturerName { get; set; }

        public string ManufacturerId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryId { get; set; }

        public string ManufacturerCode { get; set; }

        public string ModelName { get; set; }

        public string StatusName { get; set; }

        public string StatusId { get; set; }

        public decimal ListPrice { get; set; }

        public string Description { get; set; }
    }
}
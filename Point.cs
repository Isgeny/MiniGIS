﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGIS
{
    public class Point
    {
        public GEOPoint Position { get; set; }

        public Point()
        {
            Position = new GEOPoint();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApplication1.ViewModels
{
    public class KaupGraph
    {
        public KaupGraph()
        {

            this.Forms = new KaupCalculation.CalculationForm[] { new KaupCalculation.CalculationForm() };
        }

        public KaupCalculation.CalculationForm[] Forms { get; set; }
    }
}
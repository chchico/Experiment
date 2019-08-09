using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class KaupCalculation
    {
        public KaupCalculation()
        {
            this.Result = null;
        }

        public KaupCalculation(CalculationForm form)
        {
            this.Form = form;
            this.Result = new CalculationResult(form);
        }

        public CalculationForm Form { get; set; }
        public CalculationResult Result { get; set; }

        public class CalculationForm
        {

            public int Age { get; set; }

            public float Weight { get; set; }

            public float Height { get; set; }


        }

        public class CalculationResult
        {
            public CalculationResult(CalculationForm form)
            {
                if (form.Weight * form.Height != 0)
                {
                    this.IndicationList = new List<Indication>(){
                        new Indication( 0, 13, "やせ", form.Age, form.Height, form.Weight ),
                        new Indication( 13, 15, "やせ気味", form.Age, form.Height, form.Weight ),
                        new Indication( 15, 18, "標準" , form.Age, form.Height, form.Weight ),
                        new Indication( 18, 20, "肥満気味", form.Age, form.Height, form.Weight ),
                        new Indication( 20, 0, "肥満", form.Age, form.Height, form.Weight ),
                    };

                    double kaup = 0;
                    switch (form.Age)
                    {
                        case int n when n < 6:
                            kaup = Math.Round(form.Weight / Math.Pow(form.Height, 2.0) * Math.Pow(10, 4.0), 2);
                            break;
                        case int n when 6 <= n && n < 16:
                            kaup = Math.Round(form.Weight / Math.Pow(form.Height, 3.0) * Math.Pow(10, 7.0), 2);
                            break;
                        case int n:
                        default:
                            break;
                    }
                    this.Kaup = kaup;

                    this.Subject = this.IndicationList
                        .Where(m => m.LowerIndex <= kaup)
                        .Where(m => m.UpperIndex == 0 || m.UpperIndex > kaup)
                        .Single().Subject;
                }
            }

            public List<Indication> IndicationList { get; private set; }

            public double Kaup { get; private set; }

            public string Subject { get; private set; }

        }

        public class Indication
        {
            public Indication(int lower, int upper, string subject, int age, float height, float weight)
            {
                this.LowerIndex = lower;
                this.UpperIndex = upper;
                this.Subject = subject;

                double lowerWeight = 0;
                double upperWeight = 0;
                switch (age)
                {
                    case int n when n < 6:
                        lowerWeight = Math.Round(lower * Math.Pow(height, 2.0) / Math.Pow(10, 4.0), 2);
                        upperWeight = Math.Round(upper * Math.Pow(height, 2.0) / Math.Pow(10, 4.0), 2);
                        break;
                    case int n when 6 <= n && n < 16:
                        lowerWeight = Math.Round(lower * Math.Pow(height, 3.0) / Math.Pow(10, 7.0), 2);
                        upperWeight = Math.Round(upper * Math.Pow(height, 3.0) / Math.Pow(10, 7.0), 2);
                        break;
                    case int n:
                    default:
                        break;
                }
                this.LowerWeight = lowerWeight;
                this.UpperWeight = upperWeight;
            }

            public string Subject { get; set; }

            public int LowerIndex { get; set; }


            public int UpperIndex { get; set; }


            public double LowerWeight { get; private set; }

            public double UpperWeight { get; private set; }
        }
    }
}
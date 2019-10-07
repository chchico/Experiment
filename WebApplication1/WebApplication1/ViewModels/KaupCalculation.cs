using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            [Display(Name = "年齢")]
            [Range(0, 15, ErrorMessage = "16歳以上は対象外です")]
            [Required(ErrorMessage = "年齢を入力してください")]
            public int Age { get; set; }

            [Display(Name = "体重")]
            [Required(ErrorMessage = "体重を入力してください")]
            public float Weight { get; set; }

            [Display(Name = "身長")]
            [Required(ErrorMessage = "身長を入力してください")]
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
                            kaup = form.Weight / Math.Pow(form.Height, 2.0) * Math.Pow(10, 4.0);
                            break;
                        case int n when 6 <= n && n < 16:
                            kaup = form.Weight / Math.Pow(form.Height, 3.0) * Math.Pow(10, 7.0);
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

            [DisplayFormat(DataFormatString = "{0:f}", ApplyFormatInEditMode = true)]
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
                        lowerWeight = lower * Math.Pow(height, 2.0) / Math.Pow(10, 4.0);
                        upperWeight = upper * Math.Pow(height, 2.0) / Math.Pow(10, 4.0);
                        break;
                    case int n when 6 <= n && n < 16:
                        lowerWeight = lower * Math.Pow(height, 3.0) / Math.Pow(10, 7.0);
                        upperWeight = upper * Math.Pow(height, 3.0) / Math.Pow(10, 7.0);
                        break;
                    case int n:
                    default:
                        break;
                }
                this.LowerWeight = lowerWeight > 0 ? lowerWeight : (double?)null;

                this.UpperWeight = upperWeight > 0 ? upperWeight : (double?)null;
            }

            public string Subject { get; set; }

            public int LowerIndex { get; set; }


            public int UpperIndex { get; set; }


            [DisplayFormat(DataFormatString = "{0:f}kg 以上", ApplyFormatInEditMode = true)]
            public double? LowerWeight { get; private set; }

            [DisplayFormat(DataFormatString = "{0:f}kg 未満", ApplyFormatInEditMode = true)]
            public double? UpperWeight { get; private set; }
        }
    }
}
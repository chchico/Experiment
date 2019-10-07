using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class EstateData
    {
        public EstateData()
        {
            var model = new List<Estate>();

            model.Add(new Estate("0000011", 2, 2480, "東京都江東区木場1-11-18", 100.15, 152.30));
            model.Add(new Estate("0000012", 2, 2480, "江東区木場1-12", 100.15, 152.30));
            model.Add(new Estate("0000013", 2, 2480, "江東区木場1-13", 100.15, 152.30));
            model.Add(new Estate("0000014", 2, 2480, "江東区木場1-14", 100.15, 152.30));
            model.Add(new Estate("0000015", 2, 2480, "江東区木場1-15", 100.15, 152.30));
            model.Add(new Estate("0000016", 2, 2480, "江東区木場1-16", 100.15, 152.30));
            model.Add(new Estate("0000017", 2, 2480, "江東区木場1-17", 100.15, 152.30));
            model.Add(new Estate("0000018", 2, 2480, "江東区木場1-18", 100.15, 152.30));
            model.Add(new Estate("0000019", 2, 2480, "江東区木場1-19", 100.15, 152.30));
            model.Add(new Estate("0000020", 2, 2480, "江東区木場1-20", 100.15, 152.30));

            this.Estates = model;
        }

        public List<Estate> Estates { get; set; }

        public class Estate {

            public Estate(string estateID, int typeCd, decimal price, string address, double floor, double land)
            {
                this.EstateID = estateID;
                this.TypeCd = typeCd;
                this.Price = price;
                this.Address = address;
                this.Floor = floor;
                this.Land = land;

            }

            public string EstateID { get; set; }

            public int TypeCd { get; set; }

            public string Type
            {
                get
                {
                    switch (this.TypeCd)
                    {
                        case 1:
                            return "土地";
                        case 2:
                            return "戸建";
                        default:
                            return "";
                    }


                }
            }

            [DisplayFormat(DataFormatString = "{0:#0.####}万円", NullDisplayText = "-")]
            public decimal? Price { get; set; }

            public string Address { get; set; }


            /// <summary>
            /// 土地面積
            /// </summary>
            [DisplayFormat(DataFormatString = "{0}㎡", NullDisplayText = "-")]
            public double? Land { get; set; }

            /// <summary>
            /// 建物面積
            /// </summary>
            [DisplayFormat(DataFormatString = "{0}㎡", NullDisplayText = "-")]
            public double? Floor { get; set; }
        }
    }
}
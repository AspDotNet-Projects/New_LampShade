using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _01_LampSahdeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public List<ProductQueryModel> Product;

        public IndexModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public List<Chart> DataSet { get; set; }

        public void OnGet()
        {

            Product = _productQuery.Productcount();
            List<string> labals = Product.Select(x => x.Name).ToList();
            var Dataa = Product.Select(x => x.Count).ToList();
            var productcount = Product.Count;
          

        DataSet = new List<Chart>
            {
                new Chart
                {
                    
                    Label =labals,
                    Data = Dataa,
                    BackgroundColor = new List<string>
                    {
                        "rgba(255, 99, 132, 0.2)", 
                        "rgba(54, 162, 235, 0.2)", 
                        "rgba(255, 206, 86, 0.2)",
                        "rgba(75, 192, 192, 0.2)",
                        "rgba(153, 102, 255, 0.2)",
                        "rgba(255, 159, 64, 0.2)"
                    },
                    BorderColor = new List<string>
                    {
                        "rgba(255,99,132,1)",
                        "rgba(54, 162, 235, 1)",
                        "rgba(255, 206, 86, 1)", 
                        "rgba(75, 192, 192, 1)",
                        "rgba(153, 102, 255, 1)", 
                        "rgba(255, 159, 64, 1)"
                    },
                    BorderWidth = 1,


                }
            };
        }

        
        public class Chart
        {
            [JsonProperty(propertyName: "label")]
            public List<string> Label { get; set; }
            [JsonProperty(propertyName: "data")]
            public List<int> Data { get; set; }
            [JsonProperty(propertyName: "backgroundColor")]
            public List<string> BackgroundColor { get; set; }
            [JsonProperty(propertyName: "borderColor")]
            public List<string> BorderColor { get; set; }

            [JsonProperty(propertyName: "borderWidth")]
            public int BorderWidth { get; set; }

            [JsonProperty(propertyName: "animations")]
            public int Animations { get; set; }

        }
    }
}

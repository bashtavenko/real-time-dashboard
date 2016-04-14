using System.Collections.Generic;
using System.Web.Http;
using RealTimeDashboard.ViewModels;

namespace RealTimeDashboard.Controllers.Api
{
    public class ProductsController : ApiController
    {
        [Route("api/products/{productName}")]
        public IList<Product> Get(string productName)
        {
            return new List<Product>
            {
                new Product {Id = 1, Name = "Adjustable Race", Number = "AR-5381"},
                new Product {Id = 2, Name = "Bearing Ball", Number = "BA-8327"},
                new Product {Id = 3, Name = "BB Ball Bearing", Number = "BE-2349"}
            };
        }
    }
}

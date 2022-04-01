using ProductManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerIntegrationTest.Endpoint.api.v1
{

    public abstract class TestRoute
    {
        private string version= "1";

        public string Category { get => ApiRoute.Category.Replace("{version:apiVersion}", version);}

        public string Product { get => ApiRoute.Product.Replace("{version:apiVersion}", version);}

        public string Customer { get => ApiRoute.Customer.Replace("{version:apiVersion}", version); }

        public string Seller { get => ApiRoute.Seller.Replace("{version:apiVersion}", version); }

        public string Purchase { get => ApiRoute.Purchase.Replace("{version:apiVersion}", version); }

        public string Sale { get => ApiRoute.Sale.Replace("{version:apiVersion}", version); }

        public string Account { get => ApiRoute.Account.Replace("{version:apiVersion}", version); }

        public TestRoute(string v){
            version = v;
        }

        public string Single(string source, long id){
            return source + "/" + id;
        }

        public string Paged(string source, int page){
            return $"{source}?{nameof(PagingRequestParams.PageNumber)}={page}";
        }

        public string Paged(string source, int page, int pageSize){
            return $"{source}?{nameof(PagingRequestParams.PageNumber)}={page}&{nameof(PagingRequestParams.PageSize)}={pageSize}";
        }
    }
}

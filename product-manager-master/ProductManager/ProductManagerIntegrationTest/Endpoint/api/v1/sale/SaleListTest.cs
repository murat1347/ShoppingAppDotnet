using ProductManager.DTO;
using ProductManagerDTO.DTO.Product;
using ProductManagerIntegrationTest.Endpoint.api.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerIntegrationTest.Endpoint.api.v1.sale
{
    [Collection("TestCollection1 #1")]
    public class SaleListTest : AbstractTest
    {
        [Fact]
        public async void GetAllSeller_WithUnAuthorized_Except401()
        {
            var response = await Client.GetAsync(testRoute.Sale);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void GetAllSales_WithNoParameter_Except20Sales()
        {
            var response = await AuthorizedClient.GetAsync(testRoute.Sale);
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var salePagedDTO = await DeSerializeJsonObject<SalePagedDTO>(response);

            var sales = salePagedDTO.Sales;

            var dbSales = DbContext.Sales.ToList();

            Assert.NotEmpty(dbSales);

            Assert.Equal(20, sales.Count);

            for (int i = 0; i < 10; i++)
            {
                var dbSale = dbSales[i];
                var sale = sales[i];

                Assert.Equal(dbSale.Id, sale.Id);
                Assert.Equal(dbSale.Amount, sale.Amount);
                Assert.Equal(dbSale.DateTime, sale.DateTime);
            }
        }

        [Fact]
        public async void GetAllSales_WithPageTwo_Except20Sales()
        {
            var response = await AuthorizedClient.GetAsync(testRoute.Paged(testRoute.Sale, 2));
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var salePagedDTO = await DeSerializeJsonObject<SalePagedDTO>(response);

            var sales = salePagedDTO.Sales;

            var dbSales = DbContext.Sales.
                                Skip(PagingRequestParams.DEFAULT_PAGE_SIZE).
                                Take(PagingRequestParams.DEFAULT_PAGE_SIZE).
                                ToList();

            Assert.NotEmpty(dbSales);

            Assert.Equal(20, sales.Count);

            for (int i = 0; i < dbSales.Count; i++)
            {
                var dbSale = dbSales[i];
                var sale = sales[i];

                Assert.Equal(dbSale.Id, sale.Id);
                Assert.Equal(dbSale.Amount, sale.Amount);
                Assert.Equal(dbSale.DateTime, sale.DateTime);
            }
        }

        [Fact]
        public async void GetAllSales_WithPageTwoPageSize_Except50Sales()
        {
            var response = await AuthorizedClient.GetAsync(testRoute.Paged(testRoute.Sale, 2, 50));
            response.EnsureSuccessStatusCode();

            EnsureContentTypeIsUtf8AppJson(response);

            var salePagedDTO = await DeSerializeJsonObject<SalePagedDTO>(response);

            var sales = salePagedDTO.Sales;

            var dbSales = DbContext.Sales.
                                Skip(50).
                                Take(50).
                                ToList();

            Assert.NotEmpty(dbSales);

            Assert.Equal(dbSales.Count, sales.Count);

            for (int i = 0; i < dbSales.Count; i++)
            {
                var dbSale = dbSales[i];
                var sale = sales[i];

                Assert.Equal(dbSale.Id, sale.Id);
                Assert.Equal(dbSale.Amount, sale.Amount);
                Assert.Equal(dbSale.DateTime, sale.DateTime);
            }
        }

       
    }
}

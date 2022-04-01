using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductManager.Controllers;
using ProductManager.IRepository;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagerUnitTest.Controller
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task GetOne_WithUnExistingItemId_ObjectResult(){

            var repository = new Mock<IGenericRepository<Product>>();

            repository.Setup(repo => 
                            repo.Get(
                                 It.IsAny<Expression<Func<Product, bool>>>(),
                                 null
                            )).ReturnsAsync((Product)null);

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.Repository<Product>()).Returns(repository.Object);

            var logger = new Mock<ILogger<ProductController>>();

            var mapper = new Mock<IMapper>();

            var controller = new ProductController(unitOfWork.Object, logger.Object, mapper.Object,null,null);

            var result = await controller.GetOne(0);

            Assert.IsType<ObjectResult>(result);
        }

    }
}

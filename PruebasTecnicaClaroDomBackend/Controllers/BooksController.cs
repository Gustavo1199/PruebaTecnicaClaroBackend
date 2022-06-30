using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PruebasTecnicaClaroDomBackend.Models;
using PruebasTecnicaClaroDomBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebasTecnicaClaroDomBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        BaseService baseService = new BaseService();

        [Route("GetAllBook"),HttpGet]
        public IActionResult GetallBooks()
        {
           var result = baseService.getallBooks();
            return Ok(result);
        }

        [Route("AddBooks"),HttpPost]
        public IActionResult AddBooks(BooksModel books)
        {
            var result = baseService.AddBooks(books);
            return Ok(result);
        }

        [Route("GetBooksById/{id}"), HttpGet]
        public IActionResult GetBooksById(int id)
        {
            var result = baseService.GetBooksById(id);
            return Ok(result);
        }

        [Route("UpdateBooks/{id}"), HttpPut]
        public IActionResult UpdateBooks(BooksModel books, int id)
        {
            var result = baseService.UpdateBooks(books,id);
            return Ok(result);
        }

        [Route("DeleteBooks/{id}"), HttpDelete]
        public IActionResult DeleteBooks(int id)
        {
            var result = baseService.DeleteBooks(id);
            return Ok(result);
        }
    }
}

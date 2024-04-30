using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatG08.Repository.Data;
using TalabtG08.APIs.Errors;

namespace TalabtG08.APIs.Controllers
{
   
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext dbContext;

        public BuggyController(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = dbContext.Products.Find(100);
            if(product == null) { return NotFound(new ApiErrorResponse(404));}

            return Ok(product);
        }

        [HttpGet("serrver")]
        public ActionResult GetServerErorr()
        {
            var product = dbContext.Products.Find(100);
            var ReturnProduct = product.ToString(); //it will therow excption

            return Ok(ReturnProduct);
        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {

            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("badRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
          
            return Ok();
        }

    }
}

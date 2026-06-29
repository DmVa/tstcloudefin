using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Vendor.Logic.Interfaces;

namespace Vendor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly ILogger<VendorController> _logger;
        private readonly IVendorService _vendorService;

        public VendorController(ILogger<VendorController> logger, IVendorService vendorService)
        {
            _logger = logger;
            _vendorService = vendorService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get list of vendors")]
        public  async Task<ActionResult<IEnumerable<Logic.Dto.Vendor>>> List(int skip = 0, int take = 10)
        {
            if (skip < 0 || take < 0)
                return BadRequest("Skip must be >= 0 and take must be >= 0.");

            var vendors = await _vendorService.GetVendors(skip, take);
            return Ok(vendors);
            
        }
        

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get vendor by id")]
        public async Task<ActionResult<Logic.Dto.Vendor>> GetVendor(string id)
        {
            return Ok(await _vendorService.GetVendor(id));
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update vendor")]
        public async Task<IActionResult> Update(Logic.Dto.Vendor vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vendor model not valid");

            await _vendorService.Update(vendor);
            return Ok();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add new vendor")]
        public async Task<ActionResult> Add(Logic.Dto.Vendor vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest("Vendor model not valid");

            await _vendorService.Add(vendor);
            return Ok();
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete vendor by id")]
        public Task Delete(string id)
        {
            return _vendorService.Delete(id);
        }
    }
}

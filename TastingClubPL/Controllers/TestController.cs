using Microsoft.AspNetCore.Mvc;

namespace TastingClubPL.Controllers
{
    #region debug
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<string>> GetTest(string drinkType = null, string country = null, string price = null)
        {
            return Ok(string.Concat("drink Type = ", drinkType,"\ncountry = ", country));

        }
    }
    #endregion debug

}

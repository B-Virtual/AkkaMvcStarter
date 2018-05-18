using System;
using Microsoft.AspNetCore.Mvc;

namespace BVirtual.FaTi.Controllers
{
    [Route("/api/status")]
    [Route("/api/v1/status")]
    public class StatusController
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                var result = new
                {
                    Result = "SUCCESS",
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                var result = new
                {
                    Result = "FAILED",
                    Description = e
                };

                return new JsonResult(result);
            }
        }

    }
}

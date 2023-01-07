using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    //Swagger'ın bu actionu bir endpoint olarak algılamaması için yazıyoruz, aksi halde get i ya da post u olmadığı için
    //swagger hata fırlatır
    public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
    {
        if (response.StatusCode == 204)
            return new ObjectResult(null)
            {
                StatusCode = response.StatusCode
            };
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}
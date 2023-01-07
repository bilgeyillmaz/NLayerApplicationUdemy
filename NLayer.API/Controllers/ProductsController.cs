using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly IService<Product> _productService;

    public ProductsController(IMapper mapper, IService<Product> productService)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var products = await _productService.GetAllAsync();
        var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
        // return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        var productsDto = _mapper.Map<ProductDto>(product);
        return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
    }

    [HttpPost]
    public async Task<IActionResult> Save(ProductDto productDto)
    {
        var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));
        var productsDto = _mapper.Map<ProductDto>(product);
        return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
    {
        await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    //Delete api /products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "There is no item with this Id"));
        }
        await _productService.RemoveAsync(product);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}
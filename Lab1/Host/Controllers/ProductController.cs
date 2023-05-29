using Application.Contracts.Dtos;
using Application.Contracts.Dtos.Product;
using Application.Contracts.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using Domain.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Authorize(Roles = "sm")]
    public class ProductController : Controller
    {
        private readonly IProductService _iProductService;
        private readonly INotyfService _iNotyf;
        public ProductController(IProductService productRepository,
                                 INotyfService notyfService)
        {
            _iProductService = productRepository;
            _iNotyf = notyfService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _iProductService.GetListFilterProductAsync(new Paging<ProductDto, RequestGetListFilterProductDto>()));
        }
        [HttpPost]
        public async Task<IActionResult> Index(Paging<ProductDto, RequestGetListFilterProductDto> input)
        {
            return View(await _iProductService.GetListFilterProductAsync(input));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]       
        public async Task<IActionResult> Create(RequestCreateProductDto input)
        {
            try
            {
                var result = await _iProductService.CreateAsync(input);
                switch (result)
                {
                    case true:
                        _iNotyf.Success("Create success", 4);
                        return RedirectToAction("Index");
                        //return View();
                    default:
                        _iNotyf.Warning("Please check data input", 4);
                        return View();
                }
            }
            catch(Exception ex)
            {
                _iNotyf.Error(ex.Message, 4);
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _iProductService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(RequestUpdateProductDto input)
        {
            try
            {
                var result = await _iProductService.UpdateAsync(input);
                if (result)
                {
                    _iNotyf.Success("Update success", 4);
                    return RedirectToAction("Index");
                }
                _iNotyf.Warning("Update fail", 4);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _iNotyf.Error(ex.Message, 4);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _iProductService.DeleteAsync(id);
                if (result)
                {
                    _iNotyf.Success("Delete success", 4);
                    return RedirectToAction("Index");
                }
                _iNotyf.Warning("Delete fail", 4);
                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                _iNotyf.Error("Error system", 4);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

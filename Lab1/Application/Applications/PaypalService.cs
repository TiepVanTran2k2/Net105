using Application.Contracts.Dtos.Payment;
using Application.Contracts.Dtos.Product;
using Application.Contracts.Services;
using Domain.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PayPal.Core;
using PayPal.v1.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class PaypalService : IPaypalService
    {
        private readonly IConfiguration _iConfiguration;
        private readonly ICacheHelper _iCacheHelper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _iHttpContextAccessor;
        public PaypalService(IConfiguration configuration,
                             ICacheHelper cacheHelper,
                             UserManager<IdentityUser> userManager,
                             IHttpContextAccessor iHttpContextAccessor)
        {
            _iConfiguration = configuration;
            _iCacheHelper = cacheHelper;
            _userManager = userManager;
            _iHttpContextAccessor = iHttpContextAccessor;
        }

        public Task<string> CreateUrlSandboxPaypalAsync(PaymentInformationModel input)
        {
            try
            {
                var environment = new SandboxEnvironment(_iConfiguration["Paypal:Key"], _iConfiguration["Paypal:Secret"]);
                var client = new PayPalHttpClient(environment);

                #region Create Paypal Order
                var itemList = new ItemList()
                {
                    Items = new List<Item>()
                };
                var total = Math.Round(input.Amount/ 23000, 2);
                var listProductCache = _iCacheHelper.GetAsync<ItemCacheDto>(input.User);
                foreach (var item in listProductCache.ListProductCache)
                {
                    itemList.Items.Add(new Item()
                    {
                        Name = item.Name,
                        Currency = "USD",
                        Price = item.Price.ToString(),
                        Quantity = item.Count.ToString(),
                        Sku = "sku",
                        Tax = "0"
                    });
                }
                #endregion

                var paypalOrderId = DateTime.Now.Ticks;
                var hostname = $"{_iHttpContextAccessor.HttpContext.Request.Scheme}://{_iHttpContextAccessor.HttpContext.Request.Host}";
                var payment = new Payment()
                {
                    Intent = "sale",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            Amount = new Amount()
                            {
                                Total = total.ToString(),
                                Currency = "USD",
                                Details = new AmountDetails
                                {
                                    Tax = "0",
                                    Shipping = "0",
                                    Subtotal = total.ToString()
                                }
                            },
                            ItemList = itemList,
                            Description = $"Invoice #{paypalOrderId}",
                            InvoiceNumber = paypalOrderId.ToString()
                        }
                    },
                    RedirectUrls = new RedirectUrls()
                    {
                        //CancelUrl = $"{hostname}/GioHang/CheckoutFail",
                        ReturnUrl = $"{hostname}"
                    },
                    Payer = new Payer()
                    {
                        PaymentMethod = "paypal"
                    }
                };

                PaymentCreateRequest request = new PaymentCreateRequest();
                request.RequestBody(payment);
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

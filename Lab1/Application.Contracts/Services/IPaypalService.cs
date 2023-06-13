using Application.Contracts.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IPaypalService
    {
        Task<string> CreateUrlSandboxPaypalAsync(PaymentInformationModel input);
    }
}

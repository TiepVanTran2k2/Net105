using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Dtos.Bill
{
    public class BillDto
    {
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public List<DetailBillDto> DetailBillDto { get; set; }
    }
    public class DetailBillDto
    {
        public int Count { get; set; }
        public string Name { get; set; }
        public string UrlImg { get; set; }
        public decimal Price { get; set; }
    }

    public class BillManagerDto
    {
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public List<DetailBillManagerDto> DetailBillDto { get; set; }
    }
    public class DetailBillManagerDto
    {
        public int Count { get; set; }
        public string Name { get; set; }
        public string UrlImg { get; set; }
        public decimal Price { get; set; }
    }
}

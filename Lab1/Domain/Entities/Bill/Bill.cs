using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Bill
{
    public class Bill : BaseEntity
    {
        public string UserId { get; set; }
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public ICollection<DetailBill> DetailBill { get; set; }
    }
    public class DetailBill : BaseEntity
    {
        public Guid BillId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string UrlImg { get; set; }
        public decimal Price { get; set; }
        public virtual Bill Bill { get; set; }
    }
}

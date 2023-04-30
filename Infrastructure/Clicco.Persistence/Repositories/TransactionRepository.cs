using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Infrastructure.Context;
using MediatR;

namespace Clicco.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction, CliccoContext>, ITransactionRepository
    {
        public async Task<TransactionDetailViewModel> GetDetailsByTransactionIdAsync(int transactionId)
        {
            bool isCouponUsed = false;
            var transaction = await GetByIdAsync(transactionId,
                o => o.TransactionDetail,
                o => o.TransactionDetail.Product,
                o => o.Coupon,
                o => o.Address);

            if(transaction == null)
            {
                isCouponUsed = false;
                transaction = await GetByIdAsync(transactionId,
                o => o.TransactionDetail,
                o => o.TransactionDetail.Product,
                o => o.Address);
            }

            var detailViewModel = new TransactionDetailViewModel
            {
                Address = new AddressViewModel
                {
                    City = transaction.Address.City,
                    Country = transaction.Address.Country,
                    State = transaction.Address.State,
                    ZipCode = transaction.Address.ZipCode,
                    Street = transaction.Address.Street,
                },
                Product = new ProductViewModel
                {
                    Code = transaction.TransactionDetail.Product.Code,
                    Name = transaction.TransactionDetail.Product.Name,
                    Description = transaction.TransactionDetail.Product.Description,
                    Quantity = transaction.TransactionDetail.Product.Quantity,
                    SlugUrl = transaction.TransactionDetail.Product.SlugUrl,
                    UnitPrice = transaction.TransactionDetail.Product.UnitPrice,
                },
                Code = transaction.Code,
                CreatedDate = transaction.CreatedDate,
                Dealer = transaction.Dealer,
                DeliveryDate = transaction.DeliveryDate,
                TotalAmount = transaction.TotalAmount,
                TransactionStatus = transaction.TransactionStatus switch
                {
                    TransactionStatus.Failed => "Failed",
                    TransactionStatus.Pending => "Pending",
                    TransactionStatus.Success => "Successful"
                }
            };

            if (isCouponUsed)
            {
                detailViewModel.Coupon = new CouponViewModel
                {
                    Description = transaction.Coupon.Description,
                    DiscountAmount = transaction.Coupon.DiscountAmount,
                    DiscountType = Enum.ToObject(typeof(DiscountType), transaction.Coupon.DiscountType).ToString(),
                    Type = Enum.ToObject(typeof(CouponType), transaction.Coupon.Type).ToString(),
                    ExpirationDate = transaction.Coupon.ExpirationDate,
                    Name = transaction.Coupon.Name,
                };
            }

            return detailViewModel;
        }
    }
}

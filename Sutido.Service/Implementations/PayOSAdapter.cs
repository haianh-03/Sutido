using Net.payOS.Types;
using Net.payOS;
using Sutido.Model.Entites;


namespace Sutido.Service.Implementations
{
    public class PayOSAdapter
    {
        private readonly PayOS _sdk;

        public PayOSAdapter(PayOS sdk)
        {
            _sdk = sdk;
        }

        public async Task<CreatePaymentResult> CreatePaymentAsync(PayOSCreatePaymentRequest request)
        {
            var items = new List<ItemData>
            {
                new ItemData("Thanh toán lớp học", 1, request.Amount)
            };

            var paymentData = new PaymentData(
                request.OrderCode,
                request.Amount,
                request.Description,
                items,
                request.CancelUrl,
                request.ReturnUrl
            );

            var res = await _sdk.createPaymentLink(paymentData);
            return res;
        }
    }
}

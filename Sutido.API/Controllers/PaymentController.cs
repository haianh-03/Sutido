using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Sutido.Model.Entites;
using Sutido.Service;
using Sutido.Service.Implementations;

namespace SutidoWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PayOSAdapter _payOSAdapter;

        public PaymentController()
        {
            // 🔐 Thông tin thật của bạn
            var clientId = "4536b758-896f-4f13-a6ee-85e1ad0656b6";
            var apiKey = "505c5baf-7c3d-4c0d-ad04-d6a62f0bbca9";
            var checksumKey = "49258c39e8a9c27f45ed4c0cd3249405fe8a4174785002f0c76777944da606d0";

            var payOS = new PayOS(clientId, apiKey, checksumKey);
            _payOSAdapter = new PayOSAdapter(payOS);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PayOSCreatePaymentRequest request)
        {
            try
            {
                var result = await _payOSAdapter.CreatePaymentAsync(request);
                return Ok(new
                {
                    code = "00",
                    desc = "Success",
                    data = new
                    {
                        checkoutUrl = result.checkoutUrl,
                        orderCode = result.orderCode
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = "99",
                    desc = "Lỗi khi tạo link thanh toán",
                    message = ex.Message
                });
            }
        }
    }
}

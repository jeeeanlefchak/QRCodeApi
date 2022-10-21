using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;

namespace QRCodeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QRCodeController : ControllerBase
    {

        public QRCodeController()
        {

        }

        [HttpGet("generate")]
        public dynamic Get(string link)
        {
            Byte[] imageData = GenerateByteArray(link);
            return File(imageData, "image/png");
        }


        public static Bitmap GenerateImage(string url)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.L);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(10, Color.White, Color.Black, null, 0, 0, false, null);
            return qrCodeImage;
        }

        public static byte[] GenerateByteArray(string url)
        {
            var image = GenerateImage(url);
            return ImageToByte(image);
        }

        private static byte[] ImageToByte(Image img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }

    }
}

using System;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QC.services;
using QC.models;
using System.Linq;
namespace QC
{
    class Program
    {
        static void Main(string[] args)
        {
            IEntrypContext ent = new EntrypContext();
            string message = "sdfsdjfoids jfsoid jfoids jfoids jfoisjd ofisjd ofijds oifj soidj      L";
            byte[] Custume_key = Convert.FromBase64String("beUPOAX4M0fwb94bDVeQrHlD0ejC8K3DBduPD0pUFWY=");
            byte[] Custume_IV = Convert.FromBase64String("pOHgXcU6AgMQHR57WncOMw==");

            byte[] encrypted = ent.EncryptStringToBytes_Aes(message,Custume_key,Custume_IV);
            string encryptedstr = Convert.ToBase64String(encrypted);
            // MakeQR CODE
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(encryptedstr, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            
            string filename = "sdfsdf"+ ".png";
            string pathsaved = Path.Combine("qrimage",filename);
            qrCodeImage.Save(pathsaved,ImageFormat.Png);
            byte[] encrpyed = Convert.FromBase64String("OFGdzMjlFPivWVXBtFkdYFESBD3jNG/PDGssn+/nlnOLfi1yfHUIAkgHEdJlvKxyWi47Kpc4/7F7BbppYrSHYr5xW3oliJfNqXt4nC1MVQc=");

            Console.WriteLine(ent.DecryptStringFromBytes_Aes(encrpyed,Custume_key,Custume_IV));
        }
    }
}

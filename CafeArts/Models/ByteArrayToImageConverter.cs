using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public static class ByteArrayToImageConverter
    {
        public static string imgDataURL = "";

        public static string Converter (byte[] ImageFromProducts)
        {
            if (ImageFromProducts != null)
            {

                string imreBase64Data = Convert.ToBase64String(ImageFromProducts);

                if (imreBase64Data.StartsWith("/9j"))
                {
                    imgDataURL = string.Format("data:image/jpg;base64,{0}", imreBase64Data);
                }

                else if (imreBase64Data.StartsWith("/9g"))
                {
                    imgDataURL = string.Format("data:image/jpeg;base64,{0}", imreBase64Data);
                }

                else
                {
                    imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                }

                

            }

            return imgDataURL;
        }
    }
}
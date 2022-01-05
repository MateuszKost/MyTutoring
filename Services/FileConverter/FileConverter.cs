using Microsoft.AspNetCore.Components.Forms;

namespace Services.FileConverter
{
    public static class FileConverter
    {
        public static long MaxFileSize = 1024 * 1024 * 15;

        public static async Task<string> IBrowserFileToBase64Async(IBrowserFile file)
        {
            var contentType = file.ContentType;

            if (contentType.Contains("jpeg") || contentType.Contains("png") || contentType.Contains("pdf") || contentType.Contains("doc") || contentType.Contains("docx"))
            {
                using Stream fileStream = file.OpenReadStream(MaxFileSize);
                using MemoryStream ms = new();

                await fileStream.CopyToAsync(ms);
                var base64 = Convert.ToBase64String(ms.ToArray());

                return base64;
            }
            else
                throw new Exception();
        }

        public static string FileBytesToBase64(byte[] file)
        {
            return Convert.ToBase64String(file);
        }

        public static byte[] Base64ToFile(string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}

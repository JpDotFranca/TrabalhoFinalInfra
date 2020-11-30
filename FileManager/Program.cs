using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace FileManager
{
    public class Program
    {
        //static void Main(string[] args)
        //{
        //    using (var filestream = System.IO.File.OpenRead(@"C:\Users\joao\Pictures\perfil (2).png"))
        //    {
        //        byte[] bytes;
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            filestream.CopyTo(memoryStream);
        //            bytes = memoryStream.ToArray();
        //        }

        //        string base64 = Convert.ToBase64String(bytes); 
        //        UploadImage(base64);
        //    }
        //}

        public static void UploadImage(string base64Image)
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioimagem;AccountKey=hrlJaGpOMgRqWvlq8TV9HY5aQGEtAYwmkEMhdCbVSh1lDGVsyS7R5D/4WfF6rLv6qoIwioy3rrRGDfIbj77t6w==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(storageConnectionString);

            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            string imageName = Guid.NewGuid().ToString() + "_" + DateTime.Now.DayOfWeek.ToString()+".jpg";

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageName);
            blockBlob.Properties.ContentType = "image/jpg";

            var bytes = Convert.FromBase64String(base64Image);
            var streamImage = new StreamContent(new MemoryStream(bytes));
            blockBlob.UploadFromStreamAsync(streamImage.ReadAsStreamAsync().Result).Wait();

        }
    }
}

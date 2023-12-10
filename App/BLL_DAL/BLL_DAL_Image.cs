using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BLL_DAL
{
    public class BLL_DAL_Image
    {
        public HttpClient client;
        public HttpResponseMessage res;
        public string imagePath;

        public BLL_DAL_Image()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://nrodark.click:7777");
        }

        public async Task<System.Drawing.Image> uploadFile(FileInfo file)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(file.OpenRead()), "upload_file", file.Name);
            var result = await client.PostAsync($"{client.BaseAddress}api/v1/image/uploadImage/", form);
            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                var err = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorContent);
                Console.WriteLine(err["message"]);
            }
            var resContent = await result.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(resContent);

            string path = res["path"];
            var imageDes = await client.GetByteArrayAsync($"{client.BaseAddress}{path}");
            MemoryStream ms = new MemoryStream(imageDes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            imagePath = path;
            return img;
        }
    }
}

using BAL;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class ItemController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Signature, string AdditionalText)
        {
            // Validate and process the signature image data
            if (!string.IsNullOrEmpty(Signature))
            {
                try
                {
                    string base64Data = Signature.Split(',')[1];
                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                    // Generate a unique filename for the PNG image (you may want to use a more robust method)
                    string fileName = Guid.NewGuid().ToString() + ".png";

                    // Specify the directory where you want to save the image (adjust as needed)
                    string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SignatureImages");

                    // Ensure the directory exists, create it if not
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Combine the directory path and file name
                    string filePath = Path.Combine(directoryPath, fileName);

                    // Save the image to the specified path
                    System.IO.File.WriteAllBytes(filePath, imageBytes);

                    // Now, you can store the filename or path in your database or perform other actions
                    Console.WriteLine("Signature image saved: " + fileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving signature image: " + ex.Message);
                }
            }

            // You can also process and save the AdditionalText if needed

            return RedirectToAction("Create");
        }
    }

}

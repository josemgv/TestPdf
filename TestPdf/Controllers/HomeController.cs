using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestPdf.Models;
using IronOcr;


namespace TestPdf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index(int s)
        {                                              
            return View();
        }

        
        public IActionResult Test()
        {
                                    
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            //var path = Path.Combine(
            //            Directory.GetCurrentDirectory(), "wwwroot",
            //            file.FileName);

            var path = file.FileName;            

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //ConvertPdfToXmlAEAT(path);

            ConvertPdfToTxtTGSS(path);

            return RedirectToAction("Test");
        }
                             

        #region TGSS

            private void ConvertPdfToTxtTGSS(string fileName)
            {
                //Installation.LicenseKey = "IRONOCR.JOSEMARIAGONZALEZ.4671-9263DFFC0C-BLCEPDLCGNRB6-K7NRKTR5MFDQ-WV6ELKUAOG23-WCCMNK6QYWXN-XGTWW2RQ4B44-56ITS5-TDFHWKVJYM2AUA-DEPLOYMENT.TRIAL-LQM6DL.TRIAL.EXPIRES.10.JUL.2021";
                var Ocr = new IronTesseract();
                Ocr.Language = OcrLanguage.SpanishBest;

                using (var Input = new OcrInput(fileName))
                {
                    // OCR entire document
                    //Input.AddPdf(fileName);
                    var Result = Ocr.Read(Input);
                    string test = Result.Text;
                }
            }


        #endregion



    }   
}

using Datacom.TaxCalculator.Logic.Contracts;
using Datacom.TaxCalculator.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace Datacom.TaxCalculator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileProvider _fileProvider;
        private readonly ITaxManager _taxManager;

        public HomeController(IFileProvider fileProvider,ITaxManager TaxManager, ILogger<HomeController> logger)
        {
            _logger = logger;
            _fileProvider = fileProvider;
            _taxManager = TaxManager;
        }

        public IActionResult Index()
        {
            return View(new FileUploadModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            var model = new FileUploadModel();
            try
            {
                
                if (file == null || file.Length == 0)
                {
                    model.Message = "file not selected";
                    return View(model);
                }

                if (file.ContentType != "text/csv")
                {
                    model.Message = "Invalid file extension only .csv files are allowed";
                    return View(model);
                }

                if (file.Length > 5120)
                {
                    model.Message = ".csv file too large. upload files less than or equal to 5MB in size";
                    return View(model);
                }

                var fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("yyyyMMddhhmmssmmm") + ".csv";
                var inputfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

                using var stream = new FileStream(inputfilePath, FileMode.Create);

                await file.CopyToAsync(stream);

                model.FileUploadSuccess = true;
                model.FileNameUploaded = fileName;
                model.Message = "Csv File successfully uploaded. Click on process to perform tax calculations";


                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                model.Message = "Unable to upload csv file as an error occured. Kindly try again";
                return View(model);
            }
        }

        public async Task<IActionResult> Process(string fileName)
        {
            
            var result = new BatchProcessResultViewModel();

            try
            {
                if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
                {
                    result.Model.ErrorMessage = "No file was supplied in request";
                    return View(result);
                }
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    result.Model.ErrorMessage = "File not found";
                    return View(result);
                }

                var batchProcessResult = await _taxManager.BatchProcessAsync(filePath);

                batchProcessResult.OutputFileName = Path.GetFileName(batchProcessResult.OutputFileName);

                result.Model = batchProcessResult;

                return View(result);
            }
            catch (Exception ex)
            {
                result.Model.ErrorMessage = "Unable to batch process file";
                _logger.LogError(ex, ex.Message);
                return View(result);
            }
        }

        public async Task<IActionResult> Download(string filename)
        {
            try
            {
                if (filename == null)
                    return Content("filename not present");

                var path = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot", filename);

                var memory = new MemoryStream();
                using var stream = new FileStream(path, FileMode.Open);

                await stream.CopyToAsync(memory);

                memory.Position = 0;
                return File(memory, "text/csv", Path.GetFileName(path));
            }
            catch (Exception ex)   
            {
                _logger.LogError(ex, ex.Message);
                return Content("Error in downloading requested file");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Common.Configs;
using Common.Extensions;
using Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using NLog.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileInfo = Common.Files.FileInfo;

namespace Working.Controllers
{
    public class UploadController : Controller
    {
        private readonly UploadConfig _uploadConfig;

        public UploadController(
            IOptionsMonitor<UploadConfig> uploadConfig
            )
        {
            _uploadConfig = uploadConfig.CurrentValue;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                var filePath = @"D:\UploadingFiles\" + formFile.FileName;

                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            string fileDirectory = @"..\UploadingFiles\";

            foreach (var formFile in files)
            {
                var filePath = fileDirectory + formFile.FileName;

                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size });
        }

        [HttpPost]
        public async Task<IActionResult> UploadOneImage(IFormFile file)
        {
            long size = file.Length;

            var config = _uploadConfig.Document;

            var fileInfo = new FileInfo(file.FileName, file.Length)
            {
                UploadPath = config.UploadPath,
                RequestPath = config.RequestPath
            };

            object args = new { Id = 0};
            var dateTimeFormat = config.DateTimeFormat.NotNull() ? DateTime.Now.ToString(config.DateTimeFormat) : "";
            var format = config.Format.NotNull() ? StringHelper.Format(config.Format, args) : "";
            fileInfo.RelativePath = Path.Combine(dateTimeFormat, format).ToPath();

            if (!Directory.Exists(fileInfo.FileDirectory))
            {
                Directory.CreateDirectory(fileInfo.FileDirectory);
            }

            fileInfo.SaveName = $"{IdWorkerHelper.GenId64()}.{fileInfo.Extension}";

            if (file.Length > 0)
            {
                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await file.CopyToAsync(stream);
                //}

                CancellationToken cancellationToken = default;
                await SaveAsync(file, fileInfo.FilePath, cancellationToken);
            }

            return Ok(new { count = 1, size, fileRelativePath = $"{config.RequestPath}/{fileInfo.FileRelativePath}", name = fileInfo.FileName });
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveAsync(IFormFile file, string filePath, CancellationToken cancellationToken = default)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }
        }
    }
}

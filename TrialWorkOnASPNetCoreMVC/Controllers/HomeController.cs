using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrialWorkOnASPNetCoreMVC.Models;

namespace TrialWorkOnASPNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Explorer(string dir = @"C:\")
        {
            string[] drive = Environment.GetLogicalDrives();

            string dirName = dir;

            long dirSize = DirSize(new DirectoryInfo(dirName)); 

            string[] folderNames = Directory.GetDirectories(dirName);
            long[] folderSizes = new long[folderNames.Length];
            for (int i = 0; i < folderNames.Length; i++)
            {
                long fdS = DirSize(new DirectoryInfo(folderNames[i])); 
                folderSizes[i] = fdS;
            }

            string[] fileNames = Directory.GetFiles(dirName);
            long[] fileSizes = new long[fileNames.Length];
            for (int i = 0; i < fileNames.Length; i++)
            {
                long flS = DirSize(new DirectoryInfo(fileNames[i]));
                fileSizes[i] = flS;
            }

            DirectoryData dirData = new()
            {
                DirName = dirName,
                DirSize = dirSize,
                FolderNames = folderNames,
                FolderSizes = folderSizes,
                FileNames = fileNames,
                FileSizes = fileSizes
            };

            return View(dirData);
        }

        private long DirSize(DirectoryInfo dirName)
        {
            long size = 0;

            if (!Directory.Exists($"{dirName}"))
            {
                return size;
            }

            FileInfo[] files;
            DirectoryInfo[] dirs;
            try
            {
                files = dirName.GetFiles();
                dirs = dirName.GetDirectories();


                foreach (FileInfo file in files)
                {
                    size += file.Length;
                }

                foreach (DirectoryInfo dir in dirs)
                {
                    size += DirSize(dir);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"! {ex}");
            }

            return size;
        }
  

        #region 
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }

}

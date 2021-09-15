using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrialWorkOnASPNetCoreMVC.Models
{
    public class DirectoryData
    {
        public string DirName { get; set; }
        public long DirSize { get; set; }
        public string[] FolderNames { get; set; }
        public long[] FolderSizes { get; set; }
        public string[] FileNames { get; set; }
        public long[] FileSizes { get; set; }
    }
}

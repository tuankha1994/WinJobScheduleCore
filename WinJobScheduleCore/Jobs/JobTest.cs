using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinJobScheduleCore.Jobs
{
    //This is format a job 
    public class JobWriteText : JobBase
    {
        public override string JobCode
        {
            get
            {
                return AppEnum.JobCode.WriteText.ToString();
            }
        }

        //Execute job here
        public override void OnExecuting()
        {
            var directory = Path.Combine(AppConstants.StorageRootPath, "JobResult");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fileName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-tt") + ".txt";

            string path = Path.Combine(directory, fileName);

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(DateTime.Now.ToString());
            }            
        }
    }
}

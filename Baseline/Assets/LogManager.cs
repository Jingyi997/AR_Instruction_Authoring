using UnityEngine;
using System.Collections;
using System.IO;

#if !UNITY_EDITOR
using System.Threading.Tasks;
using Windows.Storage;
using System;
#endif

namespace iiscommon.Utilities
{
    public class LogManager
    {
        private string fileName_, filePath_;
        private System.DateTime logStartTime_;

        private bool addTimestamp_ = false;
        //private string logFileName_ = "training_data/Participant_6/test";
        private string logFileName_ = "user_study/Log files/Baseline/Laurie";
        
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(string logName, bool addTimestamp)
        {
            addTimestamp_ = addTimestamp;

            logStartTime_ = System.DateTime.Now;

            fileName_ = System.DateTime.Now.ToString("yyMMdd_HHmmss_") + logName + ".csv";
            filePath_ = Application.persistentDataPath;

            string fullPath = filePath_ + "/" + logFileName_ + "/" + fileName_;

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            Debug.Log(string.Format("Saving log file to: {0}", fullPath));

            using (FileStream fs = new FileStream(fullPath, FileMode.CreateNew))
            {
                using (StreamWriter outputFile = new StreamWriter(fs))
                {

                }
            }
        }

        public void WriteToLog(string line)
        {
            string fullPath = filePath_ + "/" + logFileName_ + "/" + fileName_;
            using (FileStream fs = new FileStream(fullPath, FileMode.Append))
            {
                using (StreamWriter outputFile = new StreamWriter(fs))
                {
                    string outputLine = line;

                    if (addTimestamp_)
                    {
                        System.DateTime currentTime = System.DateTime.Now;
                        System.TimeSpan elapsedTime = currentTime - logStartTime_;
                        string timestamp = elapsedTime.TotalSeconds.ToString();
                        outputLine += "," + timestamp;
                    }

                    outputFile.WriteLine(outputLine);
                }
            }
        }
    }
}
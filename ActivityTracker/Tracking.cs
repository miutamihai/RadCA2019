using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Tracker.DataLayer;
using Tracker.WebAPIClient;

namespace ActivityTracker
{
    public static class Activity
    {
        public static string StudentID { get { return Properties.Settings.Default.StudentID; } }
        public static string Name { get {return Properties.Settings.Default.StudentName; } }
        public static string ActivityName { get { return Properties.Settings.Default.StudentName; } }
        // Local logic for posting a task
        // uses web API endpoint
        public static string getCurrentGID()
        {
            var result = ActivityAPIClient.APIGet<string>("api/ActivityReport/get/StudentActivityGUID/SID/"
                + Properties.Settings.Default.StudentID + "/Activity/" + Properties.Settings.Default.ActivityName);
            return result;

        }
        public static bool Track(string task)
        {
            //if (Properties.Settings.Default.CopyID == null || Properties.Settings.Default.CopyID == "")
            //{
            //    Properties.Settings.Default.CopyID = Guid.NewGuid().ToString();
            //    Properties.Settings.Default.Save();
            //}

            // Using new API call to get Copy ID or gets a new CopyID
            string CopyID = getCurrentGID();

            StudentEntity entity = new StudentEntity
            {
                StudentID = Properties.Settings.Default.StudentID,
                ActivityName = Properties.Settings.Default.ActivityName,
                ip = GetLocalIPAddress(),
                Name = Properties.Settings.Default.StudentName,
                PartitionKey = Properties.Settings.Default.StudentID,
                //    CopyId = Properties.Settings.Default.CopyID,
                CopyId = CopyID,
                Task = task
            };
            var post = ActivityAPIClient.APIPost("api/ActivityReport/post/ActivitiesList/Activity",
            entity);

            return post;

        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}


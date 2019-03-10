using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PostOfficesDataDisplayer.Utils
{
    /// <summary>
    /// URL Manager.
    /// </summary>
    public static class URLManager
    {
        /// <summary>
        /// The URL Base path.
        /// </summary>
        public static string URLBase = "http://geojson.io/#data=data:application/json,";

        /// <summary>
        /// Opens the URL.
        /// </summary>
        /// <param name="endPoint">End point.</param>
        public static void OpenURL(string endPoint)
        {
            //set default browser to chrome, seems like Edge cant open such long URLs
            if (endPoint.Length == 0)
            {
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(URLBase + endPoint);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show("Save as geojson and open in geojson.io", "Too long URL");
            }
        }
    }
}

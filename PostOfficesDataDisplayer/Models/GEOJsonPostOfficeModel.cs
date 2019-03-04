using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace PostOfficesDataDisplayer.Models
{

    public static class URLManager
    {
        public static string URLBase = "http://geojson.io/#data=data:application/json,";

        public static void OpenURL(string endPoint)
        {
            //set default browser to chrome, seems like Edge cant open such long URLs
            System.Diagnostics.Process.Start(URLBase + endPoint);
        }
    }

    public class GEOJsonPostOfficeCollection
    {
        

        public string type = "FeatureCollection";

        public List<GEOJsonFeature> features;

        public GEOJsonPostOfficeCollection(IList<PostOffice> postOffices)
        {
            this.features = new List<GEOJsonFeature>();
            foreach (var el in postOffices)
            {
                this.features.Add(new GEOJsonFeature(el));
            }
        }

        public string JSONString()
        {
            string json = new JavaScriptSerializer().Serialize(this);
            return json;
        }

        public string EscapedJSONString()
        {
            return Uri.EscapeDataString(JSONString());

        }
    }

    public class GEOJsonFeature
    {
        public string type = "Feature";

        public GEOJsonPoint geometry;

        public GEOJsonProperties properties;

        public GEOJsonFeature(GEOJsonPoint geom, GEOJsonProperties prop)
        {
            this.geometry = geom;
            this.properties = prop;
        }

        public GEOJsonFeature(PostOffice p)
        {
            this.geometry = new GEOJsonPoint(p);
            this.properties = new GEOJsonProperties(p);
        }
    }

    public class GEOJsonPoint
    {
        public string type = "Point";
        public List<double> coordinates;

        public GEOJsonPoint(List<double> coords, string type = "Point")
        {
            this.type = type;
            this.coordinates = new List<double>();
            coords.ForEach(el => this.coordinates.Add(el));
        }

        public GEOJsonPoint(PostOffice p, string type = "Point")
        {
            this.type = type;
            this.coordinates = new List<double>();
            this.coordinates.Add(p.Location.Coords.X);
            this.coordinates.Add(p.Location.Coords.Y);
        }
    }

    public class GEOJsonProperties
    {
        public string postOfficeFullName;
        public string postOfficeShortName;
        public string district;
        

        public GEOJsonProperties(PostOffice p)
        {
            this.postOfficeFullName = p.FullName;
            this.postOfficeShortName = p.ShortName;
            this.district = p.Location.District;
        }
    }
}

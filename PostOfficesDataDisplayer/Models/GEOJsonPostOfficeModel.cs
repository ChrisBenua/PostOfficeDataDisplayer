using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace PostOfficesDataDisplayer.Models
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
            System.Diagnostics.Process.Start(URLBase + endPoint);
        }
    }

    /// <summary>
    /// GEOJson for post offices collection.
    /// </summary>
    public class GEOJsonPostOfficeCollection
    {
        
        /// <summary>
        /// The type.
        /// </summary>
        public string type = "FeatureCollection";

        /// <summary>
        /// The features.
        /// </summary>
        public List<GEOJsonFeature> features;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:PostOfficesDataDisplayer.Models.GEOJsonPostOfficeCollection"/> class.
        /// </summary>
        /// <param name="postOffices">Post offices.</param>
        public GEOJsonPostOfficeCollection(IList<PostOffice> postOffices)
        {
            this.features = new List<GEOJsonFeature>();
            foreach (var el in postOffices)
            {
                this.features.Add(new GEOJsonFeature(el));
            }
        }

        /// <summary>
        /// gets json representation of postOffices.
        /// </summary>
        /// <returns>The json string.</returns>
        public string JSONString()
        {
            string json = new JavaScriptSerializer().Serialize(this);
            return json;
        }

        /// <summary>
        /// Escapeds the JSONS tring.
        /// </summary>
        /// <returns>The JSONS tring.</returns>
        public string EscapedJSONString()
        {
            return Uri.EscapeDataString(JSONString());

        }
    }

    /// <summary>
    /// GEOJson feature.
    /// </summary>
    public class GEOJsonFeature
    {
        /// <summary>
        /// The type.
        /// </summary>
        public string type = "Feature";

        /// <summary>
        /// The geometry.
        /// </summary>
        public GEOJsonPoint geometry;

        /// <summary>
        /// The properties.
        /// </summary>
        public GEOJsonProperties properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.GEOJsonFeature"/> class.
        /// </summary>
        /// <param name="geom">Geom.</param>
        /// <param name="prop">Property.</param>
        public GEOJsonFeature(GEOJsonPoint geom, GEOJsonProperties prop)
        {
            this.geometry = geom;
            this.properties = prop;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.GEOJsonFeature"/> class.
        /// </summary>
        /// <param name="p">P.</param>
        public GEOJsonFeature(PostOffice p)
        {
            this.geometry = new GEOJsonPoint(p);
            this.properties = new GEOJsonProperties(p);
        }
    }

    /// <summary>
    /// GEOJson point.
    /// </summary>
    public class GEOJsonPoint
    {
        /// <summary>
        /// The type.
        /// </summary>
        public string type = "Point";

        /// <summary>
        /// The coordinates.
        /// </summary>
        public List<double> coordinates;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.GEOJsonPoint"/> class.
        /// </summary>
        /// <param name="coords">Coords.</param>
        /// <param name="type">Type.</param>
        public GEOJsonPoint(List<double> coords, string type = "Point")
        {
            this.type = type;
            this.coordinates = new List<double>();
            coords.ForEach(el => this.coordinates.Add(el));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.GEOJsonPoint"/> class.
        /// </summary>
        /// <param name="p">P.</param>
        /// <param name="type">Type.</param>
        public GEOJsonPoint(PostOffice p, string type = "Point")
        {
            this.type = type;
            this.coordinates = new List<double>();
            this.coordinates.Add(p.Location.Coords.X);
            this.coordinates.Add(p.Location.Coords.Y);
        }
    }

    /// <summary>
    /// GEOJson properties.
    /// </summary>
    public class GEOJsonProperties
    {
        /// <summary>
        /// The full name of the post office .
        /// </summary>
        public string postOfficeFullName;

        /// <summary>
        /// The short name of the post office .
        /// </summary>
        public string postOfficeShortName;

        /// <summary>
        /// The district.
        /// </summary>
        public string district;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.GEOJsonProperties"/> class.
        /// </summary>
        /// <param name="p">P.</param>
        public GEOJsonProperties(PostOffice p)
        {
            this.postOfficeFullName = p.FullName;
            this.postOfficeShortName = p.ShortName;
            this.district = p.Location.District;
        }
    }
}

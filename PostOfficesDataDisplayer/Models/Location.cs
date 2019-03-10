using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PostOfficesDataDisplayer.Utils;
using PostOfficesDataDisplayer.ViewModels;

namespace PostOfficesDataDisplayer.Models
{
    /// <summary>
    /// Point.
    /// </summary>
    public class Point : INotifyPropertyChanged
    {
        /// <summary>
        /// The x(latitude)
        /// </summary>
        private double _x;

        /// <summary>
        /// The y(longtitude)
        /// </summary>
        private double _y;

        /// <summary>
        /// The validate coords predicate.
        /// </summary>
        private Predicate<Double> validateCoords;

        /// <summary>
        /// The x coord string.
        /// </summary>
        private string _xcoordStr;

        /// <summary>
        /// The y coord string.
        /// </summary>
        private string _ycoordStr;

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>The validness</returns>
        public bool Valid()
        {
            double helper;
            var res = ((double.TryParse(XCoordStr ?? "", out helper) || (XCoordStr ?? "").Length == 0) && 
                (double.TryParse(YCoordStr ?? "", out helper) || (YCoordStr ?? "").Length == 0));
            return res;
        }

        /// <summary>
        /// Gets or sets the X Coord string.
        /// </summary>
        /// <value>The X Coord string.</value>
        public string XCoordStr
        {
            get => _xcoordStr;

            set
            {
                var res = Validator.ValidateDouble(value, arg => arg >= -90 && arg <= 90);
                if (res.Item1)
                {
                    X = res.Item2;
                    _xcoordStr = value;
                    OnPropertyChanged();
                }
                else
                {
                    OnIncorrectCoordsEntered();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Y Coord string.
        /// </summary>
        /// <value>The  Y Coord string.</value>
        public string YCoordStr
        {
            get => _ycoordStr;

            set
            {
                var res = Validator.ValidateDouble(value, arg => arg >= -90 && arg <= 90);
                if (res.Item1)
                {
                    Y = res.Item2;
                    _ycoordStr = value;
                    OnPropertyChanged();
                }
                else
                {
                    OnIncorrectCoordsEntered();
                }
                
            }
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X
        {
            get => _x;

            set
            {
                _x = value;
            }
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y
        {
            get => _y;

            set
            {
                _y = value;                
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.Point"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;

            this.validateCoords = new Predicate<double>(arg => Math.Abs(arg) <= 90);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.Point"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Point(string x, string y)
        {
            XCoordStr = x;
            YCoordStr = y;

            this.validateCoords = new Predicate<double>(arg => Math.Abs(arg) <= 90);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.Point"/> class.
        /// </summary>
        /// <param name="other">Other.</param>
        public Point(Point other)
        {
            X = other.X;
            Y = other.Y;

            this.validateCoords = new Predicate<double>(other.validateCoords);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.Point"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="validate">Validate.</param>
        public Point(double x, double y, Predicate<double> validate) : this(x, y)
        {
            this.validateCoords = new Predicate<double>(validate);
        }


        /// <summary>
        /// Degreeses to radians.
        /// </summary>
        /// <returns>The to radians.</returns>
        /// <param name="deg">Deg.</param>
        public static double DegreesToRadians(double deg)
        {
            return deg * Math.PI / 180;

        }

        /// <summary>
        /// Dists between Points.
        /// </summary>
        /// <returns>The distance</returns>
        /// <param name="_center">To point</param>
        public double DistTo(Point _center)
        {
            //Haversine formula [https://en.wikipedia.org/wiki/Haversine_formula]
            double EarthRadius = 6371;
            double deltaLat = DegreesToRadians(this.X - _center.X);
            double deltaLon = DegreesToRadians(this.Y - _center.Y);
            double hav = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                         Math.Cos(DegreesToRadians(this.X)) * 
                         Math.Cos(DegreesToRadians(_center.X)) *
                         Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double ans = EarthRadius * 2 * Math.Atan2(Math.Sqrt(hav), Math.Sqrt(1 - hav));
            return ans; ;
        }

        /// <summary>
        /// Ons the incorrect coords entered.
        /// </summary>
        public void OnIncorrectCoordsEntered()
        {
            IncorrectCoordsEntered?.Invoke();
        }

        /// <summary>
        /// Occurs when incorrect coords entered.
        /// </summary>
        public event Action IncorrectCoordsEntered;

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public void OnPropertyChanged([CallerMemberName]string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// post office's Location.
    /// </summary>
    public class Location: INotifyPropertyChanged
    {
        /// <summary>
        /// The administrative area.
        /// </summary>
        private string _admArea;

        /// <summary>
        /// Gets or sets the adm area.
        /// </summary>
        /// <value>The adm area.</value>
        public string AdmArea
        {
            get => _admArea;

            set
            {
                _admArea = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The district.
        /// </summary>
        private string _district;

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>The district.</value>
        public string District
        {
            get => _district;

            set
            {
                _district = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The coordinates.
        /// </summary>
        private Point _coords;

        /// <summary>
        /// Gets or sets the coords.
        /// </summary>
        /// <value>The coords.</value>
        public Point Coords
        {
            get => _coords;

            set
            {
                _coords = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Dists between Locations.
        /// </summary>
        /// <returns>The to.</returns>
        /// <param name="other">Other.</param>
        public double DistTo(Point other)
        {
            return this.Coords.DistTo(other);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.Location"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="district">District.</param>
        /// <param name="admArea">Adm area.</param>
        public Location(string x, string y, string district, string admArea)
        {
            this.Coords = new Point(x, y);
            this.District = district;
            this.AdmArea = admArea;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Models.Location"/> class.
        /// </summary>
        public Location()
        {
            this.Coords = new Point(0, 0);
            this.District = "";
            this.AdmArea = "";
        }

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

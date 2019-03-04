using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PostOfficesDataDisplayer.Models
{

    public class Point : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        private Predicate<Double> validateCoords;

        private string _xcoordStr;

        private string _ycoordStr;

        public bool Valid()
        {
            double helper;
            var res = ((double.TryParse(XCoordStr ?? "", out helper) || (XCoordStr ?? "").Length == 0) && 
                (double.TryParse(YCoordStr ?? "", out helper) || (YCoordStr ?? "").Length == 0));
            return res;
        }

        public string XCoordStr
        {
            get => _xcoordStr;

            set
            {
                double helper;

                if (!double.TryParse(value, out helper) && value.Length > 0 || !(validateCoords?.Invoke(helper) ?? true) || value.Contains(","))
                {
                    OnIncorrectCoordsEntered();
                }
                else
                {
                    X = helper;
                    _xcoordStr = value;
                    OnPropertyChanged();
                }

                /*if (double.TryParse(value, out helper))
                {
                    X = helper;
                }
                _xcoordStr = value;
                OnPropertyChanged();*/

            }
        }

        public string YCoordStr
        {
            get => _ycoordStr;

            set
            {
                double helper;

                if (!double.TryParse(value, out helper) && value.Length > 0 || !(validateCoords?.Invoke(helper) ?? true) || value.Contains(","))
                {
                    OnIncorrectCoordsEntered();
                }
                else
                {
                    Y = helper;
                    _ycoordStr = value;
                    OnPropertyChanged();
                }

                /*if (double.TryParse(value, out helper))
                {
                    Y = helper;
                }
                _ycoordStr = value;
                OnPropertyChanged();*/
            }
        }

        public double X
        {
            get => _x;

            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get => _y;

            set
            {
                _y = value;                
            }
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;

            this.validateCoords = new Predicate<double>(arg => Math.Abs(arg) <= 90);
        }

        public Point(string x, string y)
        {
            XCoordStr = x;
            YCoordStr = y;

            this.validateCoords = new Predicate<double>(arg => Math.Abs(arg) <= 90);
        }

        public Point(Point other)
        {
            X = other.X;
            Y = other.Y;

            this.validateCoords = new Predicate<double>(other.validateCoords);
        }

        public Point(double x, double y, Predicate<double> validate) : this(x, y)
        {
            this.validateCoords = new Predicate<double>(validate);
        }

        public double DistTo(Point other)
        {
            return Math.Sqrt((this.X - other.X) * (this.X - other.X) +
                (this.Y - other.Y) * (this.Y - other.Y));
        }

        public void OnIncorrectCoordsEntered()
        {
            IncorrectCoordsEntered?.Invoke();
        }

        public event Action IncorrectCoordsEntered;

        public void OnPropertyChanged([CallerMemberName]string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Location: INotifyPropertyChanged
    {

        private string _admArea;

        public string AdmArea
        {
            get => _admArea;

            set
            {
                _admArea = value;
                OnPropertyChanged();
            }
        }

        private string _district;

        public string District
        {
            get => _district;

            set
            {
                _district = value;
                OnPropertyChanged();
            }
        }

        private Point _coords;

        public Point Coords
        {
            get => _coords;

            set
            {
                _coords = value;
                OnPropertyChanged();
            }
        }

        public double DistTo(Point other)
        {
            return this.Coords.DistTo(other);
        }

        public Location(string x, string y, string district, string admArea)
        {
            this.Coords = new Point(x, y);
            this.District = district;
            this.AdmArea = admArea;
        }

        public Location()
        {
            this.Coords = new Point(0, 0);
            this.District = "";
            this.AdmArea = "";
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

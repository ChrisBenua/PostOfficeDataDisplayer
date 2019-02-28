using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PostOfficesDataDisplayer.Models
{

    public class Point
    {
        private double _x;
        private double _y;

        private Predicate<Double> validateCoords;

        public double X
        {
            get => _x;

            set
            {
                if (validateCoords?.Invoke(value) ?? true)
                {
                    _x = value;
                }
                else
                {
                    OnIncorrectCoordsEntered();
                }
            }
        }

        public double Y
        {
            get => _y;

            set
            {
                if (validateCoords?.Invoke(value) ?? true)
                {
                    _x = value;
                }
                else
                {
                    OnIncorrectCoordsEntered();
                }
            }
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point(string x, string y)
        {
            double xx; double yy;
            if (!double.TryParse(x, out xx) || !double.TryParse(y, out yy))
            {
                throw new ArgumentOutOfRangeException();
            }
            this.X = xx;
            this.Y = yy;
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

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PostOfficesDataDisplayer.Models;
using PostOfficesDataDisplayer.ViewModels;

namespace PostOfficesDataDisplayer.Utils
{
    public static class IOHelper
    {
        public static void WriteHeaders(string filePath)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine("ROWNUM;FullName;ShortName;PostalCode;AdmArea;District;" +
                    "Address;AddressExtraInfo;ChiefPhone;DeliveryDepartmentPhone;TelegraphPhone;" +
                    "WorkingHours;WorkingHoursExtraInfo;ClassOPS;TypeOPS;MMP;CloseFlag;CloseExtraInfo;" +
                    "UNOM;X_WGS84;Y_WGS84;GLOBALID;");
            }
        }

        private static string Wrap(string s)
        {
            return "\"" + s + "\"";
        }

        private static string SerializePostOffice(PostOffice p)
        {
            string res = "";
            res += p.RowNum + ";" + String.Join(";", (new string[] { p.FullName, p.ShortName, p.Contacts.PostalCode, p.Location.AdmArea,
            p.Location.District, p.Contacts.Address, p.Contacts.AddressExtraInfo, p.Contacts.ChiefPhone, p.Contacts.DeliveryDepartmentPhone,
            p.Contacts.TelegraphPhone, p.Schedule.WorkingHours, p.Schedule.WorkingHoursExtra, p.ClassOPS.ToString(), p.TypeOPS,
            p.MMR, p.CloseFlag, p.CloseExtraInfo, p.UNOM, p.Location.Coords.X.ToString(), p.Location.Coords.Y.ToString(), p.GlobalID }).ToList().
            ConvertAll<string>(s => Wrap(s)));
            return res;
        }

        public static void WriteData(ObservableCollection<PostOffice> postOffices, string filePath, bool append)
        {
            if (!append)
            {
                WriteHeaders(filePath);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                foreach (var el in postOffices)
                {
                    file.WriteLine(SerializePostOffice(el));
                }
            }
        }

        public static (bool, List<PostOffice>) ReadData(string filePath)
        {
            List<PostOffice> postOffices = new List<PostOffice>();

            var lines = System.IO.File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; ++i)
            {
                List<int> positions = new List<int>();
                
                for (int j = 0; j < lines[i].Length; ++j)
                {
                    if (lines[i][j] == '\"')
                        positions.Add(j);
                }

                List<string> postArgs = new List<string>();

                for (int pos = 0; pos < positions.Count - 1; pos += 2)
                {
                    int next = positions[pos + 1];
                    int curr = positions[pos];

                    int len = Math.Max(next - curr - 1, 0);

                    postArgs.Add(lines[i].Substring(curr + 1, len));
                }

                postArgs.Insert(0, lines[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).First());

                bool isValid;
                string message;
                (isValid, message) = Validate(postArgs);

                if (!isValid)
                {
                    MessageBox.Show(message, "Wrong File Format", MessageBoxButton.OK, MessageBoxImage.Error);
                    return (false, new List<PostOffice>());
                }

                postOffices.Add(new PostOffice(postArgs[0], postArgs[1], postArgs[2], postArgs[3], postArgs[4], postArgs[5],
                    postArgs[6], postArgs[7], postArgs[8], postArgs[9], postArgs[10], postArgs[11], postArgs[12],
                    postArgs[13], postArgs[14], postArgs[15], postArgs[16], postArgs[17], postArgs[18], postArgs[19],
                    postArgs[20], postArgs[21]));
            }

            return (true, postOffices);
        }

        private static (bool, string) Validate(List<string> postArgs)
        {
            if (postArgs.Count != PostOffice.PropertieNames.Length)
            {
                return (false, "Number of columns differs");
            }

            for (int i = 0; i < postArgs.Count; ++i)
            {
                if (PostOfficeDisplayerViewModel.IntegerColumns.Contains(i))
                {
                    int helper;
                    if (!int.TryParse(postArgs[i], out helper) && postArgs[i].Length != 0)
                    {
                        return (false, "Values, that should be Integer cant be converted to an Integer");
                    }
                }

                else if (PostOfficeDisplayerViewModel.DoubleColumns.Contains(i))
                {
                    double helper;
                    if (!double.TryParse(postArgs[i], out helper) && postArgs[i].Length != 0)
                    {
                        return (false, "Values, that should be Double cant be converted to a Double");
                    }
                }

                else if (PostOfficeDisplayerViewModel.MaxLenForStringColumns < postArgs[i].Length)
                {
                    return (false, "Too big string length");
                }
            }

            return (true, "ok");
        }

    }
}

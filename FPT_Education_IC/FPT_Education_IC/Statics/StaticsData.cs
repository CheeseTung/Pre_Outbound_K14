using System.Globalization;
using System.Xml.Linq;
using System;
using System.IO;
using Microsoft.Extensions.Logging.Abstractions;
using FPT_Education_IC.ViewModels;

namespace FPT_Education_IC.Statics
{
    public class StaticsData
    {
        public static string DEFAULT_ROLE = "User";
        public static string ADMIN_ROLE = "Admin";
        public static string STAFF_ROLE = "Staff";
        public static string SUPER_ADMIN_ROLE = "SuperAdmin";

        public static string DEFAULT_IMG = "/assets/img/illustrations/profiles/profile-2.png";

        // Program Type
        public static string OUT_BOUND = "OUT";     // Chương trình out-bound
        public static string IN_BOUND = "IN";       // Chương trình in-bound

        // Program Status
        public static int CREATE_STATUS = 0;     // Chương trình được mở
        public static int START_STATUS = 1;     // Chương trình được triển khai (có thể đăng ký)
        public static int PROCESS_STATUS = 2;   // Chương trình đóng đăng ký - cập nhật hồ sơ
        public static int HAPPEN_STATUS = 3;    // Chương trình đang diễn ra
        public static int CLOSE_STATUS = 4;     // Chương trình đã đóng (đã hoàn thành)

        // Register Status
        public static int REGISTER_REQUEST = 0;     // đăng ký chờ duyệt
        public static int REGISTER_PENDING = 1;     // đăng ký chờ cập nhật thông tin
        public static int REGISTER_CANCEL = 2;      // hủy đăng ký
        public static int REGISTER_ACCEPT = 3;     // đăng ký được duyệt

        public static string FormatCurrency(decimal? value)
        {
            string formattedValue = "0 VNĐ";
            if (value.HasValue)
            {
                // Tạo một đối tượng CultureInfo để đảm bảo định dạng số phù hợp với VND
                CultureInfo cultureInfo = new CultureInfo("vi-VN");

                // Sử dụng hàm ToString để chuyển đổi và định dạng giá trị decimal thành tiền tệ VND
                formattedValue = value.Value.ToString("N0", cultureInfo) + " VNĐ";
            }

            return formattedValue;
        }


        public static void LogToXml(string message, string logFileName, string userUpdate)
        {
            string logFolderPath = Path.Combine("wwwroot", "assets", "log", "program");
            Directory.CreateDirectory(logFolderPath);
            string logFilePath = Path.Combine(logFolderPath, logFileName);

            XElement root;

            if (File.Exists(logFilePath))
            {
                root = XElement.Load(logFilePath);
            }
            else
            {
                root = new XElement("Log");
            }

            XElement logEntry = new XElement("Entry",
                new XElement("Timestamp", DateTime.Now),
                new XElement("Message", message),
                new XElement("UserUpdate", userUpdate)
            );

            root.Add(logEntry);
            root.Save(logFilePath);
        }

        public static List<LogEntry> ReadLogEntries(string logFileName)
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            string logFolderPath = Path.Combine("wwwroot", "assets", "log", "program");
            string logFilePath = Path.Combine(logFolderPath, logFileName);

            if (File.Exists(logFilePath))
            {
                XElement root = XElement.Load(logFilePath);

                foreach (XElement entryElement in root.Elements("Entry"))
                {
                    LogEntry logEntry = new LogEntry
                    {
                        Message = entryElement.Element("Message")?.Value,
                        UserUpdate = entryElement.Element("UserUpdate")?.Value,
                        UpdateDate = DateTime.Parse(entryElement.Element("Timestamp")?.Value)
                    };

                    logEntries.Add(logEntry);
                }
            }

            return logEntries;
        }

    }
}

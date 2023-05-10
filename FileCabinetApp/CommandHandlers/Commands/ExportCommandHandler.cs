using System;
using System.IO;
using FileCabinetApp.CommandHandlers.Helpers;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.FileCabinetService.ServiceComponents;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling export requests.
    /// </summary>
    public class ExportCommandHandler : ServiceCommandHandlerBase
    {
        private const string FileTypeCsv = "csv";
        private const string FileTypeXml = "xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        public ExportCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handles 'export' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("export", StringComparison.OrdinalIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            var makeSnapshot = this.Service.MakeSnapshot();

            var exportParametrs = appCommand.Parameters.Split(' ');

            if (exportParametrs.Length != 2)
            {
                Console.WriteLine("Invalid export parameter. Two parameters are needed.");
                return;
            }

            var format = exportParametrs[0];
            var path = exportParametrs[1];

            if (format != FileTypeCsv && format != FileTypeXml)
            {
                Console.WriteLine("Invalid format entered.");
                return;
            }

            var folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Console.WriteLine($"Export failed: Unable to open file {path}.");
            }
            else if (File.Exists(path))
            {
                if (UserInputHelpers.ReadYesOrNo($"The file already exists. Do you want to overwrite {path}?", true))
                {
                    ExportData(makeSnapshot, format, path);
                }
            }
            else
            {
                ExportData(makeSnapshot, format, path);
            }
        }

        private static void ExportData(FileCabinetServiceSnapshot makeSnapshot, string format, string path)
        {
            using (var sw = new StreamWriter(path))
            {
                switch (format)
                {
                    case FileTypeCsv:
                        makeSnapshot.SaveToCsv(sw);
                        break;
                    case FileTypeXml:
                        makeSnapshot.SaveToXml(sw);
                        break;
                }
            }

            Console.WriteLine($"All records are exported to the file {path}.");
        }
    }
}

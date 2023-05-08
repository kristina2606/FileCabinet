using System;
using System.IO;
using FileCabinetApp.CommandHandlers.Helpers;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.FileCabinetService.ServiceComponents;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Contain code for handling export requests.
    /// </summary>
    public class ExportCommandHandler : ServiceCommandHandlerBase
    {
        private const string FileTypeCsv = "csv";
        private const string FileTypeXml = "xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        public ExportCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handling for export requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("export", StringComparison.InvariantCultureIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            var makeSnapshot = Service.MakeSnapshot();

            var exportParametrs = appCommand.Parameters.Split(' ');

            if (exportParametrs.Length != 2)
            {
                Console.WriteLine("You have entered an invalid export parameter. Two are needed.");
                return;
            }

            var format = exportParametrs[0];
            var path = exportParametrs[1];

            if (format != FileTypeCsv && format != FileTypeXml)
            {
                Console.WriteLine("You entered an invalid format.");
                return;
            }

            var folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Console.WriteLine($"Export failed: can't open file {path}.");
            }
            else if (File.Exists(path))
            {
                if (UserInputHelpers.ReadYesOrNo($"File is exist - rewrite {path}?", true))
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
            using (StreamWriter sw = new StreamWriter(path))
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

            Console.WriteLine($"All records are exported to file {path}.");
        }
    }
}

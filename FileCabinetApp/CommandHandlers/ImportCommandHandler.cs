using System;
using System.IO;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling import requests.
    /// </summary>
    public class ImportCommandHandler : ServiceCommandHandlerBase
    {
        private const string FileTypeCsv = "csv";
        private const string FileTypeXml = "xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        public ImportCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handling for import requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("import", StringComparison.InvariantCultureIgnoreCase))
            {
                base.Handle(appCommand);
            }

            var importParametrs = appCommand.Parameters.Split(' ');

            if (importParametrs.Length != 2)
            {
                Console.WriteLine("You have entered an invalid export parameter. Two are needed.");
                return;
            }

            var format = importParametrs[0];
            var path = importParametrs[1];

            if (format != FileTypeCsv && format != FileTypeXml)
            {
                Console.WriteLine("You entered an invalid format.");
                return;
            }

            if (!File.Exists(path))
            {
                Console.WriteLine($"Import error: {path} is not exist.");
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                FileCabinetServiceSnapshot fileCabinetServiceSnapshot = new FileCabinetServiceSnapshot();
                using (StreamReader sr = new StreamReader(fs))
                {
                    switch (format)
                    {
                        case FileTypeCsv:
                            fileCabinetServiceSnapshot.LoadFromCsv(sr);
                            break;
                        case FileTypeXml:
                            fileCabinetServiceSnapshot.LoadFromXml(sr);
                            break;
                    }
                }

                try
                {
                    this.Service.Restore(fileCabinetServiceSnapshot);
                }
                catch (ImportException dict)
                {
                    foreach (var exeption in dict.ImportExceptionByRecordId)
                    {
                        Console.WriteLine($"Record with id = {exeption.Key} - {exeption.Value}.");
                    }
                }

                Console.WriteLine($"All records were imported from {path}.");
            }
        }
    }
}

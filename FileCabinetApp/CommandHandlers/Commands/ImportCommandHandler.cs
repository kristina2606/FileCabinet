﻿using System;
using System.IO;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.FileCabinetService.ServiceComponents;
using FileCabinetApp.Helpers;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling import requests.
    /// </summary>
    public class ImportCommandHandler : ServiceCommandHandlerBase
    {
        private const string FileTypeCsv = "csv";
        private const string FileTypeXml = "xml";

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        public ImportCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handles 'import' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("import", StringComparison.OrdinalIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            var importParametrs = appCommand.Parameters.Split(' ');

            if (importParametrs.Length != 2)
            {
                Console.WriteLine("Invalid import parameters. Two parameters are needed.");
                return;
            }

            var format = importParametrs[0];
            var path = importParametrs[1];

            if (format != FileTypeCsv && format != FileTypeXml)
            {
                Console.WriteLine("Invalid format specified.");
                return;
            }

            if (!File.Exists(path))
            {
                Console.WriteLine($"Import error: {path} does not exist.");
                return;
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                var fileCabinetServiceSnapshot = new FileCabinetServiceSnapshot();
                using (var sr = new StreamReader(fs))
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

                Console.WriteLine($"All records were imported from '{path}'.");
            }
        }
    }
}

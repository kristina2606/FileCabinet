using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FileCabinetApp;

namespace FileCabinetGenerator
{
    public static class Program
    {
        private const string FileTypeCsv = "csv";
        private const string FileTypeXml = "xml";

        private static readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private static readonly Random random = new Random();


        public static void Main(string[] args)
        {
            string type = string.Empty;
            string path = string.Empty;
            string amount = string.Empty;
            string startId = string.Empty;

            for (var i = 0; i < args.Length; i++)
            {
                if (args[i].Contains('='))
                {
                    var comand = args[i].Split('=');

                    if (comand[0] == "--output-type" && comand[1].ToLowerInvariant() == FileTypeCsv)
                    {
                        type = FileTypeCsv;
                    }

                    if (comand[0] == "--output-type" && comand[1].ToLowerInvariant() == FileTypeXml)
                    {
                        type = FileTypeXml;
                    }

                    if (comand[0] == "--output")
                    {
                        path = comand[1];
                    }

                    if (comand[0] == "--records-amount")
                    {
                        amount = comand[1];
                    }

                    if (comand[0] == "--start-id")
                    {
                        startId = comand[1];
                    }
                }
                else
                {
                    if (args[i] == "-t" && args[i + 1].ToLowerInvariant() == FileTypeCsv)
                    {
                        type = FileTypeCsv;
                    }

                    if (args[i] == "-t" && args[i + 1].ToLowerInvariant() == FileTypeXml)
                    {
                        type = FileTypeXml;
                    }

                    if (args[i] == "-o")
                    {
                        path = args[i + 1];
                    }

                    if (args[i] == "-a")
                    {
                        amount = args[i + 1];

                    }

                    if (args[i] == "-i")
                    {
                        startId = args[i + 1];
                    }
                }
            }

            Create(IntConverter(startId), IntConverter(amount));

            if (type == FileTypeXml)
            {
                ExportXml(path);
            }
            else if (type == FileTypeCsv)
            {
                ExportCsv(path);
            }

            Console.WriteLine($"{amount} records were written to {path}.");

        }

        private static void Create(int idStart, int amount)
        {
            var startDate = new DateTime(1950, 1, 1);
            var daysCount = (DateTime.Now.Year - new DateTime(1950, 1, 1).Year) * 365;

            var genderRandom = new[] { 'm', 'f' };

            for (var i = 0; i < amount; i++)
            {
                list.Add(new FileCabinetRecord
                {
                    Id = idStart + i,
                    FirstName = GetRandomName(),
                    LastName = GetRandomName(),
                    DateOfBirth = startDate.AddDays(random.Next(daysCount)),
                    Gender = genderRandom[random.Next(genderRandom.Length)],
                    Height = (short)random.Next(0, 251),
                    Weight = random.Next(301),
                });
            }
        }

        private static string GetRandomName()
        {
            string letter = "qwertyuiopasdfghjklzxcvbnm";
            var letters = letter.ToCharArray();

            var nameLength = random.Next(2, 61);

            StringBuilder name = new StringBuilder();

            for (int i = 0; i < nameLength; i++)
            {
                name.Append(letters[random.Next(letters.Length)]);
            }

            return name.ToString();
        }

        private static int IntConverter(string inputNumber)
        {
            if (!int.TryParse(inputNumber, CultureInfo.InvariantCulture, out var number))
            {
                throw new ArgumentException($"Conversion failed: {inputNumber}. Please, correct your input.");
            }

            return number;
        }

        private static void ExportCsv(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                FileCabinetRecordCsvWriter recordCsvWriter = new FileCabinetRecordCsvWriter(sw);
                sw.WriteLine("Id,First Name,Last Name,Date of Birth,Gender,Height,Weight");

                foreach (var record in list)
                {
                    recordCsvWriter.Write(record);
                }
            }
        }

        private static void ExportXml(string path)
        {
            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var writer = XmlWriter.Create(path))
            {
                var serializer = new XmlSerializer(typeof(RecordsSeralization));

                var records = new RecordsSeralization();
                var recordNew = new List<FileCabinetRecordSeralization>();

                foreach (var record in list)
                {
                    recordNew.Add(new FileCabinetRecordSeralization
                    {
                        Id = record.Id,
                        FullName = new FullNameSeralization(record.FirstName, record.LastName),
                        DateOfBirth = record.DateOfBirth,
                        Gender = record.Gender,
                        Height = record.Height,
                        Weight = record.Weight,
                    });

                    records.Records = recordNew;
                }

                serializer.Serialize(writer, records, ns);
            }
        }
    }
}



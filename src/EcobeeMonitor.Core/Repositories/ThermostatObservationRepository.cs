using Azure.Storage.Files.DataLake;
using EcobeeMonitor.Core.Models;
using System.Threading.Tasks;
using EcobeeMonitor.Core.Configuration;
using System.IO;
using EcobeeMonitor.Core.Services;
using System.Text;
using Newtonsoft.Json;

namespace EcobeeMonitor.Core.Repositories
{
    public class ThermostatObservationRepository
    {
        private readonly DataLakeServiceClient _dataLakeServiceClient;

        public ThermostatObservationRepository(DataLakeServiceClient dataLakeServiceClient)
        {
            _dataLakeServiceClient = dataLakeServiceClient;
        }

        public Task Save(ThermostatObservation toSave)
        {
            var fileSystemClient = GetFileSystem(DataLakeConfiguration.Containers.ThermostatObservationData);

            var directoryPath = ResolveDirectoryPath(toSave);
            var directoryClient = GetDirectory(fileSystemClient, directoryPath);

            string fileName = ResolveFileName(toSave);
            var fileClient = directoryClient.CreateFile(fileName);

            var dataStream = ConvertToStream(toSave);
            WriteToFile(fileClient.Value, dataStream);

            return Task.CompletedTask;
        }

        private DataLakeFileSystemClient GetFileSystem(string container)
        {
            var client = _dataLakeServiceClient.GetFileSystemClient(container);
            client.CreateIfNotExists();

            return client;

        }

        private static string ResolveDirectoryPath(ThermostatObservation toSave)
        {
            var now = ClockService.Now;
            var directoryPath = $"{now:yyyy}/{now:MM}/{toSave.ThermostatId}";
            return directoryPath;
        }

        private static DataLakeDirectoryClient GetDirectory(DataLakeFileSystemClient fileSystem, string directoryPath)
        {
            var client = fileSystem.GetDirectoryClient(directoryPath);
            client.CreateIfNotExists();

            return client;
        }

        private static string ResolveFileName(ThermostatObservation toSave)
        {
            return $"{toSave.At: yyyy-MM-dd-hh-mm}.json";
        }

        private static MemoryStream ConvertToStream(ThermostatObservation toSave)
        {
            var dataAsString = JsonConvert.SerializeObject(toSave);
            var byteArray = Encoding.ASCII.GetBytes(dataAsString);
            var stream = new MemoryStream(byteArray);
            return stream;
        }

        private static void WriteToFile(DataLakeFileClient file, MemoryStream dataStream)
        {
            var fileSize = dataStream.Length;
            file.AppendAsync(dataStream, offset: 0);
            file.FlushAsync(position: fileSize);
        }
    }
}

using Azure.Storage.Files.DataLake;
using EcobeeMonitor.Core.Models;
using System.Threading.Tasks;
using EcobeeMonitor.Core.Configuration;
using System.IO;
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
            var directoryPath = $"{toSave.At:yyyy}/{toSave.At:MM}/{toSave.ThermostatId}";

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
            return $"{toSave.At: yyyy-MM-dd hh:mm:ss}.json";
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
            file.Append(dataStream, offset: 0);
            file.Flush(position: fileSize);
        }
    }
}

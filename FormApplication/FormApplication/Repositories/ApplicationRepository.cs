using FormApplication.Models;
using Microsoft.Azure.Cosmos;

namespace FormApplication.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly Container _container;

        public ApplicationRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddApplicationAsync(Application application)
        {
            await _container.CreateItemAsync(application, new PartitionKey(application.programId));
        }

        public async Task<Application> GetApplicationAsync(string id, string programId)
        {
            try
            {
                ItemResponse<Application> response = await _container.ReadItemAsync<Application>(id, new PartitionKey(programId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}

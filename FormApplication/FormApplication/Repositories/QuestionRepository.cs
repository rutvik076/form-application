using FormApplication.Models;
using Microsoft.Azure.Cosmos;

namespace FormApplication.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly Container _container;

        public QuestionRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddQuestionAsync(Question question)
        {
            try
            {
                await _container.CreateItemAsync(question, new PartitionKey(question.programId));
            }
            catch (CosmosException ex)
            {
                // Log the exception details for debugging
                Console.WriteLine($"Cosmos DB Exception: {ex.StatusCode}, {ex.Message}, {ex.ResponseBody}");
                throw new Exception($"Cosmos DB Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions here
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            await _container.UpsertItemAsync(question, new PartitionKey(question.programId));
        }

        public async Task<Question> GetQuestionAsync(string id, string programId)
        {
            try
            {
                ItemResponse<Question> response = await _container.ReadItemAsync<Question>(id, new PartitionKey(programId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Question>> GetQuestionsByProgramIdAsync(string programId)
        {
            var query = _container.GetItemQueryIterator<Question>(new QueryDefinition("SELECT * FROM c WHERE c.programId = @programId")
                .WithParameter("@programId", programId));

            List<Question> results = new List<Question>();
            while (query.HasMoreResults)
            {
                foreach (var question in await query.ReadNextAsync())
                {
                    results.Add(question);
                }
            }
            return results;
        }

        public async Task DeleteQuestionAsync(string id, string programId)
        {
            try
            {
                await _container.DeleteItemAsync<Question>(id, new PartitionKey(programId));
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Cosmos DB Exception: {ex.StatusCode}, {ex.Message}, {ex.ResponseBody}");
                throw new Exception($"Cosmos DB Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }
    }
}

using Polly.CircuitBreaker;

namespace OrderServices.Handler
{
    public class ProductServiceClient
    {
        private readonly HttpClient _httpClient;
        public ProductServiceClient(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ProductService");
        }

        public async Task<string> GetProductAsync()
        {
            try
            {


                var response = await _httpClient.GetAsync("product/GetProduct");

                if (!response.IsSuccessStatusCode)
                {
                    return "Fallback: Product not available";


                }

                return await response.Content.ReadAsStringAsync();

            }
            catch (BrokenCircuitException ex)
            {

                return "Product Service is Temporarily Unavailable!";
            }
        }

    }
}

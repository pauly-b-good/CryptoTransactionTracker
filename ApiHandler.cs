using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CryptoTransactionTracker
{
    public class ApiHandler
    {
        // code used to fetch the transaction information from the crypto explorer APIs
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<Transaction>> GetTransactionsAsync(string crypto, string walletAddress)
        {
            string apiUrl = crypto switch
            {
                //"Bitcoin" => $"https://api.blockchain.info/address/{walletAddress}?format=json",
                //"Ethereum" => $"https://api.etherscan.io/api?module=account&action=txlist&address={walletAddress}&startblock=0&endblock=99999999&sort=asc&apikey=YourApiKeyToken",
                //"Litecoin" => $"https://api.blockcypher.com/v1/ltc/main/addrs/{walletAddress}/full",
                "Kaspa" => $"https://api.kaspa.org/addresses/{walletAddress}/full-transactions?limit=20&offset=0",
                "PugDag" => $"https://api.pugdag.com/addresses/{walletAddress}/full-transactions?limit=20&offset=0",
                "Nautilus" => $"https://api.nautilus-network.net/addresses/{walletAddress}/full-transactions?limit=20&offset=0",
                "Hoosat" => $"https://api.network.hoosat.fi/addresses/{walletAddress}/full-transactions?limit=20&offset=0",
                _ => throw new ArgumentException("Unsupported cryptocurrency")
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Transaction>>(responseBody);
        }
    }
}

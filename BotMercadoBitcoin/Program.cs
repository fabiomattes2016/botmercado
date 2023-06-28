using BotMercadoBitcoin.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BotMercadoBitcoin
{
    internal class Program
    {
        private static readonly string baseUrl = "https://api.mercadobitcoin.net/api/v4";
        private static readonly string apiKey = "15cb2b795aab4492c0419c6b3cb2ffa02b2197d0fbcea6c9bf0e885a77233930";
        private static readonly string apiSecret = "3b73d47d34155d5b0ec9cbf42d65046730cea5945a723a26e221ffd856011b92";
        private static readonly HttpClient client = new HttpClient();
        private static readonly string cripto = "RIB";
        private static readonly string fiat = "BRL";
        private static readonly decimal fee = 0.70M;
        private static readonly int interval = 15;

        static async Task Main(string[] args)
        {
            while (true)
            {
                await Bot();
                await Task.Delay(TimeSpan.FromSeconds(interval));
            }
        }

        private static async Task Bot()
        {
            try
            {
                var compra = await BuyCripto(cripto, fiat);
                ResponseOrders ordemCompra = null;

                if (compra != null)
                {
                    ordemCompra = await GetOrder(cripto, fiat, compra.orderId);
                    var price = ordemCompra.avgPrice;
                    await SellCripto(cripto, fiat, price);
                }
                else
                {
                    CultureInfo culture = CultureInfo.InvariantCulture;
                    ResponseTicker tickers = await GetTicker(cripto, fiat);
                    decimal lastPrice = decimal.Parse(tickers.last, culture);
                    decimal fibo = FibonacciRetracementLevels(lastPrice, "38");
                    var price = lastPrice + fibo;
                    await SellCripto(cripto, fiat, price);
                    Console.WriteLine("Aguardando...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await Task.Delay(1000);
        }

        private static async Task<ResponseOrders> GetOrder(string symbol, string fiat, string orderId)
        {
            string token = await GenerateToken(apiKey, apiSecret);
            CultureInfo culture = CultureInfo.InvariantCulture;
            var accountId = await GetAccountId();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage resposta = await client.GetAsync($"{baseUrl}/accounts/{accountId}/{symbol}-{fiat}/orders");

            if(resposta.IsSuccessStatusCode)
            {
                string respostaString = await resposta.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<ResponseOrders>>(respostaString);
                var order = orders.Where(o => o.status == "filled" && o.id == orderId).FirstOrDefault();

                return order;
            }
            else
            {
                return null;
            }

        }

        private static async Task<ResponsePlaceOrder> SellCripto(string symbol, string fiat, decimal buyPrice)
        {
            string token = await GenerateToken(apiKey, apiSecret);
            CultureInfo culture = CultureInfo.InvariantCulture;
            var accountId = await GetAccountId();
            var results = await GetBalances(accountId);
            var availableCripto = results.Where(s => s.symbol == cripto).FirstOrDefault().available;

            if (availableCripto < 4M)
            {
                Console.WriteLine("Aguardando Compra");
                return null;
            }
            else
            {
                ResponseTicker tickers = await GetTicker(cripto, fiat);
                decimal lastPrice = decimal.Parse(tickers.last, culture);
                decimal fibo = FibonacciRetracementLevels(lastPrice, "38");
                decimal qty = Math.Round(availableCripto, 8);
                decimal total = qty * lastPrice;
                decimal feeValue = total * fee / 100;
                decimal calculated = fibo + feeValue;
                decimal price = buyPrice + fibo;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var parametros = new
                {
                    async = false,
                    qty = qty.ToString().Replace(",", "."),
                    side = "sell",
                    limitPrice = price,
                    type = "limit"
                };
                var json = JsonConvert.SerializeObject(parametros);
                var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

                var resposta = await client.PostAsync($"{baseUrl}/accounts/{accountId}/{symbol}-{fiat}/orders", conteudo);

                if (resposta.IsSuccessStatusCode)
                {
                    string respostaString = await resposta.Content.ReadAsStringAsync();
                    var objetoResposta = JsonConvert.DeserializeObject<ResponsePlaceOrder>(respostaString);
                    Console.WriteLine("Venda posicionada...");
                    return objetoResposta;
                }
                else
                {
                    return null;
                }
            }
        }

        private static async Task<ResponsePlaceOrder> BuyCripto(string symbol, string fiat)
        {
            string token = await GenerateToken(apiKey, apiSecret);
            CultureInfo culture = CultureInfo.InvariantCulture;
            var accountId = await GetAccountId();
            var results = await GetBalances(accountId);
            var availableFiat = results.Where(f => f.symbol == fiat).FirstOrDefault().available;

            if(availableFiat <= 1M)
            {
                Console.WriteLine("Aguardando venda...!");
                return null;
            }
            else
            {
                var tickers = await GetTicker(cripto, fiat);
                var price = tickers.last;

                var qty = Math.Round(availableFiat / decimal.Parse(price, culture), 8);

                var cost = CalculateCost(qty, decimal.Parse(price, culture));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var parametros = new
                {
                    async = false,
                    cost = cost,
                    qty = qty.ToString(),
                    side = "buy",
                    type = "market"
                };
                var json = JsonConvert.SerializeObject(parametros);
                var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

                var resposta = await client.PostAsync($"{baseUrl}/accounts/{accountId}/{symbol}-{fiat}/orders", conteudo);

                if (resposta.IsSuccessStatusCode)
                {
                    string respostaString = await resposta.Content.ReadAsStringAsync();
                    var objetoResposta = JsonConvert.DeserializeObject<ResponsePlaceOrder>(respostaString);
                    Console.WriteLine("Compra efetuada...");
                    return objetoResposta;
                }
                else
                {
                    return null;
                }
            }
        }

        private static int CalculateCost(decimal amount, decimal price)
        {
            var cost = amount * price;
            return (int)cost;
        }

        private static async Task<ResponseTicker> GetTicker(string cripto, string fiat)
        {
            HttpResponseMessage resposta = await client.GetAsync($"{baseUrl}/tickers?symbols={cripto}-{fiat}");

            if (resposta.IsSuccessStatusCode)
            {
                string respostaString = await resposta.Content.ReadAsStringAsync();
                var objetoResposta = JsonConvert.DeserializeObject<List<ResponseTicker>>(respostaString).FirstOrDefault();

                return objetoResposta;
            }
            else
            {
                return null;
            }
        }

        private static async Task<List<ResponseBalances>> GetBalances(string accountId)
        {
            string token = await GenerateToken(apiKey, apiSecret);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage resposta = await client.GetAsync($"{baseUrl}/accounts/{accountId}/balances");

            if (resposta.IsSuccessStatusCode)
            {
                string respostaString = await resposta.Content.ReadAsStringAsync();
                var objetoResposta = JsonConvert.DeserializeObject<List<ResponseBalancesTotal>>(respostaString);
                List<ResponseBalances> balances = new List<ResponseBalances>();

                var availableCripto = objetoResposta.Where(s => s.symbol == cripto).FirstOrDefault().available;

                // Define a cultura (culture) como InvariantCulture
                CultureInfo culture = CultureInfo.InvariantCulture;

                foreach (var balance in objetoResposta)
                {
                    var newBalance = new ResponseBalances()
                    {
                        symbol = balance.symbol,
                        available = decimal.Parse(balance.available, culture),
                    };

                    balances.Add(newBalance);
                }

                return balances;
            }
            else
            {
                return null;
            }
        }

        private static async Task<string> GetAccountId()
        {
            string token = await GenerateToken(apiKey, apiSecret);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage resposta = await client.GetAsync($"{baseUrl}/accounts");

            if (resposta.IsSuccessStatusCode)
            {
                string respostaString = await resposta.Content.ReadAsStringAsync();
                var objetoResposta = JsonConvert.DeserializeObject<List<ResponseAccount>>(respostaString);

                return objetoResposta.FirstOrDefault().id;
            }
            else
            {
                return null;
            }
        }

        private static async Task<ResponseFee> GetFeeOfAsset(string asset)
        {
            HttpResponseMessage resposta = await client.GetAsync($"{baseUrl}/{asset}/fees");

            if (resposta.IsSuccessStatusCode)
            {
                string respostaString = await resposta.Content.ReadAsStringAsync();
                var objetoResposta = JsonConvert.DeserializeObject<ResponseFee>(respostaString);
                return objetoResposta;
            }
            else
            {
                return new ResponseFee() { asset = null, deposit_minimum = null, witdraw_minimum = null, withdraw_fee = null };
            }
        }

        private static async Task<string> GenerateToken(string username, string password)
        {
            var parametros = new { login = username, password };
            var json = JsonConvert.SerializeObject(parametros);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage resposta = await client.PostAsync($"{baseUrl}/authorize", conteudo);

            if (resposta.IsSuccessStatusCode)
            {
                string respostaString = await resposta.Content.ReadAsStringAsync();
                var objetoResposta = JsonConvert.DeserializeObject<ResponseLogin>(respostaString);
                return objetoResposta.access_token;
            }
            else
            {
                return "A requisição falhou com status: " + resposta.StatusCode;
            }
        }

        public static decimal FibonacciRetracementLevels(decimal referenceValue, string reference)
        {
            decimal fibonacci38 = referenceValue * 0.382m;
            decimal fibonacci50 = referenceValue * 0.5m;
            decimal fibonacci61 = referenceValue * 0.618m;

            switch (reference)
            {
                case "38":
                    return fibonacci38;
                case "50":
                    return fibonacci50;
                case "61":
                    return fibonacci61;
                default:
                    return fibonacci50;
            }
        }
    }
}

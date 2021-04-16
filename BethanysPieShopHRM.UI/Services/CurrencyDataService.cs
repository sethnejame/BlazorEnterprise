﻿using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using System.Collections.Generic;
using BethanysPieShopHRM.UI.Interfaces;

namespace BethanysPieShopHRM.UI.Services
{
    public class CurrencyDataService : ICurrencyDataService, IPieShopAPI
    {
        private readonly HttpClient _httpClient;

        public CurrencyDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Currency>>
                (await _httpClient.GetStreamAsync($"api/currency"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Currency> GetCurrencyById(int currencyId)
        {
            return await JsonSerializer.DeserializeAsync<Currency>
                (await _httpClient.GetStreamAsync($"api/currency/{currencyId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfDemo.Services
{
    public static class HttpHelper
    {
        /// <summary>
        /// 单例 HttpClient（推荐做法，避免频繁创建）
        ///
        /// 为什么 HttpClient 可以全局复用？
        /// 1. 线程安全
        ///     HttpClient 的 方法（GetAsync、PostAsync 等）是线程安全的。
        ///     内部用了连接池（SocketsHttpHandler），多个线程同时调用是没问题的。
        /// 2. 避免 Socket 耗尽
        ///     如果每次请求都用 new HttpClient()，请求完成后就释放，会导致端口（TIME_WAIT）堆积，尤其是高并发时。
        ///     单例/长生命周期的 HttpClient 会自动复用 TCP 连接，性能更好。
        /// </summary>
        private static readonly HttpClient _httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(30), // 默认超时 30s
        };

        #region GET
        public static async Task<(bool isSuccess, T? result, string? error)> GetAsync<T>(
            string url,
            string? token = null
        )
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                if (!string.IsNullOrWhiteSpace(token))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(json);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, default, ex.Message);
            }
        }
        #endregion

        #region POST
        public static async Task<(bool isSuccess, T? result, string? error)> PostAsync<T>(
            string url,
            object data,
            string? token = null
        )
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
                if (!string.IsNullOrWhiteSpace(token))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(resultJson);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, default, ex.Message);
            }
        }
        #endregion

        #region PUT
        public static async Task<(bool isSuccess, T? result, string? error)> PutAsync<T>(
            string url,
            object data,
            string? token = null
        )
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = content };
                if (!string.IsNullOrWhiteSpace(token))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(resultJson);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                return (false, default, ex.Message);
            }
        }
        #endregion

        #region DELETE
        public static async Task<(bool isSuccess, string? error)> DeleteAsync(
            string url,
            string? token = null
        )
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                if (!string.IsNullOrWhiteSpace(token))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

using App.Server.Models.Responses;

namespace App.Server.Test.Models
{
    public static class Extensions
    {
        public const string DefaultContentType = "application/json";

        public static HttpRequestMessage ToRequest(this HttpMessageModel requestInfo)
        {
            HttpRequestMessage request = new HttpRequestMessage();

            if (requestInfo.Data != null)
            {
                request.Content = requestInfo.Data.ToHttpContent();
            }

            if (requestInfo.Headers != null)
            {
                foreach (var header in requestInfo.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            request.Method = requestInfo.ApiCall.Method;

            request.RequestUri = new Uri(requestInfo.ApiCall.Path.ToString());

            return request;
        }

        public static HttpContent ToHttpContent(this object data, string contentType = DefaultContentType)
        {
            var serealizedData = JsonConvert.SerializeObject(data);
            return new StringContent(serealizedData, Encoding.UTF8, contentType);
        }

        public static HttpMessageModel ToModel(this HttpResponseMessage response)
        {
            var result = new HttpMessageModel();

            result.StatusCode = (int)(response.StatusCode);
            result.Data = response.Content.ReadAsStringAsync().Result;

            result.Headers = new Dictionary<string, string[]>();
            foreach (var header in response.Headers)
            {
                result.Headers.Add(header.Key, header.Value.ToArray());
            }

            result.ApiCall = null;

            try
            {
                result.BadModel = JsonConvert.DeserializeObject<BadModelResponse>(
                    result.Data as string);
            } catch (Exception)
            {
                result.BadModel = null;
            }

            return result;
        }
    }
}
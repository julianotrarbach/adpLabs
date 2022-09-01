using RestSharp;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AdpLabs.Infaestructure.interview.adpeai
{
    public class InterviewAdpeaiApiService
    {
        private readonly InterviewAdpeaiApiSettings _InterviewAdpeaiApiSettings;


        public InterviewAdpeaiApiService(InterviewAdpeaiApiSettings interviewAdpeaiApiSettings)
        {
            _InterviewAdpeaiApiSettings = interviewAdpeaiApiSettings;
        }

        protected async Task<RestResponse<TResponse>> DoRequest<TResponse>(
            RestClient client,
            RestRequest request)
        {
            try
            {
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", MediaTypeNames.Application.Json);

                var response = await client.ExecuteAsync<TResponse>(request);
                return response;
            }
            catch (HttpRequestException ex)
            {
                //_logger.Error($"{request.Method} => {client.BaseUrl}", ex);
                throw ex;
            }
            catch (Exception ex)
            {
                //_logger.Error($"{request.Method} => {client.BaseUrl}", ex);
                throw ex;
            }
        }


        public async Task<RestResponse<GetTaskResponseModel>> GetTaskAsync()
        {
            var uri = new UriBuilder(_InterviewAdpeaiApiSettings.GetTaskEndPoint);
            var client = new RestClient(new HttpClient() { BaseAddress = uri.Uri }, new RestClientOptions() { MaxTimeout = _InterviewAdpeaiApiSettings.Timeout });
            var request = new RestRequest();
            request.Method = Method.Get;

            var response = await DoRequest<GetTaskResponseModel>(client, request);

            return response;

        }

        public async Task<RestResponse<string>> SubmitTaskAsync(SubmitTaskRequestModel requestParameter)
        {
            var uri = new UriBuilder(_InterviewAdpeaiApiSettings.SubmitTaskEndPoint);
            var client = new RestClient(new HttpClient() { BaseAddress = uri.Uri }, new RestClientOptions() { MaxTimeout = _InterviewAdpeaiApiSettings.Timeout });
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddJsonBody(requestParameter);

            var response = await DoRequest<string>(client, request);

            return response;
        }

    }



}

using AdpLabs.Infaestructure.interview.adpeai;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;

namespace AdpLabs.Domain
{
    public class TaskDomainService
    {
        private const string _messageProblem = "Sorry, we have a problem!";
        private readonly InterviewAdpeaiApiService _interviewAdpeaiApiService;
        public TaskDomainService
            (InterviewAdpeaiApiService interviewAdpeaiApiService)
        {
            _interviewAdpeaiApiService = interviewAdpeaiApiService;
        }

        public async Task<TaskModel> GetAsync()
        {

            try
            {
                var response = await _interviewAdpeaiApiService.GetTaskAsync();

                var task = JsonConvert.DeserializeObject<GetTaskResponseModel>(response.Content);

                TaskModel TaskModel = await this.Calculate(task);
                return TaskModel;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (HttpResponseException ex)
            {
                throw ex;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(_messageProblem);
            }
        }

        public async Task<string> SubmitAsync(TaskModel taskRequest)
        {
            try
            {
                SubmitTaskRequestModel submitTaskRequestModel = new SubmitTaskRequestModel()
                {
                    Id = taskRequest.Id,
                    Result = taskRequest.Result
                };

                var response = await _interviewAdpeaiApiService.SubmitTaskAsync(submitTaskRequestModel);
                return response.Content;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(_messageProblem);
            }

        }

        private async Task<TaskModel> Calculate(GetTaskResponseModel model)
        {

            TaskModel TaskModel = new TaskModel() { Id = model.Id };

            switch (model.Operation)
            {
                case "remainder":
                    TaskModel.Result = model.Left % model.Right;
                    break;
                case "addition":
                    TaskModel.Result = model.Left + model.Right;
                    break;
                case "subtraction":
                    TaskModel.Result = model.Left - model.Right;
                    break;
                case "multiplication":
                    TaskModel.Result = model.Left * model.Right;
                    break;
                case "division":
                    TaskModel.Result = model.Left * model.Right;
                    break;
                default:
                    throw new System.Exception("Invalid operation!");

            }

            return TaskModel;
        }
    }
}

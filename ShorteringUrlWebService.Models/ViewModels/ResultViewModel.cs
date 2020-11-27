namespace ShorteringUrlWebService.Models.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(string error)
        {
            Error = error;
        }

        public ResultViewModel(T body)
        {
            Body = body;
        }

        public string Error { get; set; }

        public T Body;
    }
}

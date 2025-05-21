namespace GlobalShared.Lib.BaseResponseModels
{
    public class IResponseModel<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }

        public bool Error { get; set; }
    }
}

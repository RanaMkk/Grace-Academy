namespace backend.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsSucceeded { get; set; }
        public string? Error { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}

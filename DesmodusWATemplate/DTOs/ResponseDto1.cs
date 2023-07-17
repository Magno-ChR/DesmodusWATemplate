namespace DesmodusWATemplate.DTOs
{
    internal class ResponseDto
    {
        public ResponseDto()
        {
        }

        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
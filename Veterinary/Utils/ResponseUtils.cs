namespace Veterinary.Utils
{
    public class ResponseUtils<T>
    {
        public bool Status { get; set; }
        public List<T> List { get; set; }
        public object Item { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public List<T> Errors { get; set; }

        public ResponseUtils(bool status, List<T> list = null, object item = null, int code = 0, string message = "", List<T> errors = null)
        {
            Status = status;
            List = list ?? new List<T>();
            Item = item;
            Code = code;
            Message = message;
            Errors = errors ?? new List<T>();
        }
    }
}
using System;

namespace Model
{
    public class Header<T> where T : class
    {
        public string ServiceId { get; set; }
        public string Id { get; set; }
        public long Timestamp { get; set; }
        public T Response { get; set; }
    }
}

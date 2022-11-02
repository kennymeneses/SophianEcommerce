namespace Ecommerce.Tools
{
    public class RedisTimeoutPolicySettings
    {
        public int RetryCount { get; set; }
        public int RetryIncrementalInMs { get; set; }
    }
}

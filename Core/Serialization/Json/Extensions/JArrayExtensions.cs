namespace Newtonsoft.Json.Linq
{
    public static class JArrayExtensions
    {
        public static JArray ToJArray(this JArray input)
        {
            if (input != null)
            {
                return JArray.FromObject(input);
            }

            return null;
        }
    }
}
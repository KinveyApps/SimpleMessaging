using Newtonsoft.Json;

namespace SimpleMessaging
{
	[JsonObject(MemberSerialization.OptIn)]
	public class UserMessage
	{
		[JsonProperty]
		public string Message { get; set; }
	}
}

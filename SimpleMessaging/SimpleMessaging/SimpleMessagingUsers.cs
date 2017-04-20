using Newtonsoft.Json;
using Kinvey;

namespace SimpleMessaging
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SimpleMessagingUser : Entity
	{
		[JsonProperty("userName")]
		public string Name { get; set; }

		[JsonProperty("userID")]
		public string UserID { get; set; }
	}
}

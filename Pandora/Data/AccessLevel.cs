using System;

namespace TheBox.Data
{
	public enum AccessLevel
	{
		Player = 0,
		Counselor = 1,
		GameMaster = 2,
		Seer = 3,
		Administrator = 4,
		//Update Access Level -Neo
		Developer = 5,
		Owner =6
		//Update Access Level -End
	}
}
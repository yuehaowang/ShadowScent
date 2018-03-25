public class CharacterData0 : BaseGameDataModel
{
	// 0 - for the player on the top; 1 - for the player on the bottom
	public float rotationY { get; set; }

	// help to clean historical data
	public long Time { get; set; }
}

public class CharacterData1 : BaseGameDataModel
{
	// 0 - for the player on the top; 1 - for the player on the bottom
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }

	public float rotationY { get; set; }

	// help to clean historical data
	public long Time { get; set; }
}
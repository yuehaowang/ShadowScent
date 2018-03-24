public class CharacterData : BaseGameDataModel
{
	
	public int playerId { get; set; } // 0 - for the player on the top; 1 - for the player on the bottom

	// when player is on the top, xyz are zero and rotation means the direction of informative arrow
	// when player is at the bottom, these variables are pellucid, which represents its mechanical status
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }

	public float rotationX { get; set; }
	public float rotationY { get; set; }
	public float rotationZ { get; set; }

	// help to clean historical data
	public long Time { get; set; }
}
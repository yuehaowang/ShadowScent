public class CharacterData : BaseGameDataModel
{
	
	public int playerId { get; set; } // 0 - for the player on the top; 1 - for the player on the bottom

	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }

	public float rotationX { get; set; }
	public float rotationY { get; set; }
	public float rotationZ { get; set; }

}
public class PlayerData {
	public bool connected = true;
}

public class Player1Data : PlayerData {
	public float rotationAngle = 0;
	public float positionX = 0;
	public float positionZ = 0;
}

public class Player0Data : PlayerData {
	public float rotationAngle = 0;
	public bool emitRaycast = false;
}
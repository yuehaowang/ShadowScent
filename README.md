# Shadow Scent

## SocketIO Interface

**player_connection**

```json
{
	"uid" : Number				// 0 for sound mode player, 1 for visual mode player
	"operation" : Number		// 0 for login, 1 for logout
}
```

**player1_action**

```json
{
	"rotationDir" : Int, 		// -1 for anti-clockwise, 1 for clockwise
	"rotationAngle" : Float,	// target angle
	"positionX" : Float			// target x coordinate
	"positionY" : Float			// target y coordinate
}
```

**player0_action**

```json
{
	"rotationDir" : Int, 		// -1 for anti-clockwise, 1 for clockwise
	"rotationAngle" : Float	// target angle
}
```
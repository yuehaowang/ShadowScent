# Shadow Scent

## SocketIO Interface

**player_connection**

```
{
	// 0 for sound mode player, 1 for visual mode player
	"uid" : Number,

	// 0 for login, 1 for logout
	"operation" : Number
}
```

**player1_action**

```
{
	// -1 for anti-clockwise, 1 for clockwise
	"rotationDir" : Int,

	// target angle
	"rotationAngle" : Float,

	// target x coordinate
	"positionX" : Float,

	// target y coordinate
	"positionY" : Float
}
```

**player0_action**

```
{
	// -1 for anti-clockwise, 1 for clockwise
	"rotationDir" : Int,

	// target angle
	"rotationAngle" : Float
}
```
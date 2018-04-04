# Shadow Scent

## SocketIO

### Interface

**player0** and **player1**

```
{
	operation : String,
	data : String
}
```

### Operation Types

**player_connection**

```
{
	// 0 for sound mode player, 1 for visual mode player
	"uid" : Int,

	// 0 for login, 1 for logout
	"operation" : Int
}
```

**player1_transform**

```
{
	// target angle
	"rotationAngle" : Float,

	// target x coordinate
	"positionX" : Float,

	// target y coordinate
	"positionZ" : Float
}
```

**player0_transform**

```
{
	// target angle
	"rotationAngle" : Float
}
```

**player_get_key**
```
{
	"index" : Int
}
```

**player_emit_raycast**
{
	"dirX" : Float,
	"dirY" : Float 
}
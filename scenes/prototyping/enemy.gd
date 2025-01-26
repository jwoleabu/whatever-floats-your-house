extends Node2D

const SPEED = 60
var direction = 1

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	
		#var collision = rcRight.get_collider()
		#print(collision.body.name)
		#while rcRight.collide_with_bodies($"../Player") == false:
	

		#while rcLeft.collide_with_bodies($"../Player") == false:

	position.x += direction * SPEED * delta

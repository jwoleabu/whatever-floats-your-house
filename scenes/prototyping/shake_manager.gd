extends Node

@export var house: RigidBody2D

@export var area1: Area2D

var current_camera_zone: int = 0;

var is_camera_shake: bool = false;

var shake_amt : Vector2 = Vector2.ZERO

@export var phantom_camera: PhantomCamera2D

func _process(delta):
	if !is_camera_shake: return
	
	if is_camera_shake == true:
		print("shaking camera")
		shake_amt = Vector2(randf_range(-1, 1), randf_range(-1, 1)) * 10

func _on_area_1_body_entered(body: Node2D) -> void:
	if body == house:
		current_camera_zone += 1
		is_camera_shake = true
		

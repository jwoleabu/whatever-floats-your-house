extends Node

@export var house: RigidBody2D

@export var area1: Area2D
@export var area2: Area2D

var current_camera_zone: int = 0;

var is_camera_shake: bool = false;

var shake_amt : Vector2 = Vector2.ZERO

@export var noise_emitter: PhantomCameraNoiseEmitter2D

@export var noise: PhantomCameraNoise2D

@export var phantom_camera: PhantomCamera2D

func _ready():
	noise_emitter.set_noise(noise)
	noise_emitter.set_continuous(true)

func _process(delta):
	if is_camera_shake == true:
		print("shaking")
		noise_emitter.emit()
	else:
		print("still")
		noise_emitter.stop(true)

func _on_area_1_body_entered(body: Node2D) -> void:
	if body == house:
		current_camera_zone += 1
		is_camera_shake = true

func _on_area_2d_body_entered(body: Node2D) -> void:
	if body == house:
		current_camera_zone += 1
		is_camera_shake = false

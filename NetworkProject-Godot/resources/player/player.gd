extends KinematicBody2D

const SCALE_OFFSET : int = 1
const PLAYER_TEXTURE = [
	preload("res://sprites/player_red.png"),
	preload("res://sprites/player_blue.png"),
]
#
onready var _label_name : Label = $Label
onready var _sprite : Sprite = $Sprite
#
var type : int

func set_name(name: String) -> void:
	self.name = name
	_label_name.text = name

func set_position(position: Vector2) -> void:
	self.position = position

func set_type(type: int) -> void:
	self.type = type
	_sprite.texture = PLAYER_TEXTURE[type]

func set_scale_multiply(scale_multiply: float) -> void:
	self.scale = Vector2(scale_multiply + SCALE_OFFSET, scale_multiply + SCALE_OFFSET)

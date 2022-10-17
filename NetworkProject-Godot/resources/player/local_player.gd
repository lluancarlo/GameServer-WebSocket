extends KinematicBody2D

const SCALE_OFFSET := 1
const PLAYER_TEXTURE = [
	preload("res://sprites/player_red.png"),
	preload("res://sprites/player_blue.png"),
]
#
signal on_player_move(name, type, position)
onready var _label_name : Label = $Label
onready var _sprite : Sprite = $Sprite
#
export (int) var speed : int = 150
#
export(String) var p_name : String
export(int) var type : int = 1
var velocity : Vector2
var change : bool

func _ready():
	set_name(Globals.local_player_name)

func get_input() -> Vector2:
	velocity = Vector2()
	if Input.is_action_pressed("game_right"):
		velocity.x += 1
		change = true
	if Input.is_action_pressed("game_left"):
		velocity.x -= 1
		change = true
	if Input.is_action_pressed("game_down"):
		velocity.y += 1
		change = true
	if Input.is_action_pressed("game_up"):
		velocity.y -= 1
		change = true
	return velocity.normalized() * speed

func _physics_process(delta):
	velocity = move_and_slide( get_input() )
	if change:
		emit_signal("on_player_move", p_name, type, self.position)
		change = false

func set_name(name: String) -> void:
	self.p_name = name
	_label_name.text = name

func set_position(position: Vector2) -> void:
	self.position = position

func set_type(type: int) -> void:
	self.type = type
	_sprite.texture = PLAYER_TEXTURE[type]

func set_scale_multiply(scale_multiply: float) -> void:
	self.scale = Vector2(scale_multiply + SCALE_OFFSET, scale_multiply + SCALE_OFFSET)

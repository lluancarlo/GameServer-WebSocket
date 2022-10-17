extends Control

onready var _name: TextEdit = $TextEditName
onready var _address: TextEdit = $TextEditAddress
onready var _port: TextEdit = $TextEditPort

func _ready():
	_address.text = "localhost"
	_port.text = "8080"
	_name.text = "Player_Godot"

func _on_Button_pressed():
	Globals.server_url = _address.text
	Globals.server_port = _port.text
	Globals.local_player_name = _name.text
	get_tree().change_scene("res://scenes/game.tscn")

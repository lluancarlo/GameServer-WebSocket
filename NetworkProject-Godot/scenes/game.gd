extends Node

const PLAYER_OBJ : Resource = preload("res://resources/player/player.res")
const MAP_OFFSET : Vector2 = Vector2(255, 225)
const MULTIPLY_OFFSET : Vector2 = Vector2(1.95, 1.95)
const START_POSITION : Vector2 = Vector2(256, 256)
#
onready var _local_player : KinematicBody2D = $LocalPlayer
#
var _client : WebSocketClient
var _players_online : Array

# ENGINE FUNCTIONS
func _ready() -> void:
	_client = WebSocketClient.new()
	_client.connect("connection_closed", self, "_closed")
	_client.connect("connection_error", self, "_closed")
	_client.connect("connection_established", self, "_connected")
	_client.connect("data_received", self, "_on_receive")
	
	# Init var
	_players_online = []

	# Initiate connection to the given URL.
	var err = _client.connect_to_url(Globals.server_url + ":" + Globals.server_port)
	if err != OK:
		print("Unable to connect")
		set_process(false)

func _process(delta) -> void:
	_client.poll()

# PRIVATE FUNCTIONS
func _closed(was_clean = false) -> void:
	print("Closed, clean: ", was_clean)
	set_process(false)

func _connected(proto = "") -> void:
	print("Connected with protocol: ", proto)

func _on_receive() -> void:
	var data = _client.get_peer(1).get_packet().get_string_from_utf8()
	_player_handler(parse_json(data))

func _send(data) -> void:
	var json = JSON.print(data)
	_client.get_peer(1).put_packet(json.to_utf8())

func _player_handler(data: Array) -> void:
	for i in data.size():
		if data[i].Name == _local_player.p_name:
			data.remove(i)
	
	for p in data:
		var name = p.Name
		var position = (Vector2( p.Position.x, 1-p.Position.z ) * 20) + MAP_OFFSET
		var scale_multiply = p.Position.y
		var type = p.Type
		
		var exist: bool
		for po in _players_online:
			if po.name == name:
				po.set_position(position)
				po.set_scale_multiply(scale_multiply)
				exist = true
				break
		
		if !exist:
			_new_player(name, position, scale_multiply, type)

func _new_player(name: String, position: Vector2, scale_multiply: float, type: int):
	var player_ref = PLAYER_OBJ.instance()
	add_child(player_ref)
	player_ref.set_name(name)
	player_ref.set_position(position)
	player_ref.set_scale_multiply(scale_multiply)
	player_ref.set_type(type)
	_players_online.append(player_ref)

func _on_LocalPlayer_on_player_move(name: String, type: int, position: Vector2):
	var convert_pos = (Vector2( position.x, position.y ) / 20 ) - Vector2(12.75, 12.25)
	convert_pos.x = stepify(convert_pos.x, 0.01)
	convert_pos.y = stepify(convert_pos.y, 0.01)
	
	var player = {
		"Name": name,
		"Position": { "x": convert_pos.x, "y": 0, "z": -convert_pos.y},
		"Type": type
	}
	_send(player)

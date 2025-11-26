@tool
extends EditorPlugin

func _enter_tree():
	add_custom_type("ALSource3D", "Node3D", preload("nodes/ALSource3D.cs"), null)
	add_custom_type("ALManager", "Node", preload("nodes/ALManager.cs"), null)
	
	print("OpenAL Audio plugin enabled")

func _exit_tree():
	remove_custom_type("ALSource3D")
	remove_custom_type("ALManager")
	
	print("OpenAL Audio plugin disabled")

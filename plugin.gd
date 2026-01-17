@tool
extends EditorPlugin

const PACKAGE_REFERENCES = """	<ItemGroup>
		<PackageReference Include="openal_soft_bindings" Version="1.0.3" />
		<PackageReference Include="NAudio" Version="2.2.1" />
		<PackageReference Include="BunLabs.NAudio.Flac" Version="2.0.1" />
		<PackageReference Include="NVorbis" Version="0.10.5" />
	</ItemGroup>"""

const PROPERTY_GROUP = """	<PropertyGroup>
		<AllowUnsafeBlocks>true</AllowUnsafeBlocks>
	</PropertyGroup>"""

const DLL_SOURCE = "addons/godot_openal/soft_oal.dll"

func _enter_tree():
	add_custom_type("ALSource3D", "Node3D", preload("nodes/ALSource3D.cs"), null)
	add_custom_type("ALManager", "Node", preload("nodes/ALManager.cs"), null)
	
	# Connect to project settings to detect solution generation
	if not ProjectSettings.settings_changed.is_connected(_on_settings_changed):
		ProjectSettings.settings_changed.connect(_on_settings_changed)
	
	# Run setup immediately in case solution already exists
	_setup_project()
	
	print("godot_openal: plugin enabled")

func _exit_tree():
	remove_custom_type("ALSource3D")
	remove_custom_type("ALManager")
	
	if ProjectSettings.settings_changed.is_connected(_on_settings_changed):
		ProjectSettings.settings_changed.disconnect(_on_settings_changed)
	
	print("godot_openal: plugin disabled")

func _on_settings_changed():
	_setup_project()

func _setup_project():
	var project_name = ProjectSettings.get_setting("application/config/name")
	var csproj_path = "res://%s.csproj" % project_name
	
	# Check if .csproj exists
	if not FileAccess.file_exists(csproj_path):
		push_error("godot_openal: No C# solution found. Please create a C# solution first (Project → Tools → C# → Create C# Solution)")
		return
	
	# Read the .csproj file
	var file = FileAccess.open(csproj_path, FileAccess.READ)
	if not file:
		return
	
	var content = file.get_as_text()
	file.close()
	
	# Check if packages are already added
	if "openal_soft_bindings" in content:
		_copy_dll()
		return
	
	# Find the closing </Project> tag and insert our packages before it
	var insert_pos = content.rfind("</Project>")
	if insert_pos == -1:
		push_error("OpenAL: Could not find </Project> tag in .csproj file")
		return
	
	# Build the content to insert (PropertyGroup + ItemGroup)
	var insert_content = "\n" + PROPERTY_GROUP + "\n\n" + PACKAGE_REFERENCES + "\n"
	
	# Insert before </Project>
	var new_content = content.substr(0, insert_pos) + insert_content + content.substr(insert_pos)
	
	# Write back to file
	file = FileAccess.open(csproj_path, FileAccess.WRITE)
	if file:
		file.store_string(new_content)
		file.close()
		print("godot_openal: Added NuGet packages to ", csproj_path)
	
	_copy_dll()

func _copy_dll():
	var project_name = ProjectSettings.get_setting("application/config/name")
	var dest_path = "res://soft_oal.dll"
	
	# Check if DLL already exists at destination
	if FileAccess.file_exists(dest_path):
		return
	
	# Copy the DLL
	if FileAccess.file_exists(DLL_SOURCE):
		var result = DirAccess.copy_absolute(DLL_SOURCE, dest_path)
		if result == OK:
			print("godot_openal: Copied soft_oal.dll to project root")
		else:
			push_error("godot_openal: Failed to copy soft_oal.dll: ", result)
	else:
		push_error("godot_openal: Source DLL not found at ", DLL_SOURCE)

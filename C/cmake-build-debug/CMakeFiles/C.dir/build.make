# CMAKE generated file: DO NOT EDIT!
# Generated by "NMake Makefiles" Generator, CMake Version 3.12

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


.SUFFIXES: .hpux_make_needs_suffix_list


# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

!IF "$(OS)" == "Windows_NT"
NULL=
!ELSE
NULL=nul
!ENDIF
SHELL = cmd.exe

# The CMake executable.
CMAKE_COMMAND = C:\Users\colin\AppData\Local\JetBrains\Toolbox\apps\CLion\ch-0\182.5107.21\bin\cmake\win\bin\cmake.exe

# The command to remove a file.
RM = C:\Users\colin\AppData\Local\JetBrains\Toolbox\apps\CLion\ch-0\182.5107.21\bin\cmake\win\bin\cmake.exe -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = C:\Appdev\Playground\RayTracers\C

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = C:\Appdev\Playground\RayTracers\C\cmake-build-debug

# Include any dependencies generated for this target.
include CMakeFiles\C.dir\depend.make

# Include the progress variables for this target.
include CMakeFiles\C.dir\progress.make

# Include the compile flags for this target's objects.
include CMakeFiles\C.dir\flags.make

CMakeFiles\C.dir\main.c.obj: CMakeFiles\C.dir\flags.make
CMakeFiles\C.dir\main.c.obj: ..\main.c
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=C:\Appdev\Playground\RayTracers\C\cmake-build-debug\CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building C object CMakeFiles/C.dir/main.c.obj"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) /FoCMakeFiles\C.dir\main.c.obj /FdCMakeFiles\C.dir\ /FS -c C:\Appdev\Playground\RayTracers\C\main.c
<<

CMakeFiles\C.dir\main.c.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing C source to CMakeFiles/C.dir/main.c.i"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe > CMakeFiles\C.dir\main.c.i @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) -E C:\Appdev\Playground\RayTracers\C\main.c
<<

CMakeFiles\C.dir\main.c.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling C source to assembly CMakeFiles/C.dir/main.c.s"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) /FoNUL /FAs /FaCMakeFiles\C.dir\main.c.s /c C:\Appdev\Playground\RayTracers\C\main.c
<<

CMakeFiles\C.dir\vector3.c.obj: CMakeFiles\C.dir\flags.make
CMakeFiles\C.dir\vector3.c.obj: ..\vector3.c
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=C:\Appdev\Playground\RayTracers\C\cmake-build-debug\CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Building C object CMakeFiles/C.dir/vector3.c.obj"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) /FoCMakeFiles\C.dir\vector3.c.obj /FdCMakeFiles\C.dir\ /FS -c C:\Appdev\Playground\RayTracers\C\vector3.c
<<

CMakeFiles\C.dir\vector3.c.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing C source to CMakeFiles/C.dir/vector3.c.i"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe > CMakeFiles\C.dir\vector3.c.i @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) -E C:\Appdev\Playground\RayTracers\C\vector3.c
<<

CMakeFiles\C.dir\vector3.c.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling C source to assembly CMakeFiles/C.dir/vector3.c.s"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) /FoNUL /FAs /FaCMakeFiles\C.dir\vector3.c.s /c C:\Appdev\Playground\RayTracers\C\vector3.c
<<

CMakeFiles\C.dir\ppmfile.c.obj: CMakeFiles\C.dir\flags.make
CMakeFiles\C.dir\ppmfile.c.obj: ..\ppmfile.c
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=C:\Appdev\Playground\RayTracers\C\cmake-build-debug\CMakeFiles --progress-num=$(CMAKE_PROGRESS_3) "Building C object CMakeFiles/C.dir/ppmfile.c.obj"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) /FoCMakeFiles\C.dir\ppmfile.c.obj /FdCMakeFiles\C.dir\ /FS -c C:\Appdev\Playground\RayTracers\C\ppmfile.c
<<

CMakeFiles\C.dir\ppmfile.c.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing C source to CMakeFiles/C.dir/ppmfile.c.i"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe > CMakeFiles\C.dir\ppmfile.c.i @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) -E C:\Appdev\Playground\RayTracers\C\ppmfile.c
<<

CMakeFiles\C.dir\ppmfile.c.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling C source to assembly CMakeFiles/C.dir/ppmfile.c.s"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) /FoNUL /FAs /FaCMakeFiles\C.dir\ppmfile.c.s /c C:\Appdev\Playground\RayTracers\C\ppmfile.c
<<

CMakeFiles\C.dir\ray.c.obj: CMakeFiles\C.dir\flags.make
CMakeFiles\C.dir\ray.c.obj: ..\ray.c
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=C:\Appdev\Playground\RayTracers\C\cmake-build-debug\CMakeFiles --progress-num=$(CMAKE_PROGRESS_4) "Building C object CMakeFiles/C.dir/ray.c.obj"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) /FoCMakeFiles\C.dir\ray.c.obj /FdCMakeFiles\C.dir\ /FS -c C:\Appdev\Playground\RayTracers\C\ray.c
<<

CMakeFiles\C.dir\ray.c.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing C source to CMakeFiles/C.dir/ray.c.i"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe > CMakeFiles\C.dir\ray.c.i @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) -E C:\Appdev\Playground\RayTracers\C\ray.c
<<

CMakeFiles\C.dir\ray.c.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling C source to assembly CMakeFiles/C.dir/ray.c.s"
	C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\cl.exe @<<
 /nologo $(C_DEFINES) $(C_INCLUDES) $(C_FLAGS) /FoNUL /FAs /FaCMakeFiles\C.dir\ray.c.s /c C:\Appdev\Playground\RayTracers\C\ray.c
<<

# Object files for target C
C_OBJECTS = \
"CMakeFiles\C.dir\main.c.obj" \
"CMakeFiles\C.dir\vector3.c.obj" \
"CMakeFiles\C.dir\ppmfile.c.obj" \
"CMakeFiles\C.dir\ray.c.obj"

# External object files for target C
C_EXTERNAL_OBJECTS =

C.exe: CMakeFiles\C.dir\main.c.obj
C.exe: CMakeFiles\C.dir\vector3.c.obj
C.exe: CMakeFiles\C.dir\ppmfile.c.obj
C.exe: CMakeFiles\C.dir\ray.c.obj
C.exe: CMakeFiles\C.dir\build.make
C.exe: CMakeFiles\C.dir\objects1.rsp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=C:\Appdev\Playground\RayTracers\C\cmake-build-debug\CMakeFiles --progress-num=$(CMAKE_PROGRESS_5) "Linking C executable C.exe"
	C:\Users\colin\AppData\Local\JetBrains\Toolbox\apps\CLion\ch-0\182.5107.21\bin\cmake\win\bin\cmake.exe -E vs_link_exe --intdir=CMakeFiles\C.dir --manifests  -- C:\PROGRA~2\MICROS~2\2017\PROFES~1\VC\Tools\MSVC\1415~1.267\bin\Hostx86\x86\link.exe /nologo @CMakeFiles\C.dir\objects1.rsp @<<
 /out:C.exe /implib:C.lib /pdb:C:\Appdev\Playground\RayTracers\C\cmake-build-debug\C.pdb /version:0.0  /machine:X86 /debug /INCREMENTAL /subsystem:console kernel32.lib user32.lib gdi32.lib winspool.lib shell32.lib ole32.lib oleaut32.lib uuid.lib comdlg32.lib advapi32.lib 
<<

# Rule to build all files generated by this target.
CMakeFiles\C.dir\build: C.exe

.PHONY : CMakeFiles\C.dir\build

CMakeFiles\C.dir\clean:
	$(CMAKE_COMMAND) -P CMakeFiles\C.dir\cmake_clean.cmake
.PHONY : CMakeFiles\C.dir\clean

CMakeFiles\C.dir\depend:
	$(CMAKE_COMMAND) -E cmake_depends "NMake Makefiles" C:\Appdev\Playground\RayTracers\C C:\Appdev\Playground\RayTracers\C C:\Appdev\Playground\RayTracers\C\cmake-build-debug C:\Appdev\Playground\RayTracers\C\cmake-build-debug C:\Appdev\Playground\RayTracers\C\cmake-build-debug\CMakeFiles\C.dir\DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles\C.dir\depend


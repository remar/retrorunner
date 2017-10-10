SDL2 := /home/andreas/Projekt/cats/SDL2-CS/bin/Debug/SDL2-CS.dll
CATS := /home/andreas/Projekt/cats/cats-cs/bin/Debug/cats-cs.dll
JSON := /home/andreas/Projekt/cats/JSON.NET/Bin/net45/Newtonsoft.Json.dll
# TARGET is either Debug or Release
TARGET := Debug
TARGET_DIR := ../bin/$(TARGET)

RetroRunner.exe: RetroRunner.cs
	dmcs -r:$(SDL2),$(CATS),$(JSON) *.cs
	@mkdir -p $(TARGET_DIR)
	@cp RetroRunner.exe $(TARGET_DIR)

copy: RetroRunner.exe
	@echo Copying libs and data
	@cp $(SDL2) $(TARGET_DIR)
	@cp $(SDL2).config $(TARGET_DIR)
	@cp $(CATS) $(TARGET_DIR)
	@cp $(JSON) $(TARGET_DIR)
	@cp -r ../data $(TARGET_DIR)

clean:
	@rm -f RetroRunner.exe

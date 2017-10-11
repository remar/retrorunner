SDL2 := /home/andreas/Projekt/cats/SDL2-CS/bin/Debug/SDL2-CS.dll
CATS := /home/andreas/Projekt/cats/cats-cs/bin/Debug/cats-cs.dll

# TARGET is either Debug or Release
TARGET := Debug
TARGET_DIR := ../bin/$(TARGET)
EXE_NAME := RetroRunner.exe
TARGET_EXE = $(TARGET_DIR)/$(EXE_NAME)

$(TARGET_EXE): $(wildcard *.cs)
	@mkdir -p $(TARGET_DIR)
	dmcs -out:$(TARGET_EXE) -r:$(SDL2),$(CATS) *.cs

copy: RetroRunner.exe
	@echo Copying libs and data
	@cp $(SDL2) $(TARGET_DIR)
	@cp $(SDL2).config $(TARGET_DIR)
	@cp $(CATS) $(TARGET_DIR)
	@cp -r ../data $(TARGET_DIR)

clean:
	@rm -f $(TARGET_EXE)

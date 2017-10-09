%.json: %.tmx
	tiled --export-map $< $@

MAPS=$(wildcard *.tmx)
JSON=$(MAPS:.tmx=.json)

all: $(JSON)

clean:
	@rm -f *.json

all:
	$(MAKE) -C data/maps
	$(MAKE) -C src copy

clean:
	$(MAKE) -C data/maps clean
	$(MAKE) -C src clean
	@rm -rf bin

all:
	$(MAKE) -C src copy

clean:
	$(MAKE) -C src clean
	@rm -rf bin

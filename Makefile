map:
	zip -r Map.zip ../Map/
	git add Map.zip
	sudo git commit -m "Updated"
	git push

sound:
	git add Sound\ Effect
	sudo git commit -m "Sound Effect updated"
	git push

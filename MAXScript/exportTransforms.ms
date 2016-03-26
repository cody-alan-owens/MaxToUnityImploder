out_filename= getSaveFileName caption:"SAVE WHERE" initialDir:"$export" filename:"export.csv"
out_file = createFile out_filename

for obj in ( selection as array ) do (
	i = 1
	x = units.formatValue obj.pos[1]
	y = units.formatValue obj.pos[2]
	z = units.formatValue obj.pos[3]
	format "%,%,%,%,%\n" "open" i x y z to:out_file
	i = i + 1
)
	
close out_file

edit out_filename
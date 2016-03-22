out_filename= getSaveFileName caption:"SAVE WHERE" initialDir:"$export" filename:"dope.csv"
out_file = createFile out_filename

for obj in ( selection as array ) do (
	i = 1
	convertToSplineShape obj
	knots = #()
	for splines = 1 to (numSplines obj) do (
		for knots = 1 to (NumKnots obj splines) do (
				knot = getKnotPoint obj splines knots
				format "%, %, %, %, %\n" i splines knot.x knot.y knot.z to:out_file
		)
	)
	i = i + 1
)
	
close out_file

edit out_filename
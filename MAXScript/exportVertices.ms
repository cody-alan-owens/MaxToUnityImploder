out_filename= getSaveFileName caption:"SAVE WHERE" initialDir:"$export" filename:"export.csv"
out_file = createFile out_filename

for obj in ( selection as array ) do (
	i = 1
	convertToSplineShape obj
	knots = #()
	for splines = 1 to (numSplines obj) do (
		if ((numKnots obj splines) == (numSegments obj splines)) then (isClosed = "closed") else (isClosed="open")
		for knots = 1 to (NumKnots obj splines) do (
				knot = getKnotPoint obj splines knots
				x = units.formatValue knot.x
				y = units.formatValue knot.y
				z = units.formatValue knot.z
				format "%,%,%,%,%\n" isClosed splines x y z to:out_file
		)
	)
	i = i + 1
)
	
close out_file

edit out_filename
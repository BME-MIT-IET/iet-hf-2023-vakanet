mkdir out
dir /b /s *.java > out\source.txt
cd out
javac @source.txt -d .
jar cvfm Projlab.jar ..\src\META-INF\MANIFEST.MF *
jar cvfe Test.jar Test Test.class
jar cvfe Gui.jar Main *
cd ..
pause
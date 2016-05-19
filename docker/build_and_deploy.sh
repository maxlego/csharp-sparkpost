git pull origin master
git checkout -b experimenting origin/experimenting
git pull origin experimenting

nuget restore
xbuild /p:Configuration=Release

nuget pack SparkPost/SparkPost.nuspec -Prop Configuration=Release

PACKAGE=$(ls *.nupkg)

nuget push $PACKAGE $APIKEY -s nuget.org
